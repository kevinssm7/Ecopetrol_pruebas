using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Helpers;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;

namespace AsaludEcopetrol.Models.Medicamentos
{
    public class GestionMedicamentos
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
        public int tipo_consulta { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DOCUMENTO")]
        public decimal numero_documento { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FACTURA")]
        public String numero_factura { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DE FORMULA")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,20}$", ErrorMessage = "Maximo 20 caracteres")]
        public String numero_formula { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "MOTIVO GLOSA")]
        public int motivo_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "VALOS GLOSA")]
        public Decimal valor_glosa { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "RESULTADO AUDITORIA")]
        public int resultado_auditoria { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "OBSERVACION")]
        [RegularExpression(@"^[a-zA-Z\W\S]{1,2000}$", ErrorMessage = "Maximo 2000 caracteres")]
        public String observaciones { get; set; }

        public String usuario_digita { get; set; }

        public int tipo { get; set; }

        [Display(Name = "VALOR FORMULA")]
        public decimal Vrltotal { get; set; }

        [Display(Name = "VALOR GLOSADO")]
        public decimal VlrGlosa { get; set; }

        [Display(Name = "VALOR PENDIENTE")]
        public decimal VlrPendiente { get; set; }


        public decimal Vlrvalidacion { get; set; }

        public String tiene_glosa { get; set; }

        public int id_md_glosa_detalle { get; set; }

        public String resultado_conciliacion { get; set; }

        public Decimal valor_sustentado { get; set; }

        public int estado { get; set; }

        [Required(ErrorMessage = "***")]

        private List<ManagmentFacMedicamentosResult> _ListFacturas;
        public List<ManagmentFacMedicamentosResult> ListFacturas
        {
            get
            {
                if (_ListFacturas == null)
                {
                    _ListFacturas = BusClass.CuentaFacMedicamentos("", numero_formula, 1, ref MsgRes);
                    return _ListFacturas;
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

        private List<ManagmentFacMedicamentosDetalleResult> _ListFacturasDetalle;
        public List<ManagmentFacMedicamentosDetalleResult> ListFacturasDetalle
        {
            get
            {
                if (_ListFacturasDetalle == null)
                {

                    _ListFacturasDetalle = BusClass.CuentaFacMedicamentosDetalle("", numero_formula, 1, ref MsgRes);


                    return _ListFacturasDetalle;
                }
                else
                {
                    return _ListFacturasDetalle;
                }

            }

            set
            {
                _ListFacturasDetalle = value;
            }
        }

        private List<ManagmentFacMedicamentosResult> _ListFormulas;
        public List<ManagmentFacMedicamentosResult> ListFormulas
        {
            get
            {
                if (_ListFormulas == null)
                {
                    return _ListFormulas = new List<ManagmentFacMedicamentosResult>();
                }
                else
                {
                    return _ListFormulas;
                }

            }

            set
            {
                _ListFormulas = value;
            }
        }

        private List<vw_md_ref_glosa> _GetMedglosa;
        public List<vw_md_ref_glosa> GetMedglosa
        {
            get
            {
                if (_GetMedglosa == null)
                {
                    return _GetMedglosa = BusClass.GetMedglosa();
                }
                else
                {
                    return _GetMedglosa;
                }

            }

            set
            {
                _GetMedglosa = value;
            }
        }

        private ICollection<vw_md_ref_glosa> _productos2;
        public ICollection<vw_md_ref_glosa> Productos2
        {
            get
            {
                if (_productos2 == null)
                {
                    return _productos2 = BusClass.GetMedglosa();
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

        private md_glosa _OBJGlosa;
        public md_glosa OBJGlosa
        {
            get
            {
                if (_OBJGlosa == null)
                {
                    return _OBJGlosa = new md_glosa();
                }
                else
                {
                    return _OBJGlosa;
                }

            }

            set
            {
                _OBJGlosa = value;
            }
        }

        private md_glosa_detalle _OBJGlosaDetalle;
        public md_glosa_detalle OBJGlosaDetalle
        {
            get
            {
                if (_OBJGlosaDetalle == null)
                {
                    return _OBJGlosaDetalle = new md_glosa_detalle();
                }
                else
                {
                    return _OBJGlosaDetalle;
                }

            }

            set
            {
                _OBJGlosaDetalle = value;
            }
        }

        private List<vw_glosa_medicamentos> _LstGlosas;
        public List<vw_glosa_medicamentos> LstGlosas
        {
            get
            {
                if (_LstGlosas == null)
                {
                    return _LstGlosas = BusClass.ConsultaGlosa(numero_formula);

                }
                else
                {
                    return _LstGlosas;
                }
            }

            set
            {
                _LstGlosas = value;
            }
        }

        private List<md_Ref_resultado_auditoria> _LstResultado;
        public List<md_Ref_resultado_auditoria> LstResultado
        {
            get
            {
                if (_LstResultado == null)
                {
                    return _LstResultado = BusClass.GetResultadoAUD();
                }
                else
                {
                    return _LstResultado;
                }

            }

            set
            {
                _LstResultado = value;
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

        private List<ManagmentFacMedicamentosResult> _ListFacturasImg;
        public List<ManagmentFacMedicamentosResult> ListFacturasImg
        {
            get
            {
                if (_ListFacturasImg == null)
                {
                    _ListFacturasImg = new List<ManagmentFacMedicamentosResult>();

                    return _ListFacturasImg;
                }
                else
                {
                    return _ListFacturasImg;
                }

            }

            set
            {
                _ListFacturasImg = value;
            }
        }

        #endregion

        #region FUNCIONES

        public List<ManagmentFacMedicamentosResult> FacMedicamentos(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            return ListFacturas = BusClass.CuentaFacMedicamentos(factura, formula, OPC, ref MsgRes);
        }

        public List<ManagmentFacMedicamentosResult> ForMedicamentos(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            return ListFormulas = BusClass.CuentaFacMedicamentos(factura, formula, OPC, ref MsgRes);
        }

        public List<management_validadorCarguePrefacturasResult> GetPrefacturasListadoCargue(int? rol, string usuario)
        {
            return BusClass.GetPrefacturasListadoCargue(rol, usuario);
        }

        public Int32 InsertarGlosaMD(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGlosaMD(OBJGlosa, ref MsgRes);
        }

        public Int32 InsertarGlosaDetalleMD(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGlosaDetalleMD(OBJGlosaDetalle, ref MsgRes);
        }

        public void EliminarGlosa(Int32 id, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarGlosa(id, ref MsgRes);
        }

        public void Cargardatos()
        {
            List<ManagmentFacMedicamentosResult> list2 = new List<ManagmentFacMedicamentosResult>();

            list2 = BusClass.CuentaFacMedicamentos("", numero_formula, 1, ref MsgRes);

            foreach (var item in list2)
            {
                Vrltotal = item.vlr_total.Value;
                tiene_glosa = item.tiene_glosa;
                resultado_auditoria = item.id_ref_md_resultado_auditoria.Value;
            }

            List<vw_glosa_medicamentos> list3 = new List<vw_glosa_medicamentos>();

            list3 = BusClass.ConsultaGlosa(numero_formula);

            foreach (var item2 in list3)
            {
                VlrGlosa = item2.total_glosa.Value;
            }

            VlrPendiente = Vrltotal - VlrGlosa;
        }

        public void CargarImg(String formula)
        {
            ListFacturasImg = BusClass.CuentaFacMedicamentos("", formula, 1, ref MsgRes);

        }

        /*Kevin*/

        //public void CarguePrefacturas(md_prefacturas_cargue_base carguebase, List<md_prefacturas_detalle> carguedetalle, ref MessageResponseOBJ MsgRes)
        //{
        //    BusClass.CarguePrefacturas(carguebase, carguedetalle, ref MsgRes);
        //}

        public void CargueLupe(md_Lupe_cargue_base carguebase, List<md_lupe_cargue_base_detalle> carguedetalle, List<md_lupe_intermediacion_dtll> Intermediaciones, ref MessageResponseOBJ MsgRes)
        {
            BusClass.CargueLupe(carguebase, carguedetalle, Intermediaciones, ref MsgRes);
        }

        public List<md_prefacturas_cargue_base> GetPrefacturasList()
        {
            return BusClass.GetPrefacturasList();
        }

        public List<management_validadorPrefacturasResult> GetPrefacturasListado(int? rol, string usuario)
        {
            return BusClass.GetPrefacturasListado(rol, usuario);
        }

        public md_prefacturas_cargue_base GetPrefacturas(int id)
        {
            return BusClass.GetPrefacturas(id);
        }

        public List<md_prefacturas_detalle> GetPrefacturasById(string numeroPrefactura)
        {
            return BusClass.GetPrefacturasById(numeroPrefactura);
        }

        public md_prefacturas_detalle GetPrefacturasID(int? id_detprefactura)
        {
            return BusClass.GetPrefacturasID(id_detprefactura);
        }

        public List<ManagmentReportePrefacturasResult> GetRptPrefacturas(int idcargue)
        {
            return BusClass.GetRptPrefacturas(idcargue);
        }

        public List<Managment_ReportePrefacturas_totalResult> GetPrefacturasTotal()
        {
            return BusClass.GetPrefacturasTotal();
        }

        public List<Managment_ReportePrefacturas_cerrar_abiertasResult> GetPrefacturasCerrarAbiertas()
        {
            return BusClass.GetPrefacturasCerrarAbiertas();
        }

        public List<Managment_ReportePrefacturas_cerrar_cerradasResult> GetPrefacturasCerrarCerradas()
        {
            return BusClass.GetPrefacturasCerrarCerradas();
        }

        public List<md_prefacturas_cargue_base> ConsultaExistenciaPrefactura(int regional, string numPrefactura, int idPrestador)
        {
            return BusClass.ConsultaExistenciaPrefactura(regional, numPrefactura, idPrestador);
        }

        public void ActualizarPrefactura(md_prefacturas_detalle obj)
        {
            BusClass.ActualizarPrefactura(obj);
        }

        public int ActualizarPrefacturaLista(List<int> ListaIds, string observaciones, double nuevo_valor)
        {
            return BusClass.ActualizarPrefacturaLista(ListaIds, observaciones, nuevo_valor);
        }

        public void ActualizarPrefacturaListaTotal(int idCargue, string observaciones, double nuevo_valor)
        {
            BusClass.ActualizarPrefacturaListaTotal(idCargue, observaciones, nuevo_valor);
        }

        public List<Managment_md_tablerocontrolResult> CuentaFacTableroControl(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentaFacTableroControl(fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<Managment_md_tablero_ConciliacionesResult> CuentaFacTableroControlConciliaciones(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentaFacTableroControlConciliaciones(ref MsgRes);
        }

        public List<Managment_md_tablero_Conciliaciones_detalleResult> CuentaFacTableroControlConciliacionesdtll(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentaFacTableroControlConciliacionesdtll(ref MsgRes);
        }


        public Int32 InsertarFFMMGlosaConciliacion(md_glosa_conciliacion OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMGlosaConciliacion(OBJ, ref MsgRes);
        }

        public vw_md_glosa_conciliacion ConsultaGlosaDtllId(Int32 id_md_glosa_detalle)
        {
            return BusClass.ConsultaGlosaDtllId(id_md_glosa_detalle);
        }

        /*Kevin*/
        public List<medicamentos_facturacion_dtll> EntidadMedicamentosFcturacionDtll(DataTable dt, ref MessageResponseOBJ MsgRes)
        {
            List<medicamentos_facturacion_dtll> result = new List<medicamentos_facturacion_dtll>();
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    medicamentos_facturacion_dtll obj = new medicamentos_facturacion_dtll();
                    fila++;

                    if (!string.IsNullOrEmpty(item["REGIONAL"].ToString()))
                    {
                        columna = "REGIONAL";
                        obj.regional = Convert.ToString(item[0]);

                        columna = "UNIS";
                        obj.unis = Convert.ToString(item[1]);

                        columna = "LOCALDAD";
                        obj.localidad = Convert.ToString(item[2]);

                        columna = "TIPO DE IDENTIFICACIÓN";
                        obj.tipo_identificacion = Convert.ToString(item[3]);

                        columna = "IDENTIFICACIÓN DEL PACIENTE";
                        obj.identificacion_paciente = Convert.ToString(item[4]);

                        columna = "Cum";
                        obj.cum = Convert.ToString(item[5]);

                        columna = "CONCEPTO";
                        obj.concepto = Convert.ToString(item[6]);

                        columna = "CANTIDAD";
                        obj.cantidad = Convert.ToInt32(item[7]);

                        columna = "FECHA DISPENSACIÓN";
                        obj.fecha_dispensacion = Convert.ToDateTime(item[8]);

                        columna = "FECHA FACTURA";
                        obj.fecha_factura = Convert.ToDateTime(item[9]);

                        columna = "NUMERO DE FACTURA";
                        obj.numero_factura = Convert.ToString(item[10]);

                        columna = "VALOR";
                        obj.valor = Convert.ToDecimal(item[11]);

                        columna = "NIT DEL PRESTADOR";
                        obj.nit_prestador = Convert.ToString(item[12]);

                        columna = "PRESTADOR";
                        obj.prestador = Convert.ToString(item[13]);

                        columna = "FACTURA SIN LETRAS";
                        obj.factura_sin_letras = Convert.ToString(item[14]);

                        columna = "FORMULA";
                        obj.formula = Convert.ToString(item[15]);

                        columna = "DOCUMENTO CONTABLE";
                        obj.documento_contable = Convert.ToString(item[16]);
                    }

                    result.Add(obj);
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
                MsgRes.CodeError = ex.Message;
            }

            return result;
        }

        public int GuardarMedicamentosFacturacion(medicamentos_facturacion Obj, List<medicamentos_facturacion_dtll> Result, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GuardarMedicamentosFacturacion(Obj, Result, ref MsgRes);
        }

        public List<ManagementMedicamentosFacturacionResult> GetListMdFacturacion()
        {
            return BusClass.GetListMdFacturacion();
        }

        public medicamentos_facturacion GetMed(int mes, int año, int regional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.medicamentos_facturacion.Where(l => l.mes == mes && l.año == año && regional == regional).FirstOrDefault();
        }

        public List<managemente_medicamentos_facturacionResult> Getmedicamentos_facturacion(int mes, int año, int regional)
        {
            return BusClass.Getmedicamentos_facturacion(mes, año, regional);
        }

        public List<ManagementMedicamentosFacturacionResult> GetMedicamentosFacturacionList(int? mesinicio, int? añoinicio, int? mesfinal, int? añofin, string Prestador, string regional)
        {
            return BusClass.GetMedicamentosFacturacionList(mesinicio, añoinicio, mesfinal, añofin, Prestador, regional);
        }

        public void CargarBaseMedicamentos(DataTable dt2, medicamentos_facturacion Obj, ref MessageResponseOBJ MsgRes)
        {

            Int32 IntContador = 1;
            String columna = "";


            List<medicamentos_facturacion_dtll> OBJDetalle = new List<medicamentos_facturacion_dtll>();
            try
            {
                foreach (DataRow item in dt2.Rows)
                {
                    medicamentos_facturacion_dtll facturas = new medicamentos_facturacion_dtll();

                    if (item["REGIONAL"].ToString() != "")
                    {
                        columna = "REGIONAL";
                        facturas.regional = Convert.ToString(item["REGIONAL"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "UNIS";
                        facturas.unis = Convert.ToString(item["UNIS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "LOCALDAD";
                        facturas.localidad = Convert.ToString(item["LOCALDAD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "TIPO DE IDENTIFICACIÓN";
                        facturas.tipo_identificacion = Convert.ToString(item["TIPO DE IDENTIFICACIÓN"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "IDENTIFICACIÓN DEL PACIENTE";
                        facturas.identificacion_paciente = Convert.ToString(item["IDENTIFICACIÓN DEL PACIENTE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "CUM";
                        facturas.cum = Convert.ToString(item["CUM"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "CONCEPTO";
                        facturas.concepto = Convert.ToString(item["CONCEPTO"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "CANTIDAD";
                        facturas.cantidad = Convert.ToInt32(item["CANTIDAD"]);
                        columna = "FECHA DISPENSACIÓN";
                        facturas.fecha_dispensacion = Convert.ToDateTime(item["FECHA DISPENSACIÓN"]);
                        columna = "FECHA FACTURA";
                        facturas.fecha_factura = Convert.ToDateTime(item["FECHA FACTURA"]);
                        columna = "NUMERO DE FACTURA";
                        facturas.numero_factura = Convert.ToString(item["NUMERO DE FACTURA"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "VALOR";
                        facturas.valor = Convert.ToDecimal(item["VALOR"]);
                        columna = "NIT DEL PRESTADOR";
                        facturas.nit_prestador = Convert.ToString(item["NIT DEL PRESTADOR"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "PRESTADOR";
                        facturas.prestador = Convert.ToString(item["PRESTADOR"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        columna = "FACTURA SIN LETRAS";
                        facturas.factura_sin_letras = Convert.ToString(item["FACTURA SIN LETRAS"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();


                        OBJDetalle.Add(facturas);
                        facturas = new medicamentos_facturacion_dtll();
                        IntContador = IntContador + 1;
                    }
                }
                BusClass.GuardarMedicamentosFacturacion(Obj, OBJDetalle, ref MsgRes);


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message + "Linea: " + IntContador.ToString() + " " + "Columna:" + columna.ToString();
            }
        }

        #endregion
        //kevin suarez 16-03-22

        public int ActualizarPrefacturaCerrar(md_prefacturas_detalle obj)
        {
            return BusClass.ActualizarPrefacturaCerrar(obj);
        }

        public int GuardarPrefacturaCerrada(md_prefacturas_cargue_cerradas obj)
        {
            return BusClass.GuardarPrefacturaCerrada(obj);
        }

        public List<md_prefacturas_detalle> ExcelAPrefacturas(DataTable dt, ref MessageResponseOBJ MsgRes)
        {
            List<md_prefacturas_detalle> result = new List<md_prefacturas_detalle>();
            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        md_prefacturas_detalle obj = new md_prefacturas_detalle();

                        fila++;

                        if (!string.IsNullOrEmpty(item["Remision pre factura fact"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Remision pre factura fact";
                            texto = Convert.ToString(item["Remision pre factura fact"]);
                            if (texto.Length <= 150)
                            {
                                obj.remision_prefactura_fact = Convert.ToString(item["Remision pre factura fact"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num Tirilla o consecutivo";
                            texto = Convert.ToString(item["Num Tirilla o consecutivo"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 12)
                                {
                                    obj.num_tirilla = Convert.ToString(item["Num Tirilla o consecutivo"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 12 carácteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha radicacion";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha radicacion"]);
                                if (fechas != null)
                                {
                                    obj.fecha_radicacion = Convert.ToDateTime(item["Fecha radicacion"]);
                                }
                                else
                                {
                                    obj.fecha_radicacion = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Nit";
                            texto = Convert.ToString(item["Nit"]);
                            if (texto.Length <= 50)
                            {
                                obj.nit = Convert.ToString(item["Nit"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo doc beneficiario";
                            texto = Convert.ToString(item["Tipo doc beneficiario"]);
                            if (texto.Length <= 3)
                            {
                                obj.tipo_id_beneficiario = Convert.ToString(item["Tipo doc beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 3 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num documento beneficiario";
                            texto = Convert.ToString(item["Num documento beneficiario"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_documento_beneficiario = Convert.ToString(item["Num documento beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre beneficiario";
                            texto = Convert.ToString(item["Nombre beneficiario"]);
                            if (texto.Length <= 300)
                            {
                                obj.nombre_beneficiario = Convert.ToString(item["Nombre beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            obj.nombre_beneficiario_especial = Regex.Replace(obj.nombre_beneficiario, @"[^\w\s.!@$%^&*()\-\/]+", "");
                            obj.nombre_beneficiario_especial = obj.nombre_beneficiario_especial.Replace("ñ", "n");
                            obj.nombre_beneficiario_especial = obj.nombre_beneficiario_especial.Replace("Ñ", "N");
                            obj.nombre_beneficiario_especial = obj.nombre_beneficiario_especial.Replace("-", "");
                            obj.nombre_beneficiario_especial = obj.nombre_beneficiario_especial.Replace(".", "");
                            obj.nombre_beneficiario_especial = obj.nombre_beneficiario_especial.Replace(" ", "");

                            columna = "Ciudad despacho";

                            texto = Convert.ToString(item["Ciudad despacho"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad_despacho = Convert.ToString(item["Ciudad despacho"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }
                            columna = "Id prescriptor";
                            texto = Convert.ToString(item["Id prescriptor"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_prescriptor = Convert.ToString(item["Id prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Prescriptor";
                            texto = Convert.ToString(item["Prescriptor"]);
                            if (texto.Length <= 300)
                            {
                                obj.prescriptor = Convert.ToString(item["Prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Especialidad";
                            texto = Convert.ToString(item["Especialidad"]);
                            if (texto.Length <= 200)
                            {
                                obj.especialidad = Convert.ToString(item["Especialidad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Cum";
                            texto = Convert.ToString(item["Cum"]);
                            if (texto.Length <= 150)
                            {
                                obj.cum = Convert.ToString(item["Cum"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod Ecopetrol hijo comercial";
                            texto = Convert.ToString(item["Cod Ecopetrol hijo comercial"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_ecopetrol_hijo_comercial = Convert.ToString(item["Cod Ecopetrol hijo comercial"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod interno medicamento";
                            texto = Convert.ToString(item["Cod interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_interno_medicamento = Convert.ToString(item["Cod interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Cod generico o interno medicamento";
                            texto = Convert.ToString(item["Cod generico o interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_generico_o_interno_medicamento = Convert.ToString(item["Cod generico o interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Presentacion";
                            texto = Convert.ToString(item["Presentacion"]);
                            if (texto.Length <= 200)
                            {
                                obj.Presentacion = Convert.ToString(item["Presentacion"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripcion producto";
                            texto = Convert.ToString(item["Descripcion producto"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_producto = Convert.ToString(item["Descripcion producto"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre comercial medicamento";
                            texto = Convert.ToString(item["Nombre comercial medicamento"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_comercial_medicacmento = Convert.ToString(item["Nombre comercial medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Laboratorio fabricante";
                            texto = Convert.ToString(item["Laboratorio fabricante"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_fabricante = Convert.ToString(item["Laboratorio fabricante"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha despacho formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha despacho formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_despacho_formula = Convert.ToDateTime(item["Fecha despacho formula"]);
                                }
                                else
                                {
                                    obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades prescritas";
                            texto = Convert.ToString(item["Num unidades prescritas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_prescritas = Convert.ToString(item["Num unidades prescritas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            if (fila == 13)
                            {
                                var prueba = 0;
                            }

                            columna = "Num formula";
                            texto = Convert.ToString(item["Num formula"]);

                            if (texto.Length <= 100)
                            {
                                obj.num_formula = Convert.ToString(item["Num formula"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_formula = Convert.ToDateTime(item["Fecha formula"]);
                                }
                                else
                                {
                                    obj.fecha_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades entregadas";
                            texto = Convert.ToString(item["Num unidades entregadas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_entregadas = Convert.ToString(item["Num unidades entregadas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Vlr unitario und entregada";
                            try
                            {
                                decima = Convert.ToDecimal(item["Vlr unitario und entregada"]);
                                if (decima != null)
                                {
                                    var valor = Convert.ToString(item["Vlr unitario und entregada"]).Replace(",","");
                                    obj.vlr_unitario_und_entregada = valor;
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }

                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Valor Iva";
                            texto = Convert.ToString(item["Valor Iva"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.IVA = Convert.ToDecimal(item["Valor Iva"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 15 carácteres.";
                                    throw new Exception(textError);
                                }

                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }


                            columna = "Valor total";
                            texto = Convert.ToString(item["Valor total"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.valor_total = Convert.ToDecimal(item["Valor total"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 15 carácteres.";
                                    throw new Exception(textError);
                                }

                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Codigo ATC";
                            texto = Convert.ToString(item["Codigo ATC"]);
                            if (texto.Length <= 150)
                            {
                                obj.CODIGO_ATC = Convert.ToString(item["Codigo ATC"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Grupo farmacologico";
                            texto = Convert.ToString(item["Grupo farmacologico"]);
                            if (texto.Length <= 150)
                            {
                                obj.grupo_farmacologico = Convert.ToString(item["Grupo farmacologico"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Unis";
                            texto = Convert.ToString(item["Unis"]);
                            if (texto.Length <= 50)
                            {
                                obj.unis = Convert.ToString(item["Unis"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Localidad";
                            texto = Convert.ToString(item["Localidad"]);
                            if (texto.Length <= 50)
                            {
                                obj.localidad = Convert.ToString(item["Localidad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            obj.observaciones = "";
                            obj.nuevo_valor = 0;
                            obj.nuevo_iva = 0;
                            obj.aprobado = false;
                            obj.abierta = 1;

                            List<md_prefacturas_detalle> llave1 = new List<md_prefacturas_detalle>();
                            llave1 = result.Where(x => x.num_documento_beneficiario == obj.num_documento_beneficiario
                            && (x.cum == obj.cum || x.nombre_comercial_medicacmento == obj.nombre_comercial_medicacmento)
                            && x.num_unidades_entregadas == obj.num_unidades_entregadas
                            && x.fecha_despacho_formula == obj.fecha_despacho_formula
                            && x.vlr_unitario_und_entregada == obj.vlr_unitario_und_entregada).ToList();

                            columna = "Existencia en cargue-";
                            if (llave1 != null)
                            {
                                if (llave1.Count() > 0)
                                {
                                    foreach (var dato in llave1)
                                    {
                                        dato.duplicado = 1;
                                        obj.duplicado = 1;
                                    }
                                }
                                else
                                {
                                    obj.duplicado = 0;
                                }
                            }
                            else
                            {
                                obj.duplicado = 0;
                            }

                            result.Add(obj);
                            obj = new md_prefacturas_detalle();
                        }
                    }

                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
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
            return result;
        }

        public Int32 ExcelALUPE(DataTable dt, md_Lupe_cargue_base obj, List<md_lupe_intermediacion_dtll> dtlleIntermediacion, ref MessageResponseOBJ MsgRes)
        {
            Int32 IntContador = 1;
            var columna = "";

            var textError = "";
            Int32 id_cargueBase = 0;
            try
            {
                md_lupe_intermediacion objM = new md_lupe_intermediacion();
                //objM.lupe_id = id_cargueBase;
                objM.fecha_vigencia = obj.vigencia_desde;
                objM.prestador_id = (int)obj.id_prestador;
                objM.fecha_digita = DateTime.Now;
                objM.usuario_digita = SesionVar.UserName;

                List<md_lupe_cargue_base_detalle> obj2 = new List<md_lupe_cargue_base_detalle>();
                int fila = 1;

                foreach (DataRow item in dt.Rows)
                {
                    fila++;
                    if (!string.IsNullOrEmpty(item["cod hijo"].ToString()))
                    {
                        var texto = "";
                        md_lupe_cargue_base_detalle lcbd = new md_lupe_cargue_base_detalle();

                        columna = "cod hijo";
                        lcbd.cod_hijo = Convert.ToString(item["cod hijo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "cod generico";
                        lcbd.cod_generico = Convert.ToString(item["cod generico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "cod comercial";
                        lcbd.cod_comercial = Convert.ToString(item["cod comercial"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "consecutivo";
                        lcbd.consecutivo = Convert.ToString(item["consecutivo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "cod ecopetrol padre";
                        lcbd.cod_ecopetrol_padre = Convert.ToString(item["cod ecopetrol padre"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "cod hijo und min farmaceutica";
                        lcbd.cod_hijo_und_min_farmaceutica = Convert.ToString(item["cod hijo und min farmaceutica"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "descripcion";
                        lcbd.descripcion = Convert.ToString(item["descripcion"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "nombre comercial";
                        lcbd.nombre_comercial = Convert.ToString(item["nombre comercial"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "laboratorio comercializador maestra";
                        lcbd.laboratorio_comercializador_maestra = Convert.ToString(item["laboratorio comercializador maestra"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "registro invima";
                        lcbd.registro_invima = Convert.ToString(item["registro invima"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "CUM";
                        lcbd.CUM = Convert.ToString(item["CUM"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "expediente";
                        lcbd.expediente = Convert.ToString(item["expediente"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "consecutivo cum";
                        lcbd.consecutivo_cum = Convert.ToString(item["consecutivo cum"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "cum numero";
                        lcbd.cum_numero = Convert.ToString(item["cum numero"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "clasificacion invma";
                        lcbd.clasificacion_invma = Convert.ToString(item["clasificacion invma"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "ATC";
                        lcbd.ATC = Convert.ToString(item["ATC"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "regulado";
                        lcbd.regulado = Convert.ToString(item["regulado"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "LUPE";
                        lcbd.LUPE = Convert.ToString(item["LUPE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "LME";
                        lcbd.LME = Convert.ToString(item["LME"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "ambito";
                        lcbd.ambito = Convert.ToString(item["ambito"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "marca generico";
                        lcbd.marca_generico = Convert.ToString(item["marca generico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "tipo acuerdo";
                        lcbd.tipo_acuerdo = Convert.ToString(item["tipo acuerdo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "und minima pactada";
                        lcbd.und_minima_pactada = Convert.ToString(item["und minima pactada"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "valor und min pactada";
                        try
                        {
                            lcbd.valor_und_min_pactada = Convert.ToDecimal(item["valor und min pactada"]);
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "opcion";
                        lcbd.opcion = Convert.ToString(item["opcion"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        columna = "activo";
                        lcbd.activo = Convert.ToString(item["activo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        obj2.Add(lcbd);
                        lcbd = new md_lupe_cargue_base_detalle();
                        IntContador = IntContador + 1;
                    }
                }

                try
                {
                    id_cargueBase = BusClass.CargueLUPEBase(obj, obj2);
                    var intermediacionId = BusClass.CargueLupeIntermediacionBase(objM, id_cargueBase);

                    if (intermediacionId != 0)
                    {
                        foreach (var item2 in dtlleIntermediacion)
                        {
                            item2.id_lupe_int_base = intermediacionId;
                        }

                        var intermediacionListaId = BusClass.CargueLupeIntermediacionLista(dtlleIntermediacion);
                    }
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;

                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    MsgRes.DescriptionResponse = ex.Message;
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                }

                return id_cargueBase;
            }
            catch (Exception ex)
            {
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
                return id_cargueBase;
            }
        }

        public int DevolverListaLupeDetalle(string rutafichero, int? idCargue, int? idPrefactura)
        {
            var insercionReaprobacion = 0;
            Int32 IntContador = 1;
            var columna = "";
            var textError = "";
            List<md_lupe_cargue_base_detalle_reaprobacion> OBJDetalle = new List<md_lupe_cargue_base_detalle_reaprobacion>();
            var book = new ExcelQueryFactory(rutafichero);

            var EData1 = (from c in book.WorksheetRange("A1", "AB999999", "Cargue") select c).ToList();
            int fila = 1;

            try
            {
                for (var i = 0; i < EData1.Count; i++)
                {

                    var item = EData1[i];
                    fila++;
                    if (item[0] != null && item[0] != "")
                    {
                        var texto = "";

                        md_lupe_cargue_base_detalle_reaprobacion lcbd = new md_lupe_cargue_base_detalle_reaprobacion();

                        var codHijo = Convert.ToString(item["cod hijo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        if (codHijo != null && codHijo != "")
                        {
                            lcbd.id_lupe_cargue_base = (int)idCargue;

                            columna = "cod hijo";
                            lcbd.cod_hijo = Convert.ToString(item["cod hijo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "cod generico";
                            lcbd.cod_generico = Convert.ToString(item["cod generico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "cod comercial";
                            lcbd.cod_comercial = Convert.ToString(item["cod comercial"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "consecutivo";
                            lcbd.consecutivo = Convert.ToString(item["consecutivo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "cod ecopetrol padre";
                            lcbd.cod_ecopetrol_padre = Convert.ToString(item["cod ecopetrol padre"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "cod hijo und min farmaceutica";
                            lcbd.cod_hijo_und_min_farmaceutica = Convert.ToString(item["cod hijo und min farmaceutica"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "descripcion";
                            lcbd.descripcion = Convert.ToString(item["descripcion"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "nombre comercial";
                            lcbd.nombre_comercial = Convert.ToString(item["nombre comercial"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "laboratorio comercializador maestra";
                            lcbd.laboratorio_comercializador_maestra = Convert.ToString(item["laboratorio comercializador maestra"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "registro invima";
                            lcbd.registro_invima = Convert.ToString(item["registro invima"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "Cum";
                            lcbd.CUM = Convert.ToString(item["Cum"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "expediente";
                            lcbd.expediente = Convert.ToString(item["expediente"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "consecutivo cum";
                            lcbd.consecutivo_cum = Convert.ToString(item["consecutivo cum"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "cum numero";
                            lcbd.cum_numero = Convert.ToString(item["cum numero"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "clasificacion invma";
                            lcbd.clasificacion_invma = Convert.ToString(item["clasificacion invma"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "ATC";
                            lcbd.ATC = Convert.ToString(item["ATC"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "regulado";
                            lcbd.regulado = Convert.ToString(item["regulado"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "LUPE";
                            lcbd.LUPE = Convert.ToString(item["LUPE"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "LME";
                            lcbd.LME = Convert.ToString(item["LME"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "ambito";
                            lcbd.ambito = Convert.ToString(item["ambito"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "marca generico";
                            lcbd.marca_generico = Convert.ToString(item["marca generico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "tipo acuerdo";
                            lcbd.tipo_acuerdo = Convert.ToString(item["tipo acuerdo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "und minima pactada";
                            lcbd.und_minima_pactada = Convert.ToString(item["und minima pactada"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "valor und min pactada";
                            try
                            {
                                lcbd.valor_und_min_pactada = Convert.ToDouble(item["valor und min pactada"]);
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }


                            columna = "opcion";
                            lcbd.opcion = Convert.ToString(item["opcion"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            columna = "activo";
                            lcbd.activo = Convert.ToString(item["activo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                            OBJDetalle.Add(lcbd);
                            lcbd = new md_lupe_cargue_base_detalle_reaprobacion();
                            IntContador = IntContador + 1;
                        }
                    }
                }

                insercionReaprobacion = BusClass.CargueLUPEListaReaprobacion(OBJDetalle, (int)idCargue, (int)idPrefactura);

                return insercionReaprobacion;
            }
            catch (Exception ex)
            {
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
                return insercionReaprobacion;
            }
        }

        //public int ExcelAPrefacturasReaprobacion(string rutafichero, int? idLog, int? idPrefacturaBase, string observacionesMasivo, ref MessageResponseOBJ MsgRes)
        //{

        //    List<md_prefacturas_reaprobacionMasiva> result = new List<md_prefacturas_reaprobacionMasiva>();
        //    var book = new ExcelQueryFactory(rutafichero);
        //    var insercionReaprobacion = 0;

        //    try
        //    {
        //        var EData1 = (from c in book.WorksheetRange("A1", "AT999999", "Cargue") select c).ToList();
        //        string columna = "";
        //        int fila = 1;
        //        var textError = "";

        //        try
        //        {
        //            for (var i = 0; i < EData1.Count; i++)
        //            {
        //                md_prefacturas_reaprobacionMasiva obj = new md_prefacturas_reaprobacionMasiva();

        //                var item = EData1[i];
        //                fila++;
        //                if (item[0] != null && item[0] != "")
        //                {
        //                    var texto = "";
        //                    var numero = 0;
        //                    DateTime fechas = new DateTime();
        //                    decimal decima = new decimal();

        //                    obj.id_log = idLog;
        //                    obj.Id_prefactura_cargue_base = idPrefacturaBase;

        //                    columna = "Nro registro";
        //                    texto = item["Nro registro"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.id_detalle_prefactura = Convert.ToInt32(item["Nro registro"]);
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Remision pre factura fact";
        //                    texto = item["Remision pre factura fact"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.remision_prefactura_fact = Convert.ToString(item["Remision pre factura fact"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num Tirilla o consecutivo";
        //                    texto = item["Num Tirilla o consecutivo"];
        //                    if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
        //                    {
        //                        if (texto.Length <= 12)
        //                        {
        //                            obj.num_tirilla = Convert.ToString(item["Num Tirilla o consecutivo"]).ToUpper();
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 12 carácteres.";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", solo puede contener números.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha radicacion";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha radicacion"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_radicacion = Convert.ToDateTime(item["Fecha radicacion"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_radicacion = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nit";
        //                    texto = item["Nit"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.nit = Convert.ToString(item["Nit"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Tipo doc beneficiario";
        //                    texto = item["Tipo doc beneficiario"];
        //                    if (texto.Length <= 3)
        //                    {
        //                        obj.tipo_id_beneficiario = Convert.ToString(item["Tipo doc beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 3 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num documento beneficiario";
        //                    texto = item["Num documento beneficiario"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_documento_beneficiario = Convert.ToString(item["Num documento beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nombre beneficiario";
        //                    texto = item["Nombre beneficiario"];
        //                    if (texto.Length <= 300)
        //                    {
        //                        obj.nombre_beneficiario = Convert.ToString(item["Nombre beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 300 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Ciudad despacho";

        //                    texto = item["Ciudad despacho"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.ciudad_despacho = Convert.ToString(item["Ciudad despacho"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Id prescriptor";
        //                    texto = item["Id prescriptor"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.id_prescriptor = Convert.ToString(item["Id prescriptor"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Prescriptor";
        //                    texto = item["Prescriptor"];
        //                    if (texto.Length <= 300)
        //                    {
        //                        obj.prescriptor = Convert.ToString(item["Prescriptor"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 300 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Especialidad";
        //                    texto = item["Especialidad"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.especialidad = Convert.ToString(item["Especialidad"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cum";
        //                    texto = item["Cum"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.cum = Convert.ToString(item["Cum"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cod Ecopetrol hijo comercial";
        //                    texto = item["Cod Ecopetrol hijo comercial"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_ecopetrol_hijo_comercial = Convert.ToString(item["Cod Ecopetrol hijo comercial"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cod interno medicamento";
        //                    texto = item["Cod interno medicamento"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_interno_medicamento = Convert.ToString(item["Cod interno medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Cod generico o interno medicamento";
        //                    texto = item["Cod generico o interno medicamento"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_generico_o_interno_medicamento = Convert.ToString(item["Cod generico o interno medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Presentacion";
        //                    texto = item["Presentacion"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.Presentacion = Convert.ToString(item["Presentacion"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Descripcion producto";
        //                    texto = item["Descripcion producto"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.descripcion_producto = Convert.ToString(item["Descripcion producto"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nombre comercial medicamento";
        //                    texto = item["Nombre comercial medicamento"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.nombre_comercial_medicacmento = Convert.ToString(item["Nombre comercial medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Laboratorio fabricante";
        //                    texto = item["Laboratorio fabricante"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.laboratorio_fabricante = Convert.ToString(item["Laboratorio fabricante"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha despacho formula";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha despacho formula"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_despacho_formula = Convert.ToDateTime(item["Fecha despacho formula"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num unidades prescritas";
        //                    texto = item["Num unidades prescritas"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_unidades_prescritas = Convert.ToString(item["Num unidades prescritas"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num formula";
        //                    texto = item["Num formula"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_formula = Convert.ToString(item["Num formula"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha formula";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha formula"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_formula = Convert.ToDateTime(item["Fecha formula"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_formula = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num unidades entregadas";
        //                    texto = item["Num unidades entregadas"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_unidades_entregadas = Convert.ToString(item["Num unidades entregadas"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Vlr unitario und entregada";
        //                    try
        //                    {
        //                        decima = Convert.ToDecimal(item["Vlr unitario und entregada"]);
        //                        if (decima != null)
        //                        {
        //                            obj.vlr_unitario_und_entregada = Convert.ToString(item["Vlr unitario und entregada"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", no puede ir vacio";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Valor Iva";
        //                    texto = item["Valor Iva"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.IVA = Convert.ToString(item["Valor Iva"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Valor total";
        //                    texto = item["Valor total"];
        //                    try
        //                    {
        //                        if (texto.Length <= 100)
        //                        {
        //                            obj.valor_total = Convert.ToDecimal(item["Valor total"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                            throw new Exception(textError);
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Codigo ATC";
        //                    texto = item["Codigo ATC"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.CODIGO_ATC = Convert.ToString(item["Codigo ATC"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Grupo farmacologico";
        //                    texto = item["Grupo farmacologico"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.grupo_farmacologico = Convert.ToString(item["Grupo farmacologico"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Observaciones";
        //                    texto = item["Observaciones"];
        //                    if (texto.Length <= 4000)
        //                    {
        //                        obj.observaciones = Convert.ToString(item["Observaciones"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 4000 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nuevo valor IVA";
        //                    texto = item["Nuevo valor IVA"];
        //                    try
        //                    {
        //                        if (texto.Length <= 100)
        //                        {
        //                            obj.nuevo_iva = Convert.ToDecimal(item["Nuevo valor IVA"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                            throw new Exception(textError);
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nuevo valor unitario";
        //                    try
        //                    {
        //                        decima = Convert.ToDecimal(item["Nuevo valor unitario"]);
        //                        if (numero != null)
        //                        {
        //                            obj.nuevo_valor = Convert.ToDecimal(item["Nuevo valor unitario"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", no puede ir vacio";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    obj.observacionesMasivo = observacionesMasivo;

        //                    result.Add(obj);
        //                    obj = new md_prefacturas_reaprobacionMasiva();
        //                }
        //            }

        //            insercionReaprobacion = BusClass.InsertarReparobacioPrefacturas(result, (int)idPrefacturaBase);
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
        //            return insercionReaprobacion;
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //            if (textError != "" && textError != null)
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
        //            }
        //            else
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
        //            }
        //            MsgRes.CodeError = ex.Message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        var mensaje = "";
        //        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //        if (error.Contains("Valid worksheet names"))
        //        {
        //            mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
        //            MsgRes.DescriptionResponse = mensaje;
        //        }
        //        else
        //        {
        //            MsgRes.DescriptionResponse = ex.Message;
        //        }
        //    }
        //    book.Dispose();
        //    return insercionReaprobacion;
        //}

        public int ExcelAPrefacturasReaprobacion(DataTable dt, int? idLog, int? idPrefacturaBase, string observacionesMasivo, ref MessageResponseOBJ MsgRes)
        {

            var insercionReaprobacion = 0;
            List<md_prefacturas_reaprobacionMasiva> listado = new List<md_prefacturas_reaprobacionMasiva>();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        md_prefacturas_reaprobacionMasiva obj = new md_prefacturas_reaprobacionMasiva();
                        fila++;

                        if (!string.IsNullOrEmpty(item["Nro registro"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            obj.id_log = idLog;
                            obj.Id_prefactura_cargue_base = idPrefacturaBase;

                            columna = "Nro registro";
                            texto = Convert.ToString(item["Nro registro"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_detalle_prefactura = Convert.ToInt32(item["Nro registro"]);
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Remision pre factura fact";
                            texto = Convert.ToString(item["Remision pre factura fact"]);
                            if (texto.Length <= 150)
                            {
                                obj.remision_prefactura_fact = Convert.ToString(item["Remision pre factura fact"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num Tirilla o consecutivo";
                            texto = Convert.ToString(item["Num Tirilla o consecutivo"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 12)
                                {
                                    obj.num_tirilla = Convert.ToString(item["Num Tirilla o consecutivo"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 12 carácteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha radicacion";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha radicacion"]);
                                if (fechas != null)
                                {
                                    obj.fecha_radicacion = Convert.ToDateTime(item["Fecha radicacion"]);
                                }
                                else
                                {
                                    obj.fecha_radicacion = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Nit";
                            texto = Convert.ToString(item["Nit"]);
                            if (texto.Length <= 50)
                            {
                                obj.nit = Convert.ToString(item["Nit"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo doc beneficiario";
                            texto = Convert.ToString(item["Tipo doc beneficiario"]);
                            if (texto.Length <= 3)
                            {
                                obj.tipo_id_beneficiario = Convert.ToString(item["Tipo doc beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 3 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num documento beneficiario";
                            texto = Convert.ToString(item["Num documento beneficiario"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_documento_beneficiario = Convert.ToString(item["Num documento beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre beneficiario";
                            texto = Convert.ToString(item["Nombre beneficiario"]);
                            if (texto.Length <= 300)
                            {
                                obj.nombre_beneficiario = Convert.ToString(item["Nombre beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Ciudad despacho";

                            texto = Convert.ToString(item["Ciudad despacho"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad_despacho = Convert.ToString(item["Ciudad despacho"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Id prescriptor";
                            texto = Convert.ToString(item["Id prescriptor"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_prescriptor = Convert.ToString(item["Id prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Prescriptor";
                            texto = Convert.ToString(item["Prescriptor"]);
                            if (texto.Length <= 300)
                            {
                                obj.prescriptor = Convert.ToString(item["Prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Especialidad";
                            texto = Convert.ToString(item["Especialidad"]);
                            if (texto.Length <= 200)
                            {
                                obj.especialidad = Convert.ToString(item["Especialidad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cum";
                            texto = Convert.ToString(item["Cum"]);
                            if (texto.Length <= 150)
                            {
                                obj.cum = Convert.ToString(item["Cum"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod Ecopetrol hijo comercial";
                            texto = Convert.ToString(item["Cod Ecopetrol hijo comercial"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_ecopetrol_hijo_comercial = Convert.ToString(item["Cod Ecopetrol hijo comercial"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod interno medicamento";
                            texto = Convert.ToString(item["Cod interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_interno_medicamento = Convert.ToString(item["Cod interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Cod generico o interno medicamento";
                            texto = Convert.ToString(item["Cod generico o interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_generico_o_interno_medicamento = Convert.ToString(item["Cod generico o interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Presentacion";
                            texto = Convert.ToString(item["Presentacion"]);
                            if (texto.Length <= 200)
                            {
                                obj.Presentacion = Convert.ToString(item["Presentacion"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripcion producto";
                            texto = Convert.ToString(item["Descripcion producto"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_producto = Convert.ToString(item["Descripcion producto"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre comercial medicamento";
                            texto = Convert.ToString(item["Nombre comercial medicamento"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_comercial_medicacmento = Convert.ToString(item["Nombre comercial medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Laboratorio fabricante";
                            texto = Convert.ToString(item["Laboratorio fabricante"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_fabricante = Convert.ToString(item["Laboratorio fabricante"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha despacho formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha despacho formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_despacho_formula = Convert.ToDateTime(item["Fecha despacho formula"]);
                                }
                                else
                                {
                                    obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades prescritas";
                            texto = Convert.ToString(item["Num unidades prescritas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_prescritas = Convert.ToString(item["Num unidades prescritas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num formula";
                            texto = Convert.ToString(item["Num formula"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_formula = Convert.ToString(item["Num formula"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_formula = Convert.ToDateTime(item["Fecha formula"]);
                                }
                                else
                                {
                                    obj.fecha_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades entregadas";
                            texto = Convert.ToString(item["Num unidades entregadas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_entregadas = Convert.ToString(item["Num unidades entregadas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Vlr unitario und entregada";
                            try
                            {
                                decima = Convert.ToDecimal(item["Vlr unitario und entregada"]);
                                if (decima != null)
                                {
                                    obj.vlr_unitario_und_entregada = Convert.ToString(item["Vlr unitario und entregada"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Valor Iva";
                            texto = Convert.ToString(item["Valor Iva"]);
                            if (texto.Length <= 100)
                            {
                                obj.IVA = Convert.ToString(item["Valor Iva"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Valor total";
                            texto = Convert.ToString(item["Valor total"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.valor_total = Convert.ToDecimal(item["Valor total"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }

                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Codigo ATC";
                            texto = Convert.ToString(item["Codigo ATC"]);
                            if (texto.Length <= 150)
                            {
                                obj.CODIGO_ATC = Convert.ToString(item["Codigo ATC"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Grupo farmacologico";
                            texto = Convert.ToString(item["Grupo farmacologico"]);
                            if (texto.Length <= 150)
                            {
                                obj.grupo_farmacologico = Convert.ToString(item["Grupo farmacologico"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Observaciones";
                            texto = Convert.ToString(item["Observaciones"]);
                            if (texto.Length <= 4000)
                            {
                                obj.observaciones = Convert.ToString(item["Observaciones"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 4000 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nuevo valor IVA";
                            texto = Convert.ToString(item["Nuevo valor IVA"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.nuevo_iva = Convert.ToDecimal(item["Nuevo valor IVA"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }

                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Nuevo valor unitario";
                            try
                            {
                                decima = Convert.ToDecimal(item["Nuevo valor unitario"]);
                                if (numero != null)
                                {
                                    obj.nuevo_valor = Convert.ToDecimal(item["Nuevo valor unitario"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            obj.observacionesMasivo = observacionesMasivo;

                            listado.Add(obj);
                            obj = new md_prefacturas_reaprobacionMasiva();
                        }
                    }

                    insercionReaprobacion = BusClass.InsertarReparobacioPrefacturas(listado, (int)idPrefacturaBase);
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    return insercionReaprobacion;
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
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

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

            return insercionReaprobacion;
        }

        //public int ExcelAPrefacturasDesaprobacion(string rutafichero, int? idLog, int? idPrefacturaBase, string observacionesMasivo, ref MessageResponseOBJ MsgRes)
        //{

        //    List<md_prefacturas_desaprobacionMasiva> result = new List<md_prefacturas_desaprobacionMasiva>();
        //    var book = new ExcelQueryFactory(rutafichero);
        //    var insercionReaprobacion = 0;

        //    try
        //    {
        //        var EData1 = (from c in book.WorksheetRange("A1", "AT999999", "Cargue") select c).ToList();
        //        string columna = "";
        //        int fila = 1;
        //        var textError = "";

        //        try
        //        {
        //            for (var i = 0; i < EData1.Count; i++)
        //            {
        //                md_prefacturas_desaprobacionMasiva obj = new md_prefacturas_desaprobacionMasiva();

        //                var item = EData1[i];
        //                fila++;
        //                if (item[0] != null && item[0] != "")
        //                {
        //                    var texto = "";
        //                    var numero = 0;
        //                    DateTime fechas = new DateTime();
        //                    decimal decima = new decimal();

        //                    obj.Id_prefactura_cargue_base = idPrefacturaBase;
        //                    obj.id_log = idLog;


        //                    columna = "Nro registro";
        //                    texto = item["Nro registro"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.id_detalle_prefactura = Convert.ToInt32(item["Nro registro"]);
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Remision pre factura fact";
        //                    texto = item["Remision pre factura fact"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.remision_prefactura_fact = Convert.ToString(item["Remision pre factura fact"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num Tirilla o consecutivo";
        //                    texto = item["Num Tirilla o consecutivo"];
        //                    if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
        //                    {
        //                        if (texto.Length <= 12)
        //                        {
        //                            obj.num_tirilla = Convert.ToString(item["Num Tirilla o consecutivo"]).ToUpper();
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 12 carácteres.";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", solo puede contener números.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha radicacion";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha radicacion"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_radicacion = Convert.ToDateTime(item["Fecha radicacion"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_radicacion = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nit";
        //                    texto = item["Nit"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.nit = Convert.ToString(item["Nit"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Tipo doc beneficiario";
        //                    texto = item["Tipo doc beneficiario"];
        //                    if (texto.Length <= 3)
        //                    {
        //                        obj.tipo_id_beneficiario = Convert.ToString(item["Tipo doc beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 3 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num documento beneficiario";
        //                    texto = item["Num documento beneficiario"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_documento_beneficiario = Convert.ToString(item["Num documento beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nombre beneficiario";
        //                    texto = item["Nombre beneficiario"];
        //                    if (texto.Length <= 300)
        //                    {
        //                        obj.nombre_beneficiario = Convert.ToString(item["Nombre beneficiario"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 300 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Ciudad despacho";

        //                    texto = item["Ciudad despacho"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.ciudad_despacho = Convert.ToString(item["Ciudad despacho"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Id prescriptor";
        //                    texto = item["Id prescriptor"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.id_prescriptor = Convert.ToString(item["Id prescriptor"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Prescriptor";
        //                    texto = item["Prescriptor"];
        //                    if (texto.Length <= 300)
        //                    {
        //                        obj.prescriptor = Convert.ToString(item["Prescriptor"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 300 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Especialidad";
        //                    texto = item["Especialidad"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.especialidad = Convert.ToString(item["Especialidad"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cum";
        //                    texto = item["Cum"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.cum = Convert.ToString(item["Cum"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cod Ecopetrol hijo comercial";
        //                    texto = item["Cod Ecopetrol hijo comercial"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_ecopetrol_hijo_comercial = Convert.ToString(item["Cod Ecopetrol hijo comercial"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Cod interno medicamento";
        //                    texto = item["Cod interno medicamento"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_interno_medicamento = Convert.ToString(item["Cod interno medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Cod generico o interno medicamento";
        //                    texto = item["Cod generico o interno medicamento"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.cod_generico_o_interno_medicamento = Convert.ToString(item["Cod generico o interno medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Presentacion";
        //                    texto = item["Presentacion"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.Presentacion = Convert.ToString(item["Presentacion"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Descripcion producto";
        //                    texto = item["Descripcion producto"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.descripcion_producto = Convert.ToString(item["Descripcion producto"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nombre comercial medicamento";
        //                    texto = item["Nombre comercial medicamento"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.nombre_comercial_medicacmento = Convert.ToString(item["Nombre comercial medicamento"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Laboratorio fabricante";
        //                    texto = item["Laboratorio fabricante"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.laboratorio_fabricante = Convert.ToString(item["Laboratorio fabricante"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha despacho formula";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha despacho formula"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_despacho_formula = Convert.ToDateTime(item["Fecha despacho formula"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num unidades prescritas";
        //                    texto = item["Num unidades prescritas"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_unidades_prescritas = Convert.ToString(item["Num unidades prescritas"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num formula";
        //                    texto = item["Num formula"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_formula = Convert.ToString(item["Num formula"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha formula";
        //                    try
        //                    {
        //                        fechas = Convert.ToDateTime(item["Fecha formula"]);
        //                        if (fechas != null)
        //                        {
        //                            obj.fecha_formula = Convert.ToDateTime(item["Fecha formula"]);
        //                        }
        //                        else
        //                        {
        //                            obj.fecha_formula = new DateTime(1900, 01, 01);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Num unidades entregadas";
        //                    texto = item["Num unidades entregadas"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.num_unidades_entregadas = Convert.ToString(item["Num unidades entregadas"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Vlr unitario und entregada";
        //                    try
        //                    {
        //                        decima = Convert.ToDecimal(item["Vlr unitario und entregada"]);
        //                        if (decima != null)
        //                        {
        //                            obj.vlr_unitario_und_entregada = Convert.ToString(item["Vlr unitario und entregada"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", no puede ir vacio";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Valor Iva";
        //                    texto = item["Valor Iva"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.IVA = Convert.ToString(item["Valor Iva"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Valor total";
        //                    texto = item["Valor total"];
        //                    try
        //                    {
        //                        if (texto.Length <= 100)
        //                        {
        //                            obj.valor_total = Convert.ToDecimal(item["Valor total"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                            throw new Exception(textError);
        //                        }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Codigo ATC";
        //                    texto = item["Codigo ATC"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.CODIGO_ATC = Convert.ToString(item["Codigo ATC"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Grupo farmacologico";
        //                    texto = item["Grupo farmacologico"];
        //                    if (texto.Length <= 150)
        //                    {
        //                        obj.grupo_farmacologico = Convert.ToString(item["Grupo farmacologico"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 150 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Observaciones";
        //                    texto = item["Observaciones"];
        //                    if (texto.Length <= 4000)
        //                    {
        //                        obj.observaciones = Convert.ToString(item["Observaciones"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 4000 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nuevo valor IVA";
        //                    texto = item["Nuevo valor IVA"];
        //                    try
        //                    {
        //                        if (texto.Length <= 100)
        //                        {
        //                            obj.nuevo_iva = Convert.ToDecimal(item["Nuevo valor IVA"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                            throw new Exception(textError);
        //                        }
        //                    }

        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Nuevo valor unitario";
        //                    try
        //                    {
        //                        decima = Convert.ToDecimal(item["Nuevo valor unitario"]);
        //                        if (numero != null)
        //                        {
        //                            obj.nuevo_valor = Convert.ToDecimal(item["Nuevo valor unitario"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", no puede ir vacio";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", tiene formato incorrecto";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Observacion desaprobación";
        //                    texto = item["Observacion desaprobación"];
        //                    if (texto.Length <= 2000)
        //                    {
        //                        obj.observacion_desaprobacion = Convert.ToString(item["Observacion desaprobación"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 4000 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    obj.observacionesMasivo = observacionesMasivo;

        //                    result.Add(obj);
        //                    obj = new md_prefacturas_desaprobacionMasiva();
        //                }
        //            }

        //            insercionReaprobacion = BusClass.InsertarDesaparobacioPrefacturas(result, (int)idPrefacturaBase);
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
        //            return insercionReaprobacion;
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //            if (textError != "" && textError != null)
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
        //            }
        //            else
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
        //            }
        //            MsgRes.CodeError = ex.Message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        var mensaje = "";
        //        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //        if (error.Contains("Valid worksheet names"))
        //        {
        //            mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
        //            MsgRes.DescriptionResponse = mensaje;
        //        }
        //        else
        //        {
        //            MsgRes.DescriptionResponse = ex.Message;
        //        }
        //    }
        //    book.Dispose();
        //    return insercionReaprobacion;
        //}

        public int ExcelAPrefacturasDesaprobacion(DataTable dt, int? idLog, int? idPrefacturaBase, string observacionesMasivo, ref MessageResponseOBJ MsgRes)
        {

            List<md_prefacturas_desaprobacionMasiva> result = new List<md_prefacturas_desaprobacionMasiva>();
            var insercionReaprobacion = 0;

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        md_prefacturas_desaprobacionMasiva obj = new md_prefacturas_desaprobacionMasiva();

                        fila++;

                        if (!string.IsNullOrEmpty(item["Nro registro"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            obj.Id_prefactura_cargue_base = idPrefacturaBase;
                            obj.id_log = idLog;


                            columna = "Nro registro";
                            texto = Convert.ToString(item["Nro registro"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_detalle_prefactura = Convert.ToInt32(item["Nro registro"]);
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Remision pre factura fact";
                            texto = Convert.ToString(item["Remision pre factura fact"]);
                            if (texto.Length <= 150)
                            {
                                obj.remision_prefactura_fact = Convert.ToString(item["Remision pre factura fact"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num Tirilla o consecutivo";
                            texto = Convert.ToString(item["Num Tirilla o consecutivo"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 12)
                                {
                                    obj.num_tirilla = Convert.ToString(item["Num Tirilla o consecutivo"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 12 carácteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha radicacion";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha radicacion"]);
                                if (fechas != null)
                                {
                                    obj.fecha_radicacion = Convert.ToDateTime(item["Fecha radicacion"]);
                                }
                                else
                                {
                                    obj.fecha_radicacion = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Nit";
                            texto = Convert.ToString(item["Nit"]);
                            if (texto.Length <= 50)
                            {
                                obj.nit = Convert.ToString(item["Nit"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo doc beneficiario";
                            texto = Convert.ToString(item["Tipo doc beneficiario"]);
                            if (texto.Length <= 3)
                            {
                                obj.tipo_id_beneficiario = Convert.ToString(item["Tipo doc beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 3 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num documento beneficiario";
                            texto = Convert.ToString(item["Num documento beneficiario"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_documento_beneficiario = Convert.ToString(item["Num documento beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre beneficiario";
                            texto = Convert.ToString(item["Nombre beneficiario"]);
                            if (texto.Length <= 300)
                            {
                                obj.nombre_beneficiario = Convert.ToString(item["Nombre beneficiario"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Ciudad despacho";

                            texto = Convert.ToString(item["Ciudad despacho"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad_despacho = Convert.ToString(item["Ciudad despacho"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Id prescriptor";
                            texto = Convert.ToString(item["Id prescriptor"]);
                            if (texto.Length <= 50)
                            {
                                obj.id_prescriptor = Convert.ToString(item["Id prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Prescriptor";
                            texto = Convert.ToString(item["Prescriptor"]);
                            if (texto.Length <= 300)
                            {
                                obj.prescriptor = Convert.ToString(item["Prescriptor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 300 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Especialidad";
                            texto = Convert.ToString(item["Especialidad"]);
                            if (texto.Length <= 200)
                            {
                                obj.especialidad = Convert.ToString(item["Especialidad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cum";
                            texto = Convert.ToString(item["Cum"]);
                            if (texto.Length <= 150)
                            {
                                obj.cum = Convert.ToString(item["Cum"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod Ecopetrol hijo comercial";
                            texto = Convert.ToString(item["Cod Ecopetrol hijo comercial"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_ecopetrol_hijo_comercial = Convert.ToString(item["Cod Ecopetrol hijo comercial"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Cod interno medicamento";
                            texto = Convert.ToString(item["Cod interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_interno_medicamento = Convert.ToString(item["Cod interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Cod generico o interno medicamento";
                            texto = Convert.ToString(item["Cod generico o interno medicamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.cod_generico_o_interno_medicamento = Convert.ToString(item["Cod generico o interno medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Presentacion";
                            texto = Convert.ToString(item["Presentacion"]);
                            if (texto.Length <= 200)
                            {
                                obj.Presentacion = Convert.ToString(item["Presentacion"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripcion producto";
                            texto = Convert.ToString(item["Descripcion producto"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_producto = Convert.ToString(item["Descripcion producto"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre comercial medicamento";
                            texto = Convert.ToString(item["Nombre comercial medicamento"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_comercial_medicacmento = Convert.ToString(item["Nombre comercial medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Laboratorio fabricante";
                            texto = Convert.ToString(item["Laboratorio fabricante"]);
                            if (texto.Length <= 200)
                            {
                                obj.laboratorio_fabricante = Convert.ToString(item["Laboratorio fabricante"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha despacho formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha despacho formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_despacho_formula = Convert.ToDateTime(item["Fecha despacho formula"]);
                                }
                                else
                                {
                                    obj.fecha_despacho_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades prescritas";
                            texto = Convert.ToString(item["Num unidades prescritas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_prescritas = Convert.ToString(item["Num unidades prescritas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Num formula";
                            texto = Convert.ToString(item["Num formula"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_formula = Convert.ToString(item["Num formula"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha formula";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha formula"]);
                                if (fechas != null)
                                {
                                    obj.fecha_formula = Convert.ToDateTime(item["Fecha formula"]);
                                }
                                else
                                {
                                    obj.fecha_formula = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Num unidades entregadas";
                            texto = Convert.ToString(item["Num unidades entregadas"]);
                            if (texto.Length <= 100)
                            {
                                obj.num_unidades_entregadas = Convert.ToString(item["Num unidades entregadas"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Vlr unitario und entregada";
                            try
                            {
                                decima = Convert.ToDecimal(item["Vlr unitario und entregada"]);
                                if (decima != null)
                                {
                                    obj.vlr_unitario_und_entregada = Convert.ToString(item["Vlr unitario und entregada"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Valor Iva";
                            texto = Convert.ToString(item["Valor Iva"]);
                            if (texto.Length <= 100)
                            {
                                obj.IVA = Convert.ToString(item["Valor Iva"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Valor total";
                            texto = Convert.ToString(item["Valor total"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.valor_total = Convert.ToDecimal(item["Valor total"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }

                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Codigo ATC";
                            texto = Convert.ToString(item["Codigo ATC"]);
                            if (texto.Length <= 150)
                            {
                                obj.CODIGO_ATC = Convert.ToString(item["Codigo ATC"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }


                            columna = "Grupo farmacologico";
                            texto = Convert.ToString(item["Grupo farmacologico"]);
                            if (texto.Length <= 150)
                            {
                                obj.grupo_farmacologico = Convert.ToString(item["Grupo farmacologico"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Observaciones";
                            texto = Convert.ToString(item["Observaciones"]);
                            if (texto.Length <= 4000)
                            {
                                obj.observaciones = Convert.ToString(item["Observaciones"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 4000 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nuevo valor IVA";
                            texto = Convert.ToString(item["Nuevo valor IVA"]);
                            try
                            {
                                if (texto.Length <= 100)
                                {
                                    obj.nuevo_iva = Convert.ToDecimal(item["Nuevo valor IVA"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }
                            }

                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Nuevo valor unitario";
                            try
                            {
                                decima = Convert.ToDecimal(item["Nuevo valor unitario"]);
                                if (numero != null)
                                {
                                    obj.nuevo_valor = Convert.ToDecimal(item["Nuevo valor unitario"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Observacion desaprobación";
                            texto = Convert.ToString(item["Observacion desaprobación"]);
                            if (texto.Length <= 2000)
                            {
                                obj.observacion_desaprobacion = Convert.ToString(item["Observacion desaprobación"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 4000 carácteres.";
                                throw new Exception(textError);
                            }

                            obj.observacionesMasivo = observacionesMasivo;

                            result.Add(obj);
                            obj = new md_prefacturas_desaprobacionMasiva();
                        }
                    }

                    insercionReaprobacion = BusClass.InsertarDesaparobacioPrefacturas(result, (int)idPrefacturaBase);
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    return insercionReaprobacion;
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
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

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

            return insercionReaprobacion;
        }

        //public int ExcelCierreMasivoPrefacturas(string rutafichero, int? cargueBase, ref MessageResponseOBJ MsgRes, List<management_prefacturas_consolidado_abiertasResult> lista)
        //{
        //    List<md_prefacturas_cierreMasivo> result = new List<md_prefacturas_cierreMasivo>();
        //    var book = new ExcelQueryFactory(rutafichero);
        //    var insercionReaprobacion = 0;

        //    try
        //    {
        //        var EData1 = (from c in book.WorksheetRange("A1", "K999999", "Abiertas") select c).ToList();
        //        string columna = "";
        //        int fila = 1;
        //        var textError = "";

        //        try
        //        {
        //            for (var i = 0; i < EData1.Count; i++)
        //            {
        //                md_prefacturas_cierreMasivo obj = new md_prefacturas_cierreMasivo();

        //                var item = EData1[i];
        //                fila++;
        //                if (item[0] != null && item[0] != "")
        //                {
        //                    var texto = "";
        //                    var numero = 0;
        //                    DateTime fechas = new DateTime();
        //                    decimal decima = new decimal();

        //                    obj.id_cargue = cargueBase;

        //                    columna = "Remisión prefactura";
        //                    texto = item["Remisión prefactura"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.remision_prefactura = Convert.ToString(item["Remisión prefactura"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Conteo prefacturas";
        //                    texto = item["Conteo prefacturas"];

        //                    if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
        //                    {
        //                        if (texto.Length <= 12)
        //                        {
        //                            obj.conteo_prefacturas = Convert.ToInt32(item["Conteo prefacturas"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 12 carácteres.";
        //                            throw new Exception(textError);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", solo puede contener números.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Regional";
        //                    texto = item["Regional"];
        //                    if (texto.Length <= 100)
        //                    {
        //                        obj.regional = Convert.ToString(item["Regional"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 100 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Prestador";
        //                    texto = item["Prestador"];
        //                    if (texto.Length <= 200)
        //                    {
        //                        obj.prestador = Convert.ToString(item["Prestador"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Total valor unitario";
        //                    texto = item["Total valor unitario"];
        //                    if (texto.Length > 0)
        //                    {
        //                        if (texto.Length <= 18)
        //                        {
        //                            obj.total_valor_unitario = Convert.ToDecimal(item["Total valor unitario"]);
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", sólo debe contener máximo 15 carácteres.";
        //                            throw new Exception(textError);
        //                        }
        //                    }

        //                    columna = "Valor total prefacturas";
        //                    texto = item["Valor total prefacturas"];

        //                    try
        //                    {
        //                        if (texto.Length > 0)
        //                        {
        //                            if (texto.Length <= 18)
        //                            {
        //                                obj.valor_total_prefacturas = Convert.ToDecimal(item["Valor total prefacturas"]);
        //                            }
        //                            else
        //                            {
        //                                textError = columna + ", sólo debe contener máximo 15 carácteres.";
        //                                throw new Exception(textError);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        var error = ex.Message;

        //                        if (error.Contains("sólo debe contener máximo"))
        //                        {
        //                            textError = columna + " " + error;
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", solo puede contener números.";
        //                        }
        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Nuevo valor total";
        //                    texto = item["Nuevo valor total"];

        //                    try
        //                    {
        //                        if (texto.Length > 0)
        //                        {
        //                            if (texto.Length <= 18)
        //                            {
        //                                obj.nuevo_valor_total = Convert.ToDecimal(item["Nuevo valor total"]);
        //                            }
        //                            else
        //                            {
        //                                textError = columna + ", sólo debe contener máximo 15 carácteres.";
        //                                throw new Exception(textError);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        var error = ex.Message;
        //                        if (error.Contains("sólo debe contener máximo"))
        //                        {
        //                            textError = columna + " " + error;
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", solo puede contener números.";
        //                        }

        //                        throw new Exception(textError);
        //                    }


        //                    columna = "Número factura final";
        //                    texto = item["Número factura final"];
        //                    if (texto.Length <= 50)
        //                    {
        //                        obj.num_factura_final = Convert.ToString(item["Número factura final"]).ToUpper();
        //                    }
        //                    else
        //                    {
        //                        textError = columna + ", sólo debe contener máximo 50 carácteres";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Fecha factura";
        //                    texto = item["Fecha factura"];
        //                    try
        //                    {
        //                        //fechas = Convert.ToDateTime(item["Fecha factura"]);
        //                        if (texto != null && texto != "")
        //                        {
        //                            obj.fecha_factura_final = Convert.ToDateTime(item["Fecha factura"]);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }

        //                    columna = "Valor cierre";
        //                    texto = item["Valor cierre"];

        //                    try
        //                    {
        //                        if (texto.Length > 0)
        //                        {
        //                            if (texto.Length <= 18)
        //                            {
        //                                obj.valor_cierrefinal = Convert.ToDecimal(item["Valor cierre"]);
        //                            }
        //                            else
        //                            {
        //                                textError = columna + ", sólo debe contener máximo 15 carácteres";
        //                                throw new Exception(textError);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        var error = ex.Message;
        //                        if (error.Contains("sólo debe contener máximo"))
        //                        {
        //                            textError = columna + " " + error;
        //                        }
        //                        else
        //                        {
        //                            textError = columna + ", solo puede contener números.";
        //                        }

        //                        throw new Exception(textError);
        //                    }

        //                    //columna = "IVA cierre";
        //                    //texto = item["IVA cierre"];

        //                    //try
        //                    //{
        //                    //    if (texto.Length > 0)
        //                    //    {
        //                    //        if (texto.Length <= 12)
        //                    //        {
        //                    //            obj.iva_final = Convert.ToDecimal(item["IVA cierre"]);
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            textError = columna + ", sólo debe contener máximo 12 carácteres";
        //                    //            throw new Exception(textError);
        //                    //        }
        //                    //    }
        //                    //}
        //                    //catch (Exception ex)
        //                    //{
        //                    //    var error = ex.Message;
        //                    //    textError = columna + ", solo puede contener números.";
        //                    //    throw new Exception(textError);
        //                    //}

        //                    obj.usuario_digita = SesionVar.UserName;
        //                    obj.fecha_digita = DateTime.Now;

        //                    management_prefacturas_consolidado_abiertasResult listaInicial = lista.Where(x => x.remision_prefactura_fact == obj.remision_prefactura).FirstOrDefault();

        //                    if (listaInicial != null)
        //                    {
        //                        var valorReal = listaInicial.nuevoValor_suma;

        //                        if (obj.valor_cierrefinal > (Convert.ToDecimal(valorReal + 100)) || obj.valor_cierrefinal < (Convert.ToDecimal(valorReal - 100)))
        //                        {
        //                            columna = "Valor de cierre diferentes";
        //                            textError = columna + ", Valor cierre no puede ser menor a 100 pesos o mayor a 100 pesos del valor aprobado.";
        //                            throw new Exception(textError);
        //                        }
        //                    }

        //                    result.Add(obj);
        //                    obj = new md_prefacturas_cierreMasivo();
        //                }
        //            }

        //            insercionReaprobacion = BusClass.InsertarCierrePrefacturasMasivo(result, (int)cargueBase);
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
        //            return insercionReaprobacion;
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //            if (textError != "" && textError != null)
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
        //            }
        //            else
        //            {
        //                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna;
        //            }
        //            MsgRes.CodeError = ex.Message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //        var mensaje = "";
        //        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

        //        if (error.Contains("Valid worksheet names"))
        //        {
        //            mensaje = "Error de nombre de hoja. El nombre debe ser: Abiertas";
        //            MsgRes.DescriptionResponse = mensaje;
        //        }
        //        else
        //        {
        //            MsgRes.DescriptionResponse = ex.Message;
        //        }
        //    }
        //    book.Dispose();
        //    return insercionReaprobacion;
        //}

        public int ExcelCierreMasivoPrefacturas(DataTable dt, int? cargueBase, ref MessageResponseOBJ MsgRes, List<management_prefacturas_consolidado_abiertasResult> lista)
        {
            List<md_prefacturas_cierreMasivo> result = new List<md_prefacturas_cierreMasivo>();
            var insercionReaprobacion = 0;

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        md_prefacturas_cierreMasivo obj = new md_prefacturas_cierreMasivo();

                        fila++;

                        if (!string.IsNullOrEmpty(item["Remisión prefactura"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            obj.id_cargue = cargueBase;

                            columna = "Remisión prefactura";
                            texto = Convert.ToString(item["Remisión prefactura"]);
                            if (texto.Length <= 100)
                            {
                                obj.remision_prefactura = Convert.ToString(item["Remisión prefactura"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Conteo prefacturas";
                            texto = Convert.ToString(item["Conteo prefacturas"]);

                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 12)
                                {
                                    obj.conteo_prefacturas = Convert.ToInt32(item["Conteo prefacturas"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 12 carácteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Regional";
                            texto = Convert.ToString(item["Regional"]);
                            if (texto.Length <= 100)
                            {
                                obj.regional = Convert.ToString(item["Regional"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Prestador";
                            texto = Convert.ToString(item["Prestador"]);
                            if (texto.Length <= 200)
                            {
                                obj.prestador = Convert.ToString(item["Prestador"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Total valor unitario";
                            texto = Convert.ToString(item["Total valor unitario"]);
                            if (texto.Length > 0)
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.total_valor_unitario = Convert.ToDecimal(item["Total valor unitario"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 15 carácteres.";
                                    throw new Exception(textError);
                                }
                            }

                            columna = "Valor total prefacturas";
                            texto = Convert.ToString(item["Valor total prefacturas"]);

                            try
                            {
                                if (texto.Length > 0)
                                {
                                    if (texto.Length <= 18)
                                    {
                                        obj.valor_total_prefacturas = Convert.ToDecimal(item["Valor total prefacturas"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", sólo debe contener máximo 15 carácteres.";
                                        throw new Exception(textError);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;

                                if (error.Contains("sólo debe contener máximo"))
                                {
                                    textError = columna + " " + error;
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener números.";
                                }
                                throw new Exception(textError);
                            }


                            columna = "Nuevo valor total";
                            texto = Convert.ToString(item["Nuevo valor total"]);

                            try
                            {
                                if (texto.Length > 0)
                                {
                                    if (texto.Length <= 18)
                                    {
                                        obj.nuevo_valor_total = Convert.ToDecimal(item["Nuevo valor total"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", sólo debe contener máximo 15 carácteres.";
                                        throw new Exception(textError);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                if (error.Contains("sólo debe contener máximo"))
                                {
                                    textError = columna + " " + error;
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener números.";
                                }

                                throw new Exception(textError);
                            }


                            columna = "Número factura final";
                            texto = Convert.ToString(item["Número factura final"]);
                            if (texto.Length <= 50)
                            {
                                obj.num_factura_final = Convert.ToString(item["Número factura final"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 carácteres";
                                throw new Exception(textError);
                            }

                            columna = "Fecha factura";
                            texto = Convert.ToString(item["Fecha factura"]);
                            try
                            {
                                //fechas = Convert.ToDateTime(item["Fecha factura"]);
                                if (texto != null && texto != "")
                                {
                                    obj.fecha_factura_final = Convert.ToDateTime(item["Fecha factura"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Valor cierre";
                            texto = Convert.ToString(item["Valor cierre"]);

                            try
                            {
                                if (texto.Length > 0)
                                {
                                    if (texto.Length <= 18)
                                    {
                                        obj.valor_cierrefinal = Convert.ToDecimal(item["Valor cierre"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", sólo debe contener máximo 15 carácteres";
                                        throw new Exception(textError);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                if (error.Contains("sólo debe contener máximo"))
                                {
                                    textError = columna + " " + error;
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener números.";
                                }

                                throw new Exception(textError);
                            }

                            //columna = "IVA cierre";
                            //texto = Convert.ToString(item["IVA cierre"];

                            //try
                            //{
                            //    if (texto.Length > 0)
                            //    {
                            //        if (texto.Length <= 12)
                            //        {
                            //            obj.iva_final = Convert.ToDecimal(item["IVA cierre"]);
                            //        }
                            //        else
                            //        {
                            //            textError = columna + ", sólo debe contener máximo 12 carácteres";
                            //            throw new Exception(textError);
                            //        }
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    var error = ex.Message;
                            //    textError = columna + ", solo puede contener números.";
                            //    throw new Exception(textError);
                            //}

                            obj.usuario_digita = SesionVar.UserName;
                            obj.fecha_digita = DateTime.Now;

                            management_prefacturas_consolidado_abiertasResult listaInicial = lista.Where(x => x.remision_prefactura_fact == obj.remision_prefactura).FirstOrDefault();

                            if (listaInicial != null)
                            {
                                var valorReal = listaInicial.nuevoValor_suma;

                                if (obj.valor_cierrefinal > (Convert.ToDecimal(valorReal + 100)) || obj.valor_cierrefinal < (Convert.ToDecimal(valorReal - 100)))
                                {
                                    columna = "Valor de cierre diferentes";
                                    textError = columna + ", Valor cierre no puede ser menor a 100 pesos o mayor a 100 pesos del valor aprobado.";
                                    throw new Exception(textError);
                                }
                            }

                            result.Add(obj);
                            obj = new md_prefacturas_cierreMasivo();
                        }
                    }

                    insercionReaprobacion = BusClass.InsertarCierrePrefacturasMasivo(result, (int)cargueBase);
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    return insercionReaprobacion;
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
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Abiertas";
                    MsgRes.DescriptionResponse = mensaje;
                }
                else
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }
            }

            return insercionReaprobacion;
        }

        public void EliminarCarguePrefactura(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarCarguePrefactura(idCargue, ref MsgRes);
        }

        public void EliminarCargueLUPE(int idCargue, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarCargueLUPE(idCargue, ref MsgRes);
        }

        public List<ref_referencia_pago> GetReferenciaPagoList()
        {
            return BusClass.GetReferenciaPagoList();
        }

        public int ExcelMedicamentosRegulados(DataTable dt, md_medicamentos_regulados MD, ref MessageResponseOBJ MsgRes)
        {
            List<md_medicamentos_regulados_detalle> Listado = new List<md_medicamentos_regulados_detalle>();
            var RtaInsercion = 0;

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        md_medicamentos_regulados_detalle obj = new md_medicamentos_regulados_detalle();

                        fila++;
                        if (!string.IsNullOrEmpty(item["CUM"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "CUM";
                            texto = Convert.ToString(item["CUM"]);
                            if (texto.Length <= 100)
                            {
                                obj.cum = Convert.ToString(item["CUM"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "Medicamento";
                            texto = Convert.ToString(item["Medicamento"]);
                            if (texto.Length <= 400)
                            {
                                obj.medicamento = Convert.ToString(item["Medicamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 400 carácteres.";
                                throw new Exception(textError);
                            }

                            columna = "ValorRegulado";
                            try
                            {
                                decima = Convert.ToDecimal(item["ValorRegulado"]);
                                if (decima != null)
                                {
                                    obj.valorRegulado = Convert.ToDecimal(item["ValorRegulado"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new md_medicamentos_regulados_detalle();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.CargueMedicamentosRegulados(MD, Listado, ref MsgRes);
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "El archivo no puede estar vacío";
                        MsgRes.CodeError = "Error";
                    }

                    return RtaInsercion;
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

            return RtaInsercion;
        }

        public int ExcelMedicamentosOPLS(DataTable dt, md_medicamentos_OPLS OPL, ref MessageResponseOBJ MsgRes)
        {
            List<md_medicamentos_OPLS_detalle> Listado = new List<md_medicamentos_OPLS_detalle>();
            var RtaInsercion = 0;

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                List<string> listaRegionales = new List<string>();
                listaRegionales.Add("CUR");
                listaRegionales.Add("CUI");
                listaRegionales.Add("CUS");
                listaRegionales.Add("CUQ");
                listaRegionales.Add("CUB");
                listaRegionales.Add("PSM");

                try
                {
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            md_medicamentos_OPLS_detalle obj = new md_medicamentos_OPLS_detalle();

                            fila++;
                            if (!string.IsNullOrEmpty(item["Regional"].ToString()))
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                columna = "Regional";
                                texto = Convert.ToString(item["Regional"]);
                                if (texto.Length <= 5)
                                {
                                    obj.regional = Convert.ToString(item["Regional"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 5 carácteres.";
                                    throw new Exception(textError);
                                }

                                if (!listaRegionales.Contains(obj.regional))
                                {
                                    textError = columna + ", la regional no está dentro de las aceptadas.";
                                    throw new Exception(textError);
                                }

                                columna = "Nombre completo del regente";
                                texto = Convert.ToString(item["Nombre completo del regente"]);
                                if (texto.Length <= 100)
                                {
                                    obj.nombre_regente = Convert.ToString(item["Nombre completo del regente"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }


                                columna = "Usuario SAMI regente";
                                texto = Convert.ToString(item["Usuario SAMI regente"]);
                                if (texto.Length <= 50)
                                {
                                    obj.usuario_regente = Convert.ToString(item["Usuario SAMI regente"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 50 carácteres.";
                                    throw new Exception(textError);
                                }

                                columna = "Nombre completo del contacto OPL";
                                texto = Convert.ToString(item["Nombre completo del contacto OPL"]);
                                if (texto.Length <= 100)
                                {
                                    obj.nombre_contactoOPL = Convert.ToString(item["Nombre completo del contacto OPL"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }

                                columna = "Correo electronico contacto OPL";
                                texto = Convert.ToString(item["Correo electronico contacto OPL"]);

                                if (validarEmail(texto))
                                {
                                    if (texto.Length <= 200)
                                    {
                                        obj.correo_OPL = Convert.ToString(item["Correo electronico contacto OPL"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", sólo debe contener máximo 200 carácteres.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo se acepta formato correo.";
                                    throw new Exception(textError);
                                }

                                columna = "Ciudad";
                                texto = Convert.ToString(item["Ciudad"]);
                                if (texto.Length <= 100)
                                {
                                    obj.ciudad = Convert.ToString(item["Ciudad"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 100 carácteres.";
                                    throw new Exception(textError);
                                }


                                Listado.Add(obj);
                                obj = new md_medicamentos_OPLS_detalle();
                            }
                        }
                    }
                    else
                    {
                        textError = "El archivo no puede venir vacío.";
                        throw new Exception(textError);
                    }

                    RtaInsercion = BusClass.CargueMedicamentosDatosOPLS(OPL, Listado, ref MsgRes);
                    return RtaInsercion;
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                    if (textError != "" && textError != null)
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " - columna: " + textError;
                    }
                    else
                    {
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " - columna: " + columna;
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

            return RtaInsercion;
        }

        public static bool validarEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}