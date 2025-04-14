using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AsaludEcopetrol.Helpers;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Xml;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class RadicacionElectronica
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
        public int id_gestion_factura_digital { get; set; }

        public int id_cargue_dtll { get; set; }

        public String multiusuario { get; set; }

        public String id_dx1 { get; set; }

        public String gasto { get; set; }

        public String factura_autorizada { get; set; }

        public String requiere_auditoria { get; set; }

        public DateTime? fecha_digita { get; set; }

        public String usuario_digita { get; set; }

        public int estado { get; set; }
        public int estado2 { get; set; }

        public String observaciones { get; set; }

        public int idcargue { get; set; }

        public int detalle { get; set; }

        public List<ref_rechazos_Fac> AllGroups { get; set; }

        public List<ref_rechazos_Fac> UserGroups { get; set; }

        public int[] SelectedGroups { get; set; }

        public String obs { get; set; }

        public String[] SelectedList { get; set; }

        public string listaSeleccionados { get; set; }

        public ICollection<Refcontrolgasto> DetalleGasto { get; set; }

        public ICollection<Refcontrolgasto> DetalleGastook { get; set; }

        public int id_auditor { get; set; }

        public int id_analista { get; set; }

        public int id_auditor2 { get; set; }

        public int id_analista2 { get; set; }


        public String observacion_gasto { get; set; }

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

        public int id_prestador { get; set; }

        public int id_regional { get; set; }

        public int id_ref_cuentas_medicas_analista { get; set; }

        public int id_ref_cuentas_medicas_auditores { get; set; }

        public DateTime fecha_inicial { get; set; }
        public DateTime fecha_final { get; set; }
        public string items { get; set; }


        #endregion

        #region Metodos

        public List<vw_prestadores_lotes> GetRecepcionFacturas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturas(ref MsgRes);
        }

        public List<vw_analistas_lotes> GetRecepcionFacturasAnalistas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturasAnalistas(ref MsgRes);
        }

        public List<managementFacturasanalistas_lotes_okResult> GetFacturaAnalistasok(int? idRol, string usuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturaAnalistasok(idRol, usuario, ref MsgRes);
        }

        public List<Management_Lotes_totales_con_analistaResult> GetLotesAnalistaTotal(DateTime fecha_inicio, DateTime fecha_fin, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetLotesAnalistaTotal(fecha_inicio, fecha_fin, ref MsgRes);
        }

        public List<Management_Lotes_totales_con_analistaDtllResult> GetLotesAnalistaTotalDtll(Int32 Id, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetLotesAnalistaTotalDtll(Id, ref MsgRes);
        }

        public List<managementFacturasanalistas_lotesResult> GetFacturaAnalistas(String usuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturaAnalistas(usuario, ref MsgRes);
        }
        public List<vw_prestadores_lotes> GetRecepcionFacturas2(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetRecepcionFacturas2(ref MsgRes);
        }

        public List<managment_prestadores_facturasResult> GetFacturasByIdRecepcion(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByIdRecepcion(idrecepcion, ref MsgRes);
        }

        public List<managment_prestadores_facturas2Result> GetFacturasByIdRecepcion2(int idrecepcion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByIdRecepcion2(idrecepcion, ref MsgRes);
        }


        public List<managment_prestadores_facturasResult> GetFactura(int idrecepcion, int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFactura(idrecepcion, iddetalle, ref MsgRes);
        }
        public managment_prestadores_facturas_GDResult GetFacturaGD(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturaGD(iddetalle, ref MsgRes);
        }
        public managment_prestadores_facturas_GD_zipResult GetFacturaGD2(int iddetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturaGD2(iddetalle, ref MsgRes);
        }
        public List<managmentprestadoresfacturasestadoResult> GetFacturasByEstado(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstado(idestado, ref MsgRes);
        }
        public List<managmentprestadoresfacturasaceptadasResult> GetFacturasAceptadas(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasAceptadas(idestado, id_usuario, ref MsgRes);
        }
        public List<managmentprestadoresfacturasaceptadasOKResult> GetFacturasAceptadas2(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasAceptadas2(ref MsgRes);
        }
        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturas()
        {
            return BusClass.GetGestionFacturas();
        }

        public List<managmentprestadoresfacturasgestionadasFechasResult> GetGestionFacturasFechas(DateTime fechaIni, DateTime fechaFin)
        {
            return BusClass.GetGestionFacturasFechas(fechaIni, fechaFin);
        }

        public List<managmentprestadoresfacturasgestionadas3Result> GetGestionFacturasv2(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            return BusClass.GetGestionFacturasv2(idDetalle, fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
        }

        public List<managmentprestadoresfacturasgestionadasCompletaResult> GetGestionFacturasv3(String numFac, String nit, String prestador, String sap, int? estado, int? idDetalle)
        {
            return BusClass.GetGestionFacturasv3(numFac, nit, prestador, sap, estado, idDetalle);
        }

        public List<managmentprestadoresfacturasgestionadas2Result> GetGestionFacturasV3Filtrada(int? idDetalle, DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac, string rol, int? idUsuario)
        {
            return BusClass.GetGestionFacturasV3Filtrada(idDetalle, fechainicial, fechafinal, estado, regional, prestador, nit, numFac, rol, idUsuario);
        }


        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidad()
        {
            return BusClass.GetGestionFacturasTrazabilidad();
        }

        public List<managmentprestadoresfacturasgestionadasTrazabilidadResult> GetGestionFacturasTrazabilidadV2(DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        {
            return BusClass.GetGestionFacturasTrazabilidadV2(fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
        }
        //public List<managmentprestadoresfacturasgestionadasTrazabilidadCompletaResult> GetGestionFacturasTrazabilidadV2C(DateTime? fechainicial, DateTime? fechafinal, String estado, int? regional, String prestador, String nit, String numFac)
        //{
        //    return BusClass.GetGestionFacturasTrazabilidadV2C(fechainicial, fechafinal, estado, regional, prestador, nit, numFac);
        //}

        public List<managmentprestadores_estados_factura_totalResult> GetTotalFacturas()
        {
            return BusClass.GetTotalFacturas();
        }

        public List<vw_ref_estado_factura_total_rango> GetRecepcionFacturasRango(Int32 opc)
        {
            return BusClass.GetRecepcionFacturasRango(opc);
        }


        public List<managmentprestadoresFacturasResult> GetFacturasByEstadoAceptadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoAceptadas(idestado, ref MsgRes);
        }
        public List<managmentprestadoresFacturas_rangoResult> GetFacturasByEstadoAceptadasRango(int rango, Int32 id_regional)
        {
            return BusClass.GetFacturasByEstadoAceptadasRango(rango, id_regional);
        }
        public List<managmentprestadoresFacturasAuditorResult> GetFacturasByAuditor(int idestado, int id_usuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByAuditor(idestado, id_usuario, ref MsgRes);
        }
        public List<managmentprestadoresFacturasAuditorOKResult> GetFacturasByAuditor2(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByAuditor2(ref MsgRes);
        }
        public List<managmentprestadoresFacturasAuditorOKCompletaResult> GetFacturasByAuditor3(int idAuditor)
        {
            return BusClass.GetFacturasByAuditor3(idAuditor);
        }

        public List<managmentprestadoresFacturasAuditorEnSubsanacionResult> GetFacturasByAuditorEnSubsanacion(int idAuditor)
        {
            return BusClass.GetFacturasByAuditorEnSubsanacion(idAuditor);
        }

        public List<managmentprestadoresFacturasAprobadasResult> GetFacturasAprobadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasAprobadas(idestado, ref MsgRes);
        }
        public List<managment_prestadores_soportes_clinicosResult> GetSoportesClinicosList(int idcargue, int detalle)
        {
            return BusClass.GetSoportesClinicosList(idcargue, detalle);
        }
        public List<managment_prestadores_documentosResult> GetSoportesList(int detalle)
        {
            return BusClass.GetSoportesList(detalle);
        }
        public management_prestadores_get_soporteResult Getsoporteclinico(int idsoportee)
        {
            return BusClass.Getsoporteclinico(idsoportee);
        }

        public List<ref_rechazos_Fac> Getref_rechazos_Fac(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Getref_rechazos_Fac(ref MsgRes);
        }
        public List<vw_auditores_totales> GetAuditorTotales(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetAuditorTotales(ref MsgRes);
        }

        public Int32 InsertarGestionFacturadigital(ecop_gestion_factura_digital obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionFacturadigital(obj, ref MsgRes);
        }
        public Int32 InsertarGestionFacturadigitalGasto(ecop_gestion_factura_digital_gasto obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public void insertarListadoGestionFacturadigitalGasto(List<ecop_gestion_factura_digital_gasto> obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.insertarListadoGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public void ActualizarGestionFacturadigitalGasto(vw_factura_digital_gasto_total obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarGestionFacturadigitalGasto(obj, ref MsgRes);
        }

        public List<ref_tipo_gasto_facturas> Getref_tipo_gasto_facturas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Getref_tipo_gasto_facturas(ref MsgRes);
        }

        public ecop_firma_digital_sami GetFirmas(Int32? idusuario)
        {
            return BusClass.GetFirmas(idusuario);
        }

        public List<managment_prestadores_list_rechazosResult> GetFacturasByRechazoList(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByRechazoList(id_dtll, ref MsgRes);
        }

        public List<ManagmentDetalleFacturasDevueltasResult> GetDetalleFacturadevuletas(int id_detalle)
        {
            return BusClass.GetDetalleFacturadevuletas(id_detalle);
        }

        public List<ManagmentDetalleFacturasDevueltas_fisResult> GetDetalleFacturadevuletas_FIS(int id_detalle)
        {
            return BusClass.GetDetalleFacturadevuletas_FIS(id_detalle);
        }

        public List<view_ref_estado_facturas> GetEstadoFacturas()
        {
            return BusClass.GetEstadoFacturas();
        }
        public List<Ref_ips_cuentas> GetPrstadorCuentas(string term, int? tipofiltro)
        {
            List<Ref_ips_cuentas> prestadores = BusClass.GetPrstadorCuentas();
            if (!string.IsNullOrEmpty(term) && tipofiltro != null)
            {
                switch (tipofiltro)
                {
                    case 1:
                        prestadores = prestadores.Where(l => l.Nombre.ToUpper().Contains(term.ToUpper())).ToList();
                        break;
                    default:
                        prestadores = new List<Ref_ips_cuentas>();
                        break;
                }
            }
            return prestadores;
        }

        public ecop_gestion_facturas_aprobadas GetFacturasAprobadas(int id_cargue_dtll)
        {
            return BusClass.GetFacturasAprobadas(id_cargue_dtll);
        }

        public void Insertaranalistalote(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.Insertaranalistalote(obj, ref MsgRes);
        }

        public int ActualizarLoteAnalista(ref_analista_lote obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ActualizarLoteAnalista(obj, ref MsgRes);
        }

        public List<managmentprestadoresFacturasOBSResult> GetConsultaObsFactura(Int32? id_af)
        {
            return BusClass.GetConsultaObsFactura(id_af);
        }
        public List<managmentprestadoresfacturasDEV_RECHResult> GetConsultaRechDevFactura()
        {
            return BusClass.GetConsultaRechDevFactura();
        }

        public List<ecop_gestion_factura_digital> GetConsultaGestionFactura(Int32? idDetalle)
        {
            return BusClass.GetConsultaGestionFactura(idDetalle);
        }
        public List<factura_devolucion> GetConsultaGestionDevolucion(Int32? idDetalle)
        {
            return BusClass.GetConsultaGestionDevolucion(idDetalle);
        }
        public List<managmentprestadoresfacturasACEP_ASIGResult> GetConsultaAcep_AsigFactura()
        {
            return BusClass.GetConsultaAcep_AsigFactura();
        }

        public Int32 InsertarControlCambios(ecop_gestion_factura_digital_control_cambios OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarControlCambios(OBJ, ref MsgRes);
        }
        public int ActualizarEstado_Facturas(int idFac, int estadoAntiguo, int estadoNuevo)
        {
            return BusClass.ActualizarEstado_Facturas(idFac, estadoAntiguo, estadoNuevo);
        }

        public void ActualizarAnalistalote(int idlote, int idanalista)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ref_analista_lote obj = db.ref_analista_lote.Where(l => l.id_lote_facturas == idlote).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.id_analista = idanalista;
                        db.SubmitChanges();
                    }
                    else
                    {
                        ref_analista_lote obj2 = new ref_analista_lote();

                        obj2.id_analista = idanalista;
                        obj2.id_lote_facturas = idlote;
                        obj2.fecha_digita = DateTime.Now;
                        obj2.usuario_digita = Convert.ToString(SesionVar.UserName);

                        Insertaranalistalote(obj2, ref MsgRes);
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }

        }

        public Int32 CargarBaseContabilizado(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {

            Int32 IntContador = 0;

            ecop_gestion_factura_digital_contabilizados_lote obj = new ecop_gestion_factura_digital_contabilizados_lote();

            obj.fecha_ingreso = Convert.ToDateTime(DateTime.Now);

            Int32 id_Lote = BusClass.InsertarloteContabilizado(obj, ref MsgRes);

            List<ecop_gestion_factura_digital_contabilizados> OBJDetalle = new List<ecop_gestion_factura_digital_contabilizados>();
            try
            {
                foreach (DataRow item in dt2.Rows)
                {
                    ecop_gestion_factura_digital_contabilizados facturas = new ecop_gestion_factura_digital_contabilizados();

                    if (item["ID FACTURA"].ToString() != "")
                    {
                        facturas.id_lote_contabilizado = id_Lote;
                        facturas.id_cargue_dtll = Convert.ToInt32(item["ID FACTURA"]);
                        facturas.numero_factura = Convert.ToString(item["NUM FACTURA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.documento_contable = Convert.ToString(item["DOCUMENTO CONTABLE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.fecha_contabilizacion = Convert.ToDateTime(item["FECHA_CONTABILIZACION"]);
                        facturas.fecha_autorizacion = Convert.ToDateTime(item["FECHA_AUTORIZACION"]);
                        facturas.digita_fecha = DateTime.Now;
                        facturas.usuario_ingresa = Convert.ToString(SesionVar.UserName);

                        OBJDetalle.Add(facturas);
                        facturas = new ecop_gestion_factura_digital_contabilizados();
                        IntContador = IntContador + 1;
                    }
                }
                BusClass.InsertarFacturacionContabilizado(OBJDetalle, ref MsgRes);
                return id_Lote;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message + "Linea: " + IntContador.ToString();
                return 0;
            }
        }

        public Int32 DispensacionMedicamentosCalidad(medicamentos_dispen_lote lote, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            int ciclos = 0;

            medicamentos_dispen_calidad obj = new medicamentos_dispen_calidad();
            medicamentos_dispen_cargue obj2 = new medicamentos_dispen_cargue();
            List<medicamentos_dispen_calidad> OBJDetalle = new List<medicamentos_dispen_calidad>();
            string columna = "";
            var mensajeLog = "";

            obj2.fecha_cargue = Convert.ToDateTime(DateTime.Now);
            obj2.usuario_digita = SesionVar.UserName;
            obj2.nombre_archivo = nombreArchivo;
            obj2.id_lote = lote.id_lote;
            obj2.año = lote.año;
            obj2.mes = lote.mes;

            var textError = "";
            var resultado = 0;
            log_cargues_masivos logMas = new log_cargues_masivos();

            try
            {
                id_cargue = BusClass.InsertarDispensacionMedicamentosCargue(obj2, ref MsgRes);

                logMas.id_cargue = id_cargue;
                logMas.fecha_Cargue = DateTime.Now;
                logMas.periodo_cargue = DateTime.Now;
                logMas.nombre_digita = SesionVar.NombreUsuario;
                logMas.usuario_digita = SesionVar.UserName;
                logMas.tipo_registro = "Medicamentos dispensación";

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["DEPENDENCIA DE SALUD"].ToString() != "")
                    {
                        var texto = "";
                        var fecha = new DateTime();
                        var entero = 0;
                        var decimales = new decimal();
                        var doble = new double();

                        obj.id_cargue = id_cargue;

                        columna = Convert.ToString(item["DEPENDENCIA DE SALUD"]);
                        texto = Convert.ToString(item["DEPENDENCIA DE SALUD"]);

                        if (texto.Length <= 500)
                        {
                            obj.dependencia_salud = Convert.ToString(item["DEPENDENCIA DE SALUD"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE LA FACTURA";
                        try
                        {
                            fecha = Convert.ToDateTime(item["FECHA DE LA FACTURA"]);
                            if (fecha != null)
                            {
                                obj.fecha_factura = Convert.ToDateTime(item["FECHA DE LA FACTURA"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE FACTURA";
                        texto = Convert.ToString(item["NÚMERO DE FACTURA"]);
                        if (texto.Length <= 500)
                        {
                            obj.numero_factura = Convert.ToString(item["NÚMERO DE FACTURA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE PAGO";
                        try
                        {
                            entero = Convert.ToInt32(item["TIPO DE PAGO"]);
                            if (entero != null)
                            {
                                obj.tipo_pago = Convert.ToInt32(item["TIPO DE PAGO"]);
                            }
                            else
                            {
                                obj.tipo_pago = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO";
                        texto = Convert.ToString(item["CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO"]);
                        if (texto.Length <= 500)
                        {
                            obj.nit = Convert.ToString(item["CÉDULA Ó NIT DE LA FARMACIA/DROGUERÍA/SERVICIO FARMACÉUTICO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE PRESCRIPCIÓN (FÓRMULA)";
                        try
                        {
                            fecha = Convert.ToDateTime(item["FECHA DE PRESCRIPCIÓN (FÓRMULA)"]);
                            if (fecha != null)
                            {
                                obj.fecha_prescripcion = Convert.ToDateTime(item["FECHA DE PRESCRIPCIÓN (FÓRMULA)"]);
                            }
                            else
                            {
                                obj.fecha_prescripcion = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE FÓRMULA";
                        texto = Convert.ToString(item["NÚMERO DE FÓRMULA"]);
                        if (texto.Length <= 500)
                        {
                            obj.numero_formula = Convert.ToString(item["NÚMERO DE FÓRMULA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ORIGEN DE LA FORMULA";
                        texto = Convert.ToString(item["ORIGEN DE LA FORMULA"]);
                        if (texto.Length <= 500)
                        {
                            obj.origen_formula = Convert.ToString(item["ORIGEN DE LA FORMULA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE FORMULA";
                        texto = Convert.ToString(item["TIPO DE FORMULA"]);
                        if (texto.Length <= 500)
                        {
                            obj.tipo_formula = Convert.ToString(item["TIPO DE FORMULA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                        texto = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.tipo_ident_beneficiario = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                        texto = Convert.ToString(item["NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.num_documento_beneficiario = Convert.ToString(item["NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE DEL BENEFICIARIO";
                        texto = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.nom_beneficiario = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DE BENEFICIARIO";
                        texto = Convert.ToString(item["CIUDAD DE BENEFICIARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.ciudad_beneficiario = Convert.ToString(item["CIUDAD DE BENEFICIARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DE DESPACHO.";
                        texto = Convert.ToString(item["CIUDAD DE DESPACHO."]);
                        if (texto.Length <= 500)
                        {
                            obj.ciudad_despacho = Convert.ToString(item["CIUDAD DE DESPACHO."]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "UNIS";
                        texto = Convert.ToString(item["UNIS"]);
                        if (texto.Length <= 500)
                        {
                            obj.unis = Convert.ToString(item["UNIS"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ID IPS";
                        texto = Convert.ToString(item["ID IPS"]);
                        if (texto.Length <= 500)
                        {
                            obj.id_ips = Convert.ToString(item["ID IPS"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMPRE IPS DE ATENCIÓN";
                        texto = Convert.ToString(item["NOMPRE IPS DE ATENCIÓN"]);
                        if (texto.Length <= 500)
                        {
                            obj.nom_ips_atencion = Convert.ToString(item["NOMPRE IPS DE ATENCIÓN"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ID. PRESCRIPTOR";
                        texto = Convert.ToString(item["ID. PRESCRIPTOR"]);
                        if (texto.Length <= 500)
                        {
                            obj.id_prescriptor = Convert.ToString(item["ID. PRESCRIPTOR"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PRESCRIPTOR";
                        texto = Convert.ToString(item["PRESCRIPTOR"]);
                        if (texto.Length <= 500)
                        {
                            obj.prescriptor = Convert.ToString(item["PRESCRIPTOR"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ESPECIALIDAD";
                        texto = Convert.ToString(item["ESPECIALIDAD"]);
                        if (texto.Length <= 500)
                        {
                            obj.especialidad = Convert.ToString(item["ESPECIALIDAD"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)";
                        texto = Convert.ToString(item["CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)"]);
                        if (texto.Length <= 500)
                        {
                            obj.cod_producto_ecop = Convert.ToString(item["CÓDIGO DEL PRODUCTO ECOPETROL (CÓDIGO HIJO)"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CODIGO COMERCIAL";
                        texto = Convert.ToString(item["CODIGO COMERCIAL"]);
                        if (texto.Length <= 500)
                        {
                            obj.cod_comercial = Convert.ToString(item["CODIGO COMERCIAL"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)";
                        texto = Convert.ToString(item["CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)"]);
                        if (texto.Length <= 500)
                        {
                            obj.cod_interno_medicamento = Convert.ToString(item["CÓDIGO INTERNO DE MEDICAMENTO (EL QUE UTILICE EL OPERADOR)"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)";
                        texto = Convert.ToString(item["CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)"]);
                        if (texto.Length <= 500)
                        {
                            obj.cod_barras_medicamento = Convert.ToString(item["CÓDIGO DE BARRAS DEL MEDICAMENTO (UTILICE CODIFICACIÓN EAN 13)"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CUM";
                        texto = Convert.ToString(item["CUM"]).ToUpper();
                        if (texto.Length <= 500)
                        {
                            obj.cum = Convert.ToString(item["CUM"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGISTRO SANITARIO";
                        texto = Convert.ToString(item["REGISTRO SANITARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.registro_sanitario = Convert.ToString(item["REGISTRO SANITARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CLASIFICACIÓN INVIMA";
                        texto = Convert.ToString(item["CLASIFICACIÓN INVIMA"]);
                        if (texto.Length <= 500)
                        {
                            obj.clasificacion_invima = Convert.ToString(item["CLASIFICACIÓN INVIMA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO ATC";
                        texto = Convert.ToString(item["CÓDIGO ATC"]);
                        if (texto.Length <= 500)
                        {
                            obj.cod_atc = Convert.ToString(item["CÓDIGO ATC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)";
                        texto = Convert.ToString(item["GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)"]);
                        if (texto.Length <= 500)
                        {
                            obj.grupo_farmacologico = Convert.ToString(item["GRUPO FARMACOLOGICO (SEGÚN NORMA FARMACOLOGICA DEL INVIMA)"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE DEL MEDICAMENTO EN D.C.I.";
                        texto = Convert.ToString(item["NOMBRE DEL MEDICAMENTO EN D.C.I."]);
                        if (texto.Length <= 500)
                        {
                            obj.nom_medicamento_DCI = Convert.ToString(item["NOMBRE DEL MEDICAMENTO EN D.C.I."]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FORMA FARMACÉUTICA";
                        texto = Convert.ToString(item["FORMA FARMACÉUTICA"]);
                        if (texto.Length <= 500)
                        {
                            obj.forma_farmaceutica = Convert.ToString(item["FORMA FARMACÉUTICA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CONCENTRACIÓN";
                        texto = Convert.ToString(item["CONCENTRACIÓN"]);
                        if (texto.Length <= 500)
                        {
                            obj.concentracion = Convert.ToString(item["CONCENTRACIÓN"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PRESENTACIÓN";
                        texto = Convert.ToString(item["PRESENTACIÓN"]);
                        if (texto.Length <= 500)
                        {
                            obj.presentacion = Convert.ToString(item["PRESENTACIÓN"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                        texto = Convert.ToString(item["DESCRIPCIÓN COMPLETA DEL PRODUCTO"]);
                        if (texto.Length <= 500)
                        {
                            obj.descripcion_producto = Convert.ToString(item["DESCRIPCIÓN COMPLETA DEL PRODUCTO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                        texto = Convert.ToString(item["NOMBRE COMERCIAL DEL MEDICAMENTO"]);
                        if (texto.Length <= 500)
                        {
                            obj.nom_comercial_medicamento = Convert.ToString(item["NOMBRE COMERCIAL DEL MEDICAMENTO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "¿COMERCIAL O GENÉRICO?";
                        texto = Convert.ToString(item["¿COMERCIAL O GENÉRICO?"]);
                        if (texto.Length <= 500)
                        {
                            obj.comercial_o_generico = Convert.ToString(item["¿COMERCIAL O GENÉRICO?"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "ERROR DE PRESCRIPCIÓN";
                        texto = Convert.ToString(item["ERROR DE PRESCRIPCIÓN"]);
                        if (texto.Length <= 500)
                        {
                            obj.error_prescripcion = Convert.ToString(item["ERROR DE PRESCRIPCIÓN"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LABORATORIO FABRICANTE";
                        texto = Convert.ToString(item["LABORATORIO FABRICANTE"]);

                        if (texto.Length <= 500)
                        {
                            obj.laboratorio_fabricante = Convert.ToString(item["LABORATORIO FABRICANTE"]);
                        }

                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR";
                        texto = Convert.ToString(item["LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR"]);
                        if (texto.Length <= 500)
                        {
                            obj.laboratorio_distribuidor = Convert.ToString(item["LABORATORIO COMERCIALIZADOR  O DISTRIBUIDOR"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                        texto = Convert.ToString(item["LABORATORIO TITULAR DEL REGISTRO SANITARIO"]);
                        if (texto.Length <= 500)
                        {
                            obj.laboratorio_regis_sanitario = Convert.ToString(item["LABORATORIO TITULAR DEL REGISTRO SANITARIO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE DESPACHO DE LA FÓRMULA";
                        try
                        {
                            fecha = Convert.ToDateTime(item["FECHA DE DESPACHO DE LA FÓRMULA"]);
                            if (fecha != null)
                            {
                                obj.fecha_despacho_formula = Convert.ToDateTime(item["FECHA DE DESPACHO DE LA FÓRMULA"]);
                            }
                            else
                            {
                                obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "UNIDAD MÍNIMA DE ENTREGA";
                        texto = Convert.ToString(item["UNIDAD MÍNIMA DE ENTREGA"]);
                        if (texto.Length <= 100)
                        {
                            obj.unidad_medica_entrega = Convert.ToString(item["UNIDAD MÍNIMA DE ENTREGA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE UNIDADES PRESCRITAS";
                        try
                        {
                            decimales = Convert.ToDecimal(item["NÚMERO DE UNIDADES PRESCRITAS"]);
                            if (decimales != null)
                            {
                                obj.numero_unidades_prescritas = Convert.ToDecimal(item["NÚMERO DE UNIDADES PRESCRITAS"]);
                            }
                            else
                            {
                                obj.numero_unidades_prescritas = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "NÚMERO DE UNIDADES ENTREGADAS";
                        try
                        {
                            decimales = Convert.ToDecimal(item["NÚMERO DE UNIDADES ENTREGADAS"]);
                            if (decimales != null)
                            {
                                obj.numero_unidades_entregadas = Convert.ToDecimal(item["NÚMERO DE UNIDADES ENTREGADAS"]);
                            }
                            else
                            {
                                obj.numero_unidades_entregadas = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                        try
                        {
                            decimales = Convert.ToDecimal(item["VALOR UNITARIO DE LA UNIDAD ENTREGADA"]);
                            if (decimales != null)
                            {
                                obj.valor_unitario = (double?)Convert.ToDecimal(item["VALOR UNITARIO DE LA UNIDAD ENTREGADA"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "IVA";
                        try
                        {
                            doble = (double)Convert.ToDecimal(item["IVA"]);
                            if (doble != null)
                            {
                                obj.iva = (double)Convert.ToDecimal(item["IVA"]);
                            }
                            else
                            {
                                obj.iva = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "VALOR TOTAL DE LA ENTREGA";
                        try
                        {
                            doble = (double)Convert.ToDecimal(item["VALOR TOTAL DE LA ENTREGA"]);
                            if (doble != null)
                            {
                                obj.valor_total_entrega = (double?)Convert.ToDecimal(item["VALOR TOTAL DE LA ENTREGA"]);
                            }
                            else
                            {
                                obj.valor_total_entrega = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA";
                        try
                        {
                            texto = Convert.ToString(item["CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA"]);
                            if (texto.Length <= 500)
                            {
                                obj.consecutivo_tiquete = Convert.ToString(item["CONSECUTIVO TIQUETE DE MÁQUINA REGISTADORA Ó FACTURA ELECTRÓNICA"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 500 caracteres.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "PRODUCTO ABE S/N";
                        texto = Convert.ToString(item["PRODUCTO ABE S/N"]);
                        if (texto.Length <= 500)
                        {
                            obj.producto_ABE = Convert.ToString(item["PRODUCTO ABE S/N"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MEDICAMENTO REGULADO S/N";
                        texto = Convert.ToString(item["MEDICAMENTO REGULADO S/N"]);
                        if (texto.Length <= 500)
                        {
                            obj.medicamento_regulado = Convert.ToString(item["MEDICAMENTO REGULADO S/N"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO";
                        texto = Convert.ToString(item["CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO"]);
                        if (texto.Length <= 500)
                        {
                            obj.clasif_producto_gest_riesgo = Convert.ToString(item["CLASIFICACIÓN PRODUCTO GESTIÓN DE RIESGO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)";
                        texto = Convert.ToString(item["CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)"]);
                        if (texto.Length <= 500)
                        {
                            obj.centro_dispensacion = Convert.ToString(item["CENTRO DE DISPENSACIÓN (FARMACIA O DROGUERÍA)"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AMBITO";
                        texto = Convert.ToString(item["AMBITO"]);
                        if (texto.Length <= 500)
                        {
                            obj.ambito = Convert.ToString(item["AMBITO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MODALIDAD DE ENTREGA";
                        texto = Convert.ToString(item["MODALIDAD DE ENTREGA"]);
                        if (texto.Length <= 500)
                        {
                            obj.modalidad_entrega = Convert.ToString(item["MODALIDAD DE ENTREGA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DOSIS DIARIA DEFINIDA";
                        try
                        {
                            decimales = Convert.ToDecimal(item["DOSIS DIARIA DEFINIDA"]);
                            if (decimales != null)
                            {
                                obj.dosis_diaria_definida = Convert.ToDecimal(item["DOSIS DIARIA DEFINIDA"]);
                            }
                            else
                            {
                                obj.dosis_diaria_definida = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "DOSIS";
                        try
                        {
                            decimales = Convert.ToDecimal(item["DOSIS"]);
                            if (decimales != null)
                            {
                                obj.dosis = Convert.ToDecimal(item["DOSIS"]);
                            }
                            else
                            {
                                obj.dosis = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "DIAG P";
                        texto = Convert.ToString(item["DIAG P"]);
                        if (texto.Length <= 500)
                        {
                            obj.diag_p = Convert.ToString(item["DIAG P"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "DIAG S";
                        texto = Convert.ToString(item["DIAG S"]);
                        if (texto.Length <= 500)
                        {
                            obj.diag_s = Convert.ToString(item["DIAG S"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AÑO DE REPORTE";
                        try
                        {
                            entero = Convert.ToInt32(item["AÑO DE REPORTE"]);
                            if (entero != null)
                            {
                                obj.año_reporte = Convert.ToInt32(item["AÑO DE REPORTE"]);
                            }
                            else
                            {
                                obj.año_reporte = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "MES DE REPORTE";
                        texto = Convert.ToString(item["MES DE REPORTE"]);
                        if (texto.Length <= 500)
                        {
                            obj.mes_reporte = Convert.ToString(item["MES DE REPORTE"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CODIGO FAMILIAR";
                        try
                        {
                            entero = Convert.ToInt32(item["CODIGO FAMILIAR"]);
                            if (entero != null)
                            {
                                obj.codigo_familiar = Convert.ToInt32(item["CODIGO FAMILIAR"]);
                            }
                            else
                            {
                                obj.codigo_familiar = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO DE SALUD";
                        texto = Convert.ToString(item["TIPO DE SALUD"]);
                        if (texto.Length <= 500)
                        {
                            obj.tipo_salud = Convert.ToString(item["TIPO DE SALUD"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION";
                        texto = Convert.ToString(item["MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION"]);
                        if (texto.Length <= 500)
                        {
                            obj.medico_general_asignado_periodoDispensacion = Convert.ToString(item["MEDICO GENERAL ASIGNADO EN EL PERIODO DE DISPENSACION"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "EDAD";
                        try
                        {
                            entero = Convert.ToInt32(item["EDAD"]);
                            if (entero != null)
                            {
                                obj.edad = Convert.ToInt32(item["EDAD"]);
                            }
                            else
                            {
                                obj.edad = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "GRUPO ETAREO";
                        texto = Convert.ToString(item["GRUPO ETAREO"]);
                        if (texto.Length <= 500)
                        {
                            obj.grupo_etareo = Convert.ToString(item["GRUPO ETAREO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "GENERO";
                        texto = Convert.ToString(item["GENERO"]);
                        if (texto.Length <= 500)
                        {
                            obj.genero = Convert.ToString(item["GENERO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DE DESPACHO HOMOLOGADA";
                        texto = Convert.ToString(item["CIUDAD DE DESPACHO HOMOLOGADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.ciudad_despacho_homologada = Convert.ToString(item["CIUDAD DE DESPACHO HOMOLOGADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LOCALIDAD DE DESPACHO HOMOLOGADA";
                        texto = Convert.ToString(item["LOCALIDAD DE DESPACHO HOMOLOGADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.localidad_despacho_homologada = Convert.ToString(item["LOCALIDAD DE DESPACHO HOMOLOGADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL DE DESPACHO HOMOLOGADA";
                        texto = Convert.ToString(item["REGIONAL DE DESPACHO HOMOLOGADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.regional_despacho_homologada = Convert.ToString(item["REGIONAL DE DESPACHO HOMOLOGADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "CIUDAD DEL BENEFICIARIO VALIDADA";
                        texto = Convert.ToString(item["CIUDAD DEL BENEFICIARIO VALIDADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.ciudad_beneficiario_oValidada = Convert.ToString(item["CIUDAD DEL BENEFICIARIO VALIDADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "LOCALIDAD DE BENEFICIARIO VALIDADA";
                        texto = Convert.ToString(item["LOCALIDAD DE BENEFICIARIO VALIDADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.localidad_beneficiario_oValidada = Convert.ToString(item["LOCALIDAD DE BENEFICIARIO VALIDADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL DE BENEFECIARIO VALIDADA";
                        texto = Convert.ToString(item["REGIONAL DE BENEFECIARIO VALIDADA"]);
                        if (texto.Length <= 500)
                        {
                            obj.regional_beneficiario_oValidada = Convert.ToString(item["REGIONAL DE BENEFECIARIO VALIDADA"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PRESTADOR";
                        texto = Convert.ToString(item["PRESTADOR"]);
                        if (texto.Length <= 500)
                        {
                            obj.prestador = Convert.ToString(item["PRESTADOR"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "GRUPO DE RIESGO";
                        texto = Convert.ToString(item["GRUPO DE RIESGO"]);
                        if (texto.Length <= 500)
                        {
                            obj.grupo_riesgo = Convert.ToString(item["GRUPO DE RIESGO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MEDICAMENTO ALTO COSTO";
                        texto = Convert.ToString(item["MEDICAMENTO ALTO COSTO"]);
                        if (texto.Length <= 500)
                        {
                            obj.medicamento_alto_costo = Convert.ToString(item["MEDICAMENTO ALTO COSTO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA FALLECIMIENTO";
                        try
                        {
                            fecha = Convert.ToDateTime(item["FECHA FALLECIMIENTO"]);
                            if (fecha != null)
                            {
                                obj.fecha_fallecimiento = Convert.ToDateTime(item["FECHA FALLECIMIENTO"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "ALERTA FALLECIDO";
                        try
                        {
                            entero = Convert.ToInt32(item["ALERTA FALLECIDO"]);
                            if (entero != null)
                            {
                                obj.alerta_fallecido = Convert.ToInt32(item["ALERTA FALLECIDO"]);
                            }
                            else
                            {
                                obj.alerta_fallecido = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "HOMOLOGACION ATC";
                        texto = Convert.ToString(item["HOMOLOGACION ATC"]);
                        if (texto.Length <= 500)
                        {
                            obj.homologacion_atc = Convert.ToString(item["HOMOLOGACION ATC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "INDICADOR ALTO COSTO";
                        texto = Convert.ToString(item["INDICADOR ALTO COSTO"]);
                        if (texto.Length <= 200)
                        {
                            obj.indicador_altoCosto = Convert.ToString(item["INDICADOR ALTO COSTO"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = Convert.ToString(SesionVar.UserName);

                        OBJDetalle.Add(obj);
                        obj = new medicamentos_dispen_calidad();
                        IntContador = IntContador + 1;

                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarDispensacionMedicamentosCalidad(OBJDetalle, id_cargue, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<medicamentos_dispen_calidad>();
                            ciclos++;
                        }
                    }
                    //else
                    //{
                    //    textError = "Fila: " + IntContador.ToString() + " Columna:" + columna;
                    //    throw new Exception(textError);
                    //}
                }


                logMas.registros_Cargados = OBJDetalle.Count();

                try
                {
                    resultado = BusClass.InsertarDispensacionMedicamentosCalidad(OBJDetalle, id_cargue, ref MsgRes);
                    mensajeLog = "Cargue completado";
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    mensajeLog = "Error en el cargue";

                    BusClass.EliminarCargueDispendtll(id_cargue, ref MsgRes);

                    var error = ex.Message;
                }

                logMas.estado_cargue = mensajeLog;
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                return resultado;
            }
            catch (Exception ex)
            {
                BusClass.EliminarCargueDispen(id_cargue, ref MsgRes);

                BusClass.EliminarCargueDispendtll(id_cargue, ref MsgRes);

                if (textError != "" && textError != null)
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + textError;
                }
                else
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + columna;
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.CodeError = ex.Message;

                logMas.estado_cargue = mensajeLog;
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                return 0;
            }
        }

        public List<management_prestadores_regionalResult> GetPrestadoresRegional(int idRegional)
        {
            return BusClass.GetPrestadoresRegional(idRegional);
        }


        private List<managmentprestadoresfacturasaceptadasResult> _Lista;
        public List<managmentprestadoresfacturasaceptadasResult> Lista
        {
            get
            {
                if (_Lista == null)
                {
                    return _Lista = BusClass.GetFacturasAceptadas(2, SesionVar.IDUser, ref MsgRes);
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

        public List<vw_factura_digital_gasto_total> GetGatosFactura(int id)
        {
            return BusClass.GetGatosFactura(id);
        }
        public List<managementprestadores_alertas_activasResult> GetConsultaAlertasactivas()
        {
            return BusClass.GetConsultaAlertasactivas();
        }

        public List<managmentprestadoresfacturasgestionadasdtllResult> GetListFacturasByNumFactura(string numfactura)
        {
            return BusClass.GetListFacturasByNumFactura(numfactura);
        }

        public List<managmentprestadoresfacturasgestionadasdtllCompletaResult> GetListFacturasByNumFacturaCompleta(String numFac, String nit, String prestador, String sap, int? idFactura)
        {
            return BusClass.GetListFacturasByNumFacturaCompleta(numFac, nit, prestador, sap, idFactura);

        }
        public void IngresoDocsSoportes(List<Models.CuentasMedicas.SoportesClinicos> Soportes, string connectionString, ref MessageResponseOBJ MsgRes)
        {
            if (Soportes.Count > 0)
            {
                foreach (var item in Soportes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = connection.CreateCommand())
                        {

                            command.CommandText = "INSERT INTO recep_facturas_soportes_clinicos(id_cargue_base,id_cargue_dtll,num_factura,nom_documento,ruta,fecha_ingreso,proyecto_cargado,usuario_ingreso)values(@id_cargue_base,@id_cargue_dtll,@num_factura,@nom_documento,@ruta,@fecha_ingreso,@proyecto_cargado,@usuario_ingreso)";
                            command.Parameters.AddWithValue("@id_cargue_base", item.id_cargue_base);
                            command.Parameters.AddWithValue("@id_cargue_dtll", item.id_cargue_dtll);
                            command.Parameters.AddWithValue("@num_factura", item.num_factura);
                            command.Parameters.AddWithValue("@nom_documento", item.nom_documento);
                            command.Parameters.AddWithValue("@ruta", item.ruta);
                            command.Parameters.AddWithValue("@fecha_ingreso", Convert.ToDateTime(DateTime.Now));
                            command.Parameters.AddWithValue("@proyecto_cargado", "Ecopetrol");
                            command.Parameters.AddWithValue("@usuario_ingreso", item.usuario);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();

                            MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                        MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    }
                }
            }
        }

        public ManagementPrestadoresFacturasByIdDtllResult GetInfoFacturaById(int idcarguedtll)
        {
            return BusClass.GetInfoFacturaById(idcarguedtll);
        }

        //Codigo facturas rechazadas
        public List<managmentprestadoresFacturasRechazadasResult> GetFacturasRechazadas(string factura, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasRechazadas(factura, ref MsgRes);
        }


        public List<managmentprestadoresFacturas_analistasResult> prestadoresFacturas_analistas()
        {
            return BusClass.prestadoresFacturas_analistas();
        }

        public List<managmentprestadoresFacturas_auditoresResult> prestadoresFacturas_auditores()
        {
            return BusClass.prestadoresFacturas_auditores();
        }

        public Int32 InsertarGestionAnalista(ref_cuentas_medicas_analista OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionAnalista(OBJ, ref MsgRes);
        }
        public Int32 InsertarGestionAuditor(ref_cuentas_medicas_auditores OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGestionAuditor(OBJ, ref MsgRes);
        }

        public void ActualizaAnalistaAsignado(ref_cuentas_medicas_analista ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaAnalistaAsignado(ObjAudi, ref MsgRes);
        }

        public void ActualizaAuditorAsignado(ref_cuentas_medicas_auditores ObjAudi, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaAuditorAsignado(ObjAudi, ref MsgRes);
        }

        public void ActualizarFacturaAceptadaAnalista(List<int> ListadoFacturas, int idUsuario, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    foreach (int idCargueDtll in ListadoFacturas)
                    {
                        int insercion = db.Management_aceptar_factura(idCargueDtll, idUsuario);
                    }

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Facturas aceptadas correctamente";

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ah ocurrido un error, comuniquese con el administrador";
            }
        }
        public List<management_facturas_sinDocumentacionResult> ListaFacturasIncompletas()
        {
            return BusClass.ListaFacturasIncompletas();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 17-nov-2022
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarLogBusquedaTableros(log_busquedas_tableros_facturas obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarLogBusquedaTableros(obj, ref MsgRes);
        }

        public List<int> ExcelMasivoAlertasDispe(DataTable dt2, alertas_dispensacion ale, ref MessageResponseOBJ MsgRes)
        {
            List<alertas_dispensacion_registros> Listado = new List<alertas_dispensacion_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<int> listaRespuesta = new List<int>();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        alertas_dispensacion_registros obj = new alertas_dispensacion_registros();

                        fila++;
                        if (!string.IsNullOrEmpty(item["DEPENDENCIA DE SALUD"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();
                            //if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))

                            columna = "DEPENDENCIA DE SALUD";
                            texto = Convert.ToString(item["DEPENDENCIA DE SALUD"]);
                            if (texto.Length <= 50)
                            {
                                obj.dependencia_salid = Convert.ToString(item["DEPENDENCIA DE SALUD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "FECHA DE PRESCRIPCIÓN FÓRMULA";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA DE PRESCRIPCIÓN FÓRMULA"]);
                                if (fechas != null)
                                {
                                    obj.fecha_prescripcion_formula = Convert.ToDateTime(item["FECHA DE PRESCRIPCIÓN FÓRMULA"]);

                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("ser mayor a la fecha actual"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }

                                throw new Exception(textError);
                            }

                            columna = "NÚMERO DE FÓRMULA";
                            texto = Convert.ToString(item["NÚMERO DE FÓRMULA"]);
                            if (texto.Length <= 50)
                            {
                                obj.numero_formula = Convert.ToString(item["NÚMERO DE FÓRMULA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "TIPO DE FORMULA";
                            texto = Convert.ToString(item["TIPO DE FORMULA"]);
                            if (texto.Length <= 50)
                            {
                                obj.tipo_formula = Convert.ToString(item["TIPO DE FORMULA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                            texto = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                            if (texto.Length <= 50)
                            {
                                obj.tipo_identificacion_beneficiario = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                            texto = Convert.ToString(item["NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                            if (texto.Length <= 50)
                            {
                                obj.numero_documento_beneficiario = Convert.ToString(item["NÚMERO DE DOCUMENTO DE IDENTIFICACIÓN DEL BENEFICIARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "NOMBRE DEL BENEFICIARIO";
                            texto = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_beneficiario = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "CIUDAD DE DESPACHO";
                            texto = Convert.ToString(item["CIUDAD DE DESPACHO"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad_despacho = Convert.ToString(item["CIUDAD DE DESPACHO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }



                            columna = "UNIS";
                            texto = Convert.ToString(item["UNIS"]);
                            if (texto.Length <= 50)
                            {
                                obj.unis = Convert.ToString(item["UNIS"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "ID IPS";
                            texto = Convert.ToString(item["ID IPS"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_ips = Convert.ToString(item["ID IPS"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "NOMPRE IPS DE ATENCIÓN";
                            texto = Convert.ToString(item["NOMPRE IPS DE ATENCIÓN"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_ips_atencion = Convert.ToString(item["NOMPRE IPS DE ATENCIÓN"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "ID PRESCRIPTOR";
                            texto = Convert.ToString(item["ID PRESCRIPTOR"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_prescriptor = Convert.ToString(item["ID PRESCRIPTOR"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "PRESCRIPTOR";
                            texto = Convert.ToString(item["PRESCRIPTOR"]);
                            if (texto.Length <= 200)
                            {
                                obj.prescriptor = Convert.ToString(item["PRESCRIPTOR"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "ESPECIALIDAD";
                            texto = Convert.ToString(item["ESPECIALIDAD"]);
                            if (texto.Length <= 200)
                            {
                                obj.especialidad = Convert.ToString(item["ESPECIALIDAD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "CODIFICACIÓN EAN 13";
                            texto = Convert.ToString(item["CODIFICACIÓN EAN 13"]);
                            if (texto.Length <= 200)
                            {
                                obj.codificacion_ean13 = Convert.ToString(item["CODIFICACIÓN EAN 13"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }



                            columna = "CÓDIGO HIJO";
                            texto = Convert.ToString(item["CÓDIGO HIJO"]);
                            if (texto.Length <= 50)
                            {
                                obj.codigo_hijo = Convert.ToString(item["CÓDIGO HIJO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CÓDIGO INTERNO DE MEDICAMENTO";
                            texto = Convert.ToString(item["CÓDIGO INTERNO DE MEDICAMENTO"]);
                            if (texto.Length <= 50)
                            {
                                obj.codigo_interno_medicamento = Convert.ToString(item["CÓDIGO INTERNO DE MEDICAMENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CÓDIGO COMERCIAL";
                            texto = Convert.ToString(item["CÓDIGO COMERCIAL"]);
                            if (texto.Length <= 50)
                            {
                                obj.codigo_comercial = Convert.ToString(item["CÓDIGO COMERCIAL"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "CUM";
                            texto = Convert.ToString(item["CUM"]);
                            if (texto.Length <= 50)
                            {
                                obj.cum = Convert.ToString(item["CUM"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "REGISTRO SANITARIO";
                            texto = Convert.ToString(item["REGISTRO SANITARIO"]);
                            if (texto.Length <= 200)
                            {
                                obj.registro_sanitario = Convert.ToString(item["REGISTRO SANITARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }





                            columna = "CLASIFICACIÓN INVIMA";
                            texto = Convert.ToString(item["CLASIFICACIÓN INVIMA"]);
                            if (texto.Length <= 100)
                            {
                                obj.clasificacion_invima = Convert.ToString(item["CLASIFICACIÓN INVIMA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CÓDIGO ATC";
                            texto = Convert.ToString(item["CÓDIGO ATC"]);
                            if (texto.Length <= 100)
                            {
                                obj.codigo_atc = Convert.ToString(item["CÓDIGO ATC"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "GRUPO FARMACOLOGICO";
                            texto = Convert.ToString(item["GRUPO FARMACOLOGICO"]);
                            if (texto.Length <= 100)
                            {
                                obj.grupo_farmacologico = Convert.ToString(item["GRUPO FARMACOLOGICO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NOMBRE DEL MEDICAMENTO EN D C I";
                            texto = Convert.ToString(item["NOMBRE DEL MEDICAMENTO EN D C I"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_medicamento_DCI = Convert.ToString(item["NOMBRE DEL MEDICAMENTO EN D C I"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "FORMA FARMACÉUTICA";
                            texto = Convert.ToString(item["FORMA FARMACÉUTICA"]);
                            if (texto.Length <= 200)
                            {
                                obj.formula_farmaceutica = Convert.ToString(item["FORMA FARMACÉUTICA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CONCENTRACIÓN";
                            texto = Convert.ToString(item["CONCENTRACIÓN"]);
                            if (texto.Length <= 50)
                            {
                                obj.concentracion = Convert.ToString(item["CONCENTRACIÓN"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "PRESENTACIÓN";
                            texto = Convert.ToString(item["PRESENTACIÓN"]);
                            if (texto.Length <= 1000)
                            {
                                obj.presentacion = Convert.ToString(item["PRESENTACIÓN"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 1000 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "DESCRIPCIÓN COMPLETA DEL PRODUCTO";
                            texto = Convert.ToString(item["DESCRIPCIÓN COMPLETA DEL PRODUCTO"]);
                            if (texto.Length <= 300)
                            {
                                obj.descripcion_completa_producto = Convert.ToString(item["DESCRIPCIÓN COMPLETA DEL PRODUCTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 300 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NOMBRE COMERCIAL DEL MEDICAMENTO";
                            texto = Convert.ToString(item["NOMBRE COMERCIAL DEL MEDICAMENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_comercial_medicamento = Convert.ToString(item["NOMBRE COMERCIAL DEL MEDICAMENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "COMERCIAL O GENÉRICO";
                            texto = Convert.ToString(item["COMERCIAL O GENÉRICO"]);
                            if (texto.Length <= 200)
                            {
                                obj.comercial_o_generico = Convert.ToString(item["COMERCIAL O GENÉRICO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "LABORATORIO FABRICANTE";
                            texto = Convert.ToString(item["LABORATORIO FABRICANTE"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_fabricante = Convert.ToString(item["LABORATORIO FABRICANTE"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "LABORATORIO COMERCIALIZADOR O DISTRIBUIDOR";
                            texto = Convert.ToString(item["LABORATORIO COMERCIALIZADOR O DISTRIBUIDOR"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_comercializador_distribuidor = Convert.ToString(item["LABORATORIO COMERCIALIZADOR O DISTRIBUIDOR"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "LABORATORIO TITULAR DEL REGISTRO SANITARIO";
                            texto = Convert.ToString(item["LABORATORIO TITULAR DEL REGISTRO SANITARIO"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_titulo_registro_sanitario = Convert.ToString(item["LABORATORIO TITULAR DEL REGISTRO SANITARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "DOCUMENTO FECHA DISPENSACION";
                            try
                            {
                                fechas = Convert.ToDateTime(item["DOCUMENTO FECHA DISPENSACION"]);
                                if (fechas != null)
                                {
                                    obj.documento_fecha_dispensacion = Convert.ToDateTime(item["DOCUMENTO FECHA DISPENSACION"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("no puede ir vacio"))
                                {
                                    textError = ex.Message;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                }

                                throw new Exception(textError);
                            }


                            columna = "NÚMERO DE UNIDADES PRESCRITAS";
                            texto = Convert.ToString(item["NÚMERO DE UNIDADES PRESCRITAS"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.numero_unidades_prescritas = Convert.ToDecimal(item["NÚMERO DE UNIDADES PRESCRITAS"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "NÚMERO DE UNIDADES ENTREGADAS";
                            texto = Convert.ToString(item["NÚMERO DE UNIDADES ENTREGADAS"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.numero_unidades_entregadas = Convert.ToDecimal(item["NÚMERO DE UNIDADES ENTREGADAS"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "VALOR UNITARIO DE LA UNIDAD ENTREGADA";
                            texto = Convert.ToString(item["VALOR UNITARIO DE LA UNIDAD ENTREGADA"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.valor_unitario_unidad_entregada = Convert.ToDecimal(item["VALOR UNITARIO DE LA UNIDAD ENTREGADA"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "IVA";
                            texto = Convert.ToString(item["IVA"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.iva = Convert.ToDecimal(item["NÚMERO DE UNIDADES PRESCRITAS"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "VALOR";
                            texto = Convert.ToString(item["VALOR"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.valor = Convert.ToDecimal(item["VALOR"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }




                            columna = "CONSECUTIVO TIQUETE";
                            texto = Convert.ToString(item["CONSECUTIVO TIQUETE"]);
                            if (texto.Length <= 50)
                            {
                                obj.concutivo_tiquete = Convert.ToString(item["CONSECUTIVO TIQUETE"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "PRESTADOR";
                            texto = Convert.ToString(item["PRESTADOR"]);
                            if (texto.Length <= 200)
                            {
                                obj.prestador = Convert.ToString(item["PRESTADOR"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "FECHA ALERTA";
                            texto = Convert.ToString(item["FECHA ALERTA"]);
                            if (texto.Length <= 50)
                            {
                                obj.fecha_alerta = Convert.ToString(item["FECHA ALERTA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CONSECUTIVO";
                            texto = Convert.ToString(item["CONSECUTIVO"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.consecutivo = Convert.ToDecimal(item["CONSECUTIVO"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 18 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "NUMERO DE ALERTA";
                            texto = Convert.ToString(item["NUMERO DE ALERTA"]);
                            if (texto.Length <= 50)
                            {
                                obj.numero_alerta = Convert.ToString(item["NUMERO DE ALERTA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            Listado.Add(obj);
                            obj = new alertas_dispensacion_registros();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.CargueAlertasDispensacion(ale, Listado, ref MsgRes);
                        listaRespuesta.Add(1);
                        listaRespuesta.Add(RtaInsercion);
                        listaRespuesta.Add(Listado.Count());

                        return listaRespuesta;
                    }
                    else
                    {
                        var mensaje = "";
                        mensaje = "Hoja vacía.";
                        MsgRes.DescriptionResponse = mensaje;
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                        listaRespuesta.Add(0);
                        listaRespuesta.Add(RtaInsercion);
                        listaRespuesta.Add(Listado.Count());

                        return listaRespuesta;
                    }
                }

                catch (Exception ex)
                {
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                    if (textError != "" && textError != null)
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
                    }
                    else
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                    }
                    MsgRes.CodeError = ex.Message;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                var mensaje = "";

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
                    MsgRes.DescriptionResponse = mensaje;
                }
                else
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

            listaRespuesta.Add(0);
            listaRespuesta.Add(RtaInsercion);
            listaRespuesta.Add(Listado.Count());

            return listaRespuesta;
        }

        public List<management_fis_rips_ParaActualizar_tigasResult> ValidacionDatosCUPSTigas(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {
            List<management_fis_rips_ParaActualizar_tigasResult> Listado = new List<management_fis_rips_ParaActualizar_tigasResult>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            try
            {
                string columna = "";
                int fila = 1;

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        management_fis_rips_ParaActualizar_tigasResult obj = new management_fis_rips_ParaActualizar_tigasResult();

                        fila++;
                        if (!string.IsNullOrEmpty(item["Id Registro"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();
                            //if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))

                            columna = "Id Registro";
                            try
                            {
                                texto = Convert.ToString(item["Id Registro"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_registro = Convert.ToInt32(item["Id Registro"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Id Factura FIS";
                            try
                            {
                                texto = Convert.ToString(item["Id Factura FIS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.idFacturaFis = Convert.ToInt32(item["Id Factura FIS"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }
                            columna = "Id Factura";
                            try
                            {
                                texto = Convert.ToString(item["Id Factura"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_factura = Convert.ToInt32(item["Id Factura"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Numero de Factura";
                            try
                            {
                                texto = Convert.ToString(item["Numero de Factura"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.num_factura = Convert.ToString(item["Numero de Factura"]);
                                }
                       
                            }

                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Fecha de Recepción";
                            try
                            {
                                texto = Convert.ToString(item["Fecha de Recepción"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.fecha_recepcion_fac = Convert.ToDateTime(item["Fecha de Recepción"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Código Prestador";
                            try
                            {
                                texto = Convert.ToString(item["Código Prestador"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_prestador = Convert.ToString(item["Código Prestador"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Id Transacción";
                            try
                            {
                                texto = Convert.ToString(item["Id Transacción"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_transaccion = Convert.ToInt32(item["Id Transacción"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Id Usuario";
                            try
                            {
                                texto = Convert.ToString(item["Id Usuario"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_usuario = Convert.ToInt32(item["Id Usuario"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }

                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Num Documento Identificación";
                            try
                            {
                                texto = Convert.ToString(item["Num Documento Identificación"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numDocumentoIdentificacion = Convert.ToString(item["Num Documento Identificación"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Nombre Usuario";
                            try
                            {
                                texto = Convert.ToString(item["Nombre Usuario"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.nombreUsuario = Convert.ToString(item["Nombre Usuario"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Código CUPS";
                            try
                            {
                                texto = Convert.ToString(item["Código CUPS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.cod_cups = Convert.ToString(item["Código CUPS"]);
                                }
                            }

                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Descripción CUPS";
                            try
                            {
                                texto = Convert.ToString(item["Descripción CUPS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.descripcion_cuvs = Convert.ToString(item["Descripción CUPS"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Conteo CUPS";
                            try
                            {
                                texto = Convert.ToString(item["Conteo CUPS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.conteo_cups = Convert.ToInt32(item["Conteo CUPS"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Valor CUPS";
                            try
                            {
                                texto = Convert.ToString(item["Valor CUPS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_cups = Convert.ToDecimal(item["Valor CUPS"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "Valor Individual";
                            try
                            {
                                texto = Convert.ToString(item["Valor Individual"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_individual = Convert.ToDecimal(item["Valor Individual"]);
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }
                            }

                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }


                            columna = "Código CUV";
                            try
                            {
                                texto = Convert.ToString(item["Código CUV"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.cod_cuv = Convert.ToString(item["Código CUV"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception(error + " formato incorrecto");
                                }
                            }

                            columna = "TIGA A CORREGIR";
                            try
                            {
                                texto = Convert.ToString(item["TIGA A CORREGIR"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_tiga = Convert.ToString(item["TIGA A CORREGIR"]);

                                    ref_tigas_detallados tiga = BusClass.TraerTigasDetallados(obj.codigo_tiga);
                                    if (tiga == null)
                                    {
                                        throw new Exception(" No existe este TIGA");
                                    }
                                    else
                                    {
                                        obj.descripcion_tiga = tiga.descripcion_tiga_detallado;
                                    }
                                }
                                else
                                {
                                    throw new Exception("No puede venir sin datos");
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("No puede venir sin datos") || error.Contains("No existe"))
                                {
                                    throw new Exception(error);
                                }
                                else
                                {
                                    throw new Exception("formato incorrecto");
                                }
                            }

                            columna = "TIGA INTEGRAL";
                            try
                            {
                                texto = Convert.ToString(item["TIGA INTEGRAL"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.actualizaTigaintegral = Convert.ToString(item["TIGA INTEGRAL"]);
                                    if (!(obj.actualizaTigaintegral.Trim().ToUpper() == "SI" || obj.actualizaTigaintegral.Trim().ToUpper() == "NO"))
                                    {
                                        throw new Exception("Colocar una opción válida (SI/NO).");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            Listado.Add(obj);
                            obj = new management_fis_rips_ParaActualizar_tigasResult();
                        }
                    }

                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;

                }

                catch (Exception ex)
                {
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    MsgRes.CodeError = ex.Message;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                var mensaje = "";

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
                    MsgRes.DescriptionResponse = mensaje;
                }
                else
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

            return Listado;
        }

        #endregion

    }
}