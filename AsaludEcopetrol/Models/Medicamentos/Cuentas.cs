using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Helpers;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;



namespace AsaludEcopetrol.Models.Medicamentos
{
    public class Cuentas
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

        private md_cargue_cuentas _OBJCargueCuentas;
        public md_cargue_cuentas OBJCargueCuentas
        {
            get
            {
                if (_OBJCargueCuentas == null)
                {
                    return _OBJCargueCuentas = new md_cargue_cuentas();
                }
                else
                {
                    return _OBJCargueCuentas;
                }

            }

            set
            {
                _OBJCargueCuentas = value;
            }
        }

        private md_cargue_cuentas_detalle _OBJCargueCuentasDetalle;
        public md_cargue_cuentas_detalle OBJCargueCuentasDetalle
        {
            get
            {
                if (_OBJCargueCuentasDetalle == null)
                {
                    return _OBJCargueCuentasDetalle = new md_cargue_cuentas_detalle();
                }
                else
                {
                    return _OBJCargueCuentasDetalle;
                }

            }

            set
            {
                _OBJCargueCuentasDetalle = value;
            }
        }

        private List<ManagmentocargueconsolidadoResult> _ListReporte;
        public List<ManagmentocargueconsolidadoResult> ListReporte
        {
            get
            {
                if (_ListReporte == null)
                {
                    return _ListReporte = new List<ManagmentocargueconsolidadoResult>();
                }
                else
                {
                    return _ListReporte;
                }

            }

            set
            {
                _ListReporte = value;
            }
        }

        private List<md_Ref_consultas> _ListMDconsulta;
        public List<md_Ref_consultas> ListMDconsulta
        {
            get
            {
                if (_ListMDconsulta == null)
                {
                    return _ListMDconsulta = BusClass.GetRefConsulta();
                }
                else
                {
                    return _ListMDconsulta;
                }

            }

            set
            {
                _ListMDconsulta = value;
            }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();


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
        public String matricula_mercantil { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "UNIS")]
        public String unis { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "COORDINACION")]
        public String coordinacion { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO")]
        public Decimal resultado { get; set; }

        [Display(Name = "SELECCIONE EL ARCHIVO CORRESPONDIENTE")]
        public String cargue_excel { get; set; }

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

        public Int32? id_md_cargue_cuentas { get; set; }

        public String Numero_factura { get; set; }

        public String Numero_formula { get; set; }

        public DateTime fecha_inicial { get; set; }

        public DateTime fecha_final { get; set; }

        public List<ManagmentocargueconsolidadoResult> listam  { get; set; }


        #endregion

        #region FUNCIONES 

        public List<sis_usuario> Usuarios()
        {
            return BusClass.GetSisUsuario();
        }

        public Int32 InsertarCargueCuentasMd(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarCargueCuentasMd(OBJCargueCuentas, ref MsgRes);
        }

        public Int32 InsertarConsolidadoFacturacion(List<md_consolidado_facturacion> OBJDetalle, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarConsolidadoFacturacion(OBJDetalle, ref MsgRes);
        }

        public void CargarBaseMDT(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContador = 0;
            List<md_consolidado_facturacion> OBJDetalle = new List<md_consolidado_facturacion>();
            try
            {
                foreach (DataRow item in dt2.Rows)
                {
                    md_consolidado_facturacion facturas = new md_consolidado_facturacion();

                    if (item["REGIONAL"].ToString() != "")
                    {

                        facturas.regional = Convert.ToString(item["REGIONAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.proveedor = Convert.ToString(item["PROVEEDOR"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.ciudad = Convert.ToString(item["CIUDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.numero_radicado = Convert.ToString(item["N° RAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["FECHA REAL"].ToString() == "")
                        {
                            facturas.fecha_real = null;
                        }
                        else
                        {
                            facturas.fecha_real = Convert.ToDateTime(item["FECHA REAL"]);
                        }
                       
                        facturas.numero_factura = Convert.ToString(item["NUMERO DE FACT"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["FECHA DE FACTURA"].ToString() == "")
                        {
                            facturas.fecha_factura = null;
                        }
                        else
                        {
                            facturas.fecha_factura = Convert.ToDateTime(item["FECHA DE FACTURA"]);
                        }
                        facturas.nit = Convert.ToString(item["NIT"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.tipo_documento = Convert.ToString(item["TIPO DE DOC"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.historia_clinica = Convert.ToString(item["HISTORIA CLINICA_OK"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.doc_beneficiario = Convert.ToString(item["DOC BENEFICIARIO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_beneficiario = Convert.ToString(item["NOMBRE BENEFICIARIO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["EDAD"].ToString() == "")
                        {
                            facturas.edad = null;
                        }
                        else
                        {
                            facturas.edad = Convert.ToInt32(item["EDAD"]);
                        }
                        facturas.nom_medico = Convert.ToString(item["NOMBRE MEDICO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.doc_medico = Convert.ToString(item["DOC MEDICO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_especialidad = Convert.ToString(item["NOMBRE ESPECIALIDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.cod_comercial = Convert.ToString(item["CÓDIGO ECOPETROL (Padre)"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.cod_equivalente = Convert.ToString(item["CÓDIGO HIJO * UNIDAD MINIMA FARMACEUTICA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.producto_en_DCI = Convert.ToString(item["DESCRIPCION CLIENTE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_comercial = Convert.ToString(item["NOMBRE COMERCIAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_laboratorio = Convert.ToString(item["NOMBRE LABORATORIO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["FECHA DE ENTREGA"].ToString() == "")
                        {
                            facturas.fecha_entrega = null;
                        }
                        else
                        {
                            facturas.fecha_entrega = Convert.ToDateTime(item["FECHA DE ENTREGA"]);
                        }
                        facturas.presentacion = Convert.ToString(item["PRESENTACION"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.cat_entregada = Convert.ToString(item["CANT ENTREGADA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (item["VALOR UNITARIO"].ToString() == "")
                        {
                            facturas.valor_unitario = null;
                        }
                        else
                        {
                            facturas.valor_unitario = Convert.ToInt32(item["VALOR UNITARIO"]);
                        }

                        if (item["VALOR TOTAL"].ToString() == "")
                        {
                            facturas.valor_total = null;
                        }
                        else
                        {
                            facturas.valor_total = Convert.ToInt32(item["VALOR TOTAL"]);
                        }
                        facturas.numero_formula = Convert.ToString(item["FÓRMULA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.med_pendiente = Convert.ToString(item["MED PENDIENTE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_nivel_autorizacion = Convert.ToString(item["NOMBRE NIVEL AUTORIZACION"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.diagnostico_cod_base = Convert.ToString(item["DIAGNOSTICO COD CLASE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.cod_atc = Convert.ToString(item["COD ATC"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.cobro_iva = Convert.ToString(item["COBRO DE IVA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.subcuenta = Convert.ToString(item["SUBCUENTA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.dosis = Convert.ToString(item["DOSIS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.expediente = Convert.ToString(item["EXPEDIENTE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.consecutivo = Convert.ToString(item["CONSECUTIVO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.categoria_invima = Convert.ToString(item["CATEGORIA INVIMA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.registro_sanitario = Convert.ToString(item["REGISTRO SANITARIO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.tipo_documento2 = Convert.ToString(item["TIPO DOCUMENTO 2"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.letra_ilegible = Convert.ToString(item["LETRA ILEGIBLE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.enmendadu_tachaduras = Convert.ToString(item["ENMENDADURA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.unidades_diferentes = Convert.ToString(item["DOSIS EN UNIDADES DIFERENTES"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.medicamentos_control_especial = Convert.ToString(item["PRESCRIPCIÓN DE MEDICAMENTOS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_datos_paciente = Convert.ToString(item["SIN DATOS DEL PACIENTE: NOMBRE Y NÚMERO DE IDENTIFICACIÓN"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.nombre_medicamento_en_comercial = Convert.ToString(item["NOMBRE DEL MEDICAMENTO EN COMERCIAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_concentracion = Convert.ToString(item["SIN CONCENTRACION"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_forma_farmaceutica = Convert.ToString(item["SIN FORMA FARMACEUTICA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_via_de_administracion_o_via_incorrecta = Convert.ToString(item["SIN VIA DE ADMINISTRACIÓN O VIA INCORRECTA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_dosis = Convert.ToString(item["SIN DOSIS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_frecuencia_de_adm = Convert.ToString(item["SIN FRECUENCIA DE ADMINISTRACION"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_cantidad_total_tratamiento = Convert.ToString(item["SIN CANTIDAD TOTAL DE TRATAMIENTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.sin_datos_prescriptor = Convert.ToString(item["SIN DATOS DEL PRESCRIPTOR O DATOS INCOMPLETOS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.total_errore_mes = Convert.ToString(item["TOTAL ERRORES MES"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.en_auditor_prescriptor_no_formulas_hechas = Convert.ToString(item["FÓRMULAS HECHAS EN AUDITOR PRESCRIPTOR"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.otros_convenios = Convert.ToString(item["FÓRMULAS HECHAS OTROS CONVENIOS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.en_sistemas_esalud = Convert.ToString(item["FÓRMULAS HECHAS EN SISTEMA E-SALUD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.formulas_hechas_manual = Convert.ToString(item["FÓRMULAS HECHAS MANUAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.total_formulas_evaluadas_mes_medico = Convert.ToString(item["TOTAL FORMULAS EVALUADAS MES POR MÉDICO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.tipo_formula = Convert.ToString(item["TIPO DE FÓRMULA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.accion = Convert.ToString(item["ACCION"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.medicamentos_de_aplicacion_supervisada = Convert.ToString(item["MEDICAMENTOS DE APLICACIÓN SUPERVISADA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.formula_de_trascripcion = Convert.ToString(item["FÓRMULA DE TRASNCRIPCIÓN"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.mes_de_despacho = Convert.ToString(item["MES DE DESPACHO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.especialidad = Convert.ToString(item["ESPECIALIDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.mega_para_adherencia = Convert.ToString(item["MEGA PARA ADHERENCIA?"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.lupe = Convert.ToString(item["LUPE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.dci_lupe = Convert.ToString(item["DCI LUPE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.dci_sin_espacio = Convert.ToString(item["espacios"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        facturas.fecha_cargue = DateTime.Now;

                        OBJDetalle.Add(facturas);
                        facturas = new md_consolidado_facturacion();
                        IntContador = IntContador + 1;

                    }
                }

                BusClass.InsertarConsolidadoFacturacion(OBJDetalle, ref MsgRes);

            }

            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message + "Linea: " + IntContador.ToString();
            }
        }

        public List<ManagmentocargueconsolidadoResult> CuentaConsolidadoMD(String numero_factura, String numero_formula, DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return ListReporte = BusClass.CuentaConsolidadoMD(numero_factura, numero_formula, fecha_inicial, fecha_final, ref MsgRes);
        }

        

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        #endregion

    }
}