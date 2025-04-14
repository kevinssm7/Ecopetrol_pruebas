using AsaludEcopetrol.BussinesManager;
using Aspose.Cells;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;

namespace AsaludEcopetrol.Models.FIS
{
    public class FIS_RIPS
    {
        #region PROPIEDADES

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

        //Ingreso prestador
        public int? id_prestador { get; set; }
        public string nit { get; set; }
        public string codigo_verfificacion { get; set; }
        public Decimal? codigo_SAP { get; set; }
        public string codigo_habilitacion { get; set; }
        public string codigo_sede { get; set; }
        public string razon_social { get; set; }
        public string tipoPrestador { get; set; }
        public int ciudad_proveedor { get; set; }
        public int departamento_proveedor { get; set; }
        public int regional { get; set; }
        public string direccion { get; set; }
        public string contacto_telefonico { get; set; }
        public string correo_electronico { get; set; }
        public int? estado { get; set; }
        public int tiene_mas_sedes { get; set; }
        //Fin ingreso prestador

        //Ingreso sedes prestador
        public int id_sede { get; set; }
        public string codigo_habilitacion_sede { get; set; }
        public string codigo_otra_sede { get; set; }
        public int ciudad_sede { get; set; }
        public int departamento_sede { get; set; }
        public int regional_sede { get; set; }
        public string direccion_sede { get; set; }
        public Decimal? contacto_telefonico_sede { get; set; }
        public string correo_electronico_sede { get; set; }

        //Fin ingreso sedes prestador

        #endregion

    }

    public class Contratos
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

        #endregion PROPIEDADES

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

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
        public int id_contrato { get; set; }
        public int id_prestador { get; set; }
        public string num_contrato { get; set; }
        public DateTime fecha_suscripcion { get; set; }
        public DateTime fecha_inicial { get; set; }
        public DateTime fecha_final { get; set; }
        public string objeto_contrato { get; set; }
        public string id_adm_contrato { get; set; }
        public string nom_adm_contrato { get; set; }
        public string id_apoyo_transaccional { get; set; }

        public string nom_apoyo_transaccional { get; set; }
        public string id_interventor { get; set; }
        public string nom_interventor { get; set; }
        public decimal valor_contrato { get; set; }
        public string manual_tarifario { get; set; }
        public string neogociacion { get; set; }
        public int id_registro { get; set; }
        public int id_tiga { get; set; }
        public string mensajeIngresoTarifas { get; set; }
        public int rtaIngresoTarifas { get; set; }
        public string mensajeIngresoTarifasValidacion { get; set; }
        public int rtaIngresoTarifasValidacion { get; set; }
        public string grupo_compras { get; set; }
        public string centro_logistico { get; set; }
        public string posicion_contrato { get; set; }

        public string contrato_operativo { get; set; }

        public string CargueTarifas(HttpPostedFileBase file, fis_rips_prestadores_contratos_tarifas tarifa, fis_rips_prestadores_contratos contrato)
        {
            var respuesta = "";
            try
            {
                CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                var asposeOptions = new Aspose.Cells.LoadOptions
                {
                    MemorySetting = MemorySetting.MemoryPreference
                };

                Workbook wb = new Workbook(file.InputStream, asposeOptions);
                Worksheet worksheet = wb.Worksheets[0];
                Cells cells = worksheet.Cells;
                int MaximaFila = cells.MaxDataRow;

                var ExportTableOptions = new Aspose.Cells.ExportTableOptions
                {
                    CheckMixedValueType = false,
                    ExportColumnName = true,
                    ExportAsString = true
                };

                DataTable dataTable = worksheet.Cells.ExportDataTable(cells.MinRow, cells.MinColumn, cells.Rows.Count, cells.MaxColumn + 1, ExportTableOptions);

                respuesta = ExcelMasivoTarifas(dataTable, tarifa, contrato, ref MsgRes);

                if (rtaIngresoTarifasValidacion == 1)
                {
                    mensajeIngresoTarifas = Convert.ToString(id_registro);
                    rtaIngresoTarifas = 1;
                }
                else
                {
                    mensajeIngresoTarifas = "ERROR AL INGRESAR CARGUE TARIFAS EN EL ARCHIVO: " + file.FileName + " - " + mensajeIngresoTarifasValidacion;
                    rtaIngresoTarifas = 2;
                }
            }


            catch (Exception ex)
            {
                var error = ex.Message;
                mensajeIngresoTarifas = "NO SE CARGARON LOS REGISTROS:" + error;
                rtaIngresoTarifas = 2;
            }

            return mensajeIngresoTarifas;
        }

        public string ExcelMasivoTarifas(DataTable dt2, fis_rips_prestadores_contratos_tarifas tarifas, fis_rips_prestadores_contratos contrato, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores_contratos_tarifas_registros> Listado = new List<fis_rips_prestadores_contratos_tarifas_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";
            var fechaInicial = contrato.fecha_inicial.Value.ToString("dd/MM/yyyy");
            DateTime fechaIni = Convert.ToDateTime(contrato.fecha_inicial);

            var fechaFinal = contrato.fecha_final.Value.ToString("dd/MM/yyyy");
            DateTime fechaFin = Convert.ToDateTime(contrato.fecha_final.Value);

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_prestadores_contratos_tarifas_registros obj = new fis_rips_prestadores_contratos_tarifas_registros();
                        fis_rips_cups cups = new fis_rips_cups();

                        fila++;
                        if (!string.IsNullOrEmpty(item["CUPS NEGOCIADO"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();
                            //if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))

                            columna = "CUPS NEGOCIADO";
                            texto = Convert.ToString(item["CUPS NEGOCIADO"]);
                            if (texto.Length <= 200)
                            {
                                obj.cups_negociado = Convert.ToString(item["CUPS NEGOCIADO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            cups = BusClass.TraerCupsCodigo(obj.cups_negociado);
                            if (cups == null)
                            {
                                textError = columna + ", no existe este cups negociado.";
                                throw new Exception(textError);
                            }

                            columna = "DESCRIPCIÓN CUPS";
                            texto = Convert.ToString(item["DESCRIPCIÓN CUPS"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_cups = Convert.ToString(item["DESCRIPCIÓN CUPS"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "MANUAL";
                            texto = Convert.ToString(item["MANUAL"]);
                            if (texto.Length <= 200)
                            {
                                obj.manual = Convert.ToString(item["MANUAL"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "INTERMEDIACIÓN";
                            texto = Convert.ToString(item["INTERMEDIACIÓN"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.intermediacion = Convert.ToDecimal(item["INTERMEDIACIÓN"]);
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


                            columna = "TARIFA INICIAL";
                            texto = Convert.ToString(item["TARIFA INICIAL"]);
                            try
                            {
                                if (texto.Length > 0)
                                {
                                    if (texto.Length <= 18)
                                    {
                                        obj.tarifa_inicial = Convert.ToDecimal(item["TARIFA INICIAL"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 18 caracteres.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede quedar vacio.";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "TAFIFA CON INTERMEDIACIÓN";
                            texto = Convert.ToString(item["TAFIFA CON INTERMEDIACIÓN"]);
                            try
                            {
                                if (texto.Length > 0)
                                {
                                    if (texto.Length <= 18)
                                    {
                                        obj.tarifa_intermediacion = Convert.ToDecimal(item["TAFIFA CON INTERMEDIACIÓN"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 18 caracteres.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede quedar vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            decimal tarifa_inicial = (decimal)obj.tarifa_inicial;
                            decimal intermediacion = (decimal)obj.intermediacion;

                            // Convertir intermediacion de porcentaje a decimal
                            decimal intermediacionDecimal = intermediacion / 100;

                            // Calcular totalIntermediaciones
                            var totalIntermediaciones = tarifa_inicial + (tarifa_inicial * intermediacionDecimal);

                            if (totalIntermediaciones != (decimal)obj.tarifa_intermediacion)
                            {
                                textError = columna + ", está mal calculada.";
                                throw new Exception(textError);
                            }

                            columna = "FECHA DE INICIO DE VIGENCIA";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA DE INICIO DE VIGENCIA"]);
                                if (fechas != null)
                                {
                                    obj.fecha_inicio_vigencia = Convert.ToDateTime(item["FECHA DE INICIO DE VIGENCIA"]);

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

                            if (obj.fecha_inicio_vigencia < fechaIni)
                            {
                                textError = columna + ", no puede ser menor a la fecha inicial de contrato";
                                throw new Exception(textError);
                            }

                            if (obj.fecha_inicio_vigencia > fechaFin)
                            {
                                textError = columna + ", no puede ser mayor a la fecha final de contrato";
                                throw new Exception(textError);
                            }

                            columna = "FECHA FIN VIGENCIA";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA FIN VIGENCIA"]);
                                if (fechas != null)
                                {
                                    obj.fecha_fin_vigencia = Convert.ToDateTime(item["FECHA FIN VIGENCIA"]);

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

                            if (obj.fecha_fin_vigencia < fechaIni)
                            {
                                textError = columna + ", no puede ser menor a la fecha inicial de contrato";
                                throw new Exception(textError);
                            }

                            if (obj.fecha_fin_vigencia > fechaFin)
                            {
                                textError = columna + ", no puede ser mayor a la fecha final de contrato";
                                throw new Exception(textError);
                            }

                            columna = "AMBITO";
                            texto = Convert.ToString(item["AMBITO"]);
                            if (texto.Length <= 200)
                            {
                                obj.ambito = Convert.ToString(item["AMBITO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new fis_rips_prestadores_contratos_tarifas_registros();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        id_registro = BusClass.insertarCargueTarifas(tarifas, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoTarifasValidacion = "SE INGRESARON CORRECTAMENTE LAS TARIFAS";
                            rtaIngresoTarifasValidacion = 1;
                        }
                        else
                        {
                            mensajeIngresoTarifasValidacion = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoTarifasValidacion = 2;
                        }

                        return mensajeIngresoTarifasValidacion;
                    }
                    else
                    {
                        mensajeIngresoTarifasValidacion = "Hoja vacía.";
                        rtaIngresoTarifasValidacion = 2;
                        return mensajeIngresoTarifasValidacion;
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

                    mensajeIngresoTarifasValidacion = MsgRes.DescriptionResponse;
                    rtaIngresoTarifasValidacion = 2;
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

                mensajeIngresoTarifasValidacion = MsgRes.DescriptionResponse;
                rtaIngresoTarifasValidacion = 2;
            }

            return mensajeIngresoTarifasValidacion;
        }


    }


    public class CUPS
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

        #endregion PROPIEDADES

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

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

        public string mensajeResultado { get; set; }

        public string InsertarTipoConsulta(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_consulta> ListadoConsultas = new List<fis_rips_cargue_consulta>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["consultas"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_consulta dato = new fis_rips_cargue_consulta();

                                columna = "CodPrestador";
                                texto = Convert.ToString(consulta["CodPrestador"]);
                                if (texto.Length <= 13)
                                {

                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["CodPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }

                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                //columna = "fechalnicioAtencion";
                                //try
                                //{
                                //    fechas = Convert.ToDateTime(consulta["fechalnicioAtencion"]);
                                //    if (fechas != null)
                                //    {
                                //        dato.fechaInicioAtencion = (DateTime?)consulta["fechalnicioAtencion"];
                                //    }
                                //    else
                                //    {
                                //        textError = columna + ", no puede ir vacio";
                                //        throw new Exception(textError);
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    textError = columna + ", tiene formato incorrecto";
                                //    throw new Exception(textError);
                                //}
                                columna = "fechaInicioAtencion";
                                try
                                {
                                    var datoPrueba = consulta["fechaInicioAtencion"];

                                    if (datoPrueba != null)
                                    {
                                        string fechaTexto = datoPrueba.ToString().Trim();
                                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                        if (!string.IsNullOrEmpty(fechaTexto))
                                        {
                                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                            string[] formatos = {
                                            "d/M/yyyy hh:mm:ss tt",   // Día y mes como uno o dos dígitos
                                            "d/M/yyyy HH:mm:ss",
                                            "M/d/yyyy hh:mm:ss tt",   // Mes como uno o dos dígitos
                                            "M/d/yyyy HH:mm:ss",
                                            "d/M/yyyy",
                                            "M/d/yyyy",
                                            "dd/MM/yyyy HH:mm:ss", // Formato original para día y mes en dos dígitos
                                            "MM/dd/yyyy hh:mm:ss tt",
                                            "dd/MM/yyyy HH:mm:ss",
                                            "MM/dd/yyyy HH:mm:ss"
                                            };

                                            DateTime fechaEspecial;

                                            // Probar los diferentes formatos
                                            bool fechaValida = DateTime.TryParseExact(
                                                fechaTexto,
                                                formatos,
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                                out fechaEspecial
                                            );

                                            if (fechaValida)
                                            {
                                                dato.fechaInicioAtencion = fechaEspecial;
                                            }
                                            else
                                            {
                                                textError = columna + ", tiene formato incorrecto";
                                                throw new Exception(textError);
                                            }
                                        }
                                        else
                                        {
                                            textError = columna + ", no puede ir vacío";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }



                                columna = "numAutorizacion";
                                texto = Convert.ToString(consulta["numAutorizacion"]);
                                if (texto.Length <= 100)
                                {

                                    dato.numAutorizacion = (string)consulta["numAutorizacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codConsulta";
                                try
                                {
                                    texto = Convert.ToString(consulta["codConsulta"]);
                                    if (texto.Length <= 13)
                                    {
                                        dato.codConsulta = Convert.ToString(consulta["codConsulta"]);
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 13 caracteres.";
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

                                columna = "modalidadGrupoServicioTecSal";
                                texto = Convert.ToString(consulta["modalidadGrupoServicioTecSal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.modalidadGrupoServicioTecSal = (string)consulta["modalidadGrupoServicioTecSal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "grupoServicios";
                                texto = Convert.ToString(consulta["grupoServicios"]);
                                if (texto.Length <= 100)
                                {
                                    dato.grupoServicios = (string)consulta["grupoServicios"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codServicio";
                                texto = Convert.ToString(consulta["codServicio"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codServicio = (string)consulta["codServicio"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "finalidadTecnologiaSalud";
                                texto = Convert.ToString(consulta["finalidadTecnologiaSalud"]);
                                if (texto.Length <= 100)
                                {
                                    dato.finalidadTecnologiaSalud = (string)consulta["finalidadTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "causaMotivoAtencion";
                                texto = Convert.ToString(consulta["causaMotivoAtencion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.causaMotivoAtencion = (string)consulta["causaMotivoAtencion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoRelacionado1";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionado1"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionado1 = (string)consulta["codDiagnosticoRelacionado1"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoRelacionado2";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionado2"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionado2 = (string)consulta["codDiagnosticoRelacionado2"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "codDiagnosticoRelacionado3";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionado3"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionado3 = (string)consulta["codDiagnosticoRelacionado3"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "tipoDiagnosficoPrincipal";
                                texto = Convert.ToString(consulta["tipoDiagnosficoPrincipal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.tipoDiagnosticoPrincipal = (string)consulta["tipoDiagnosficoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.tipoDocumentoIdentificacion = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numDocumentoldentificacion";
                                texto = Convert.ToString(consulta["numDocumentoldentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numDocumentoldentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "vrServicio";
                                texto = Convert.ToString(consulta["vrServicio"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                                    {
                                        dato.vrServicio = (decimal)consulta["vrServicio"];
                                    }
                                    else
                                    {
                                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "conceptoRecaudo";
                                texto = Convert.ToString(consulta["conceptoRecaudo"]);
                                if (texto.Length <= 100)
                                {
                                    dato.conceptoRecaudo = (string)consulta["conceptoRecaudo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "valorPagoModerador";
                                texto = Convert.ToString(consulta["valorPagoModerador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.valorPagoModerador = (decimal)consulta["valorPagoModerador"];
                                    }
                                    else
                                    {
                                        dato.valorPagoModerador = null;
                                    }

                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numFEVPagoModerador";
                                texto = Convert.ToString(consulta["numFEVPagoModerador"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numFEVPagoModerador = (string)consulta["numFEVPagoModerador"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.consecutivo = (decimal)consulta["consecutivo"];
                                    }
                                    else
                                    {
                                        dato.consecutivo = null;
                                    }

                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                ListadoConsultas.Add(dato);

                                fila++;
                            }

                            if (ListadoConsultas.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonConsultas(cargue, ListadoConsultas);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue CONSULTAS");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en CONSULTAS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }
                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_hospitalizacion(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_hospitalizacion> Listado = new List<fis_rips_cargue_hospitalizacion>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["hospitalizacion"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_hospitalizacion dato = new fis_rips_cargue_hospitalizacion();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {

                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }

                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "viaIngresoServicioSalud";
                                texto = Convert.ToString(consulta["viaIngresoServicioSalud"]);
                                if (texto.Length <= 50)
                                {

                                    dato.viaIngresoServicioSalud = (string)consulta["viaIngresoServicioSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                //columna = "fechaInicioAtencion";
                                //try
                                //{
                                //    fechas = Convert.ToDateTime(consulta["fechaInicioAtencion"]);
                                //    if (fechas != null)
                                //    {
                                //        dato.fechaInicioAtencion = (DateTime?)consulta["fechaInicioAtencion"];
                                //    }
                                //    else
                                //    {
                                //        textError = columna + ", no puede ir vacio";
                                //        throw new Exception(textError);
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    textError = columna + ", tiene formato incorrecto";
                                //    throw new Exception(textError);
                                //}

                                columna = "fechaInicioAtencion";
                                try
                                {
                                    var datoPrueba = consulta["fechaInicioAtencion"];

                                    if (datoPrueba != null)
                                    {
                                        string fechaTexto = datoPrueba.ToString().Trim();
                                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                        if (!string.IsNullOrEmpty(fechaTexto))
                                        {
                                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                            string[] formatos = {
        "d/M/yyyy hh:mm:ss tt",   // Día y mes como uno o dos dígitos
        "d/M/yyyy HH:mm:ss",
        "M/d/yyyy hh:mm:ss tt",   // Mes como uno o dos dígitos
        "M/d/yyyy HH:mm:ss",
        "d/M/yyyy",
        "M/d/yyyy",
        "dd/MM/yyyy HH:mm:ss", // Formato original para día y mes en dos dígitos
        "MM/dd/yyyy hh:mm:ss tt",
        "dd/MM/yyyy HH:mm:ss",
        "MM/dd/yyyy HH:mm:ss"
        };
                                            DateTime fechaEspecial;

                                            // Probar los diferentes formatos
                                            bool fechaValida = DateTime.TryParseExact(
                                                fechaTexto,
                                                formatos,
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                                out fechaEspecial
                                            );

                                            if (fechaValida)
                                            {
                                                dato.fechaInicioAtencion = fechaEspecial;
                                            }
                                            else
                                            {
                                                textError = columna + ", tiene formato incorrecto";
                                                throw new Exception(textError);
                                            }
                                        }
                                        else
                                        {
                                            textError = columna + ", no puede ir vacío";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }


                                columna = "numAutorizacion";
                                texto = Convert.ToString(consulta["numAutorizacion"]);
                                if (texto.Length <= 100)
                                {

                                    dato.numAutorizacion = (string)consulta["numAutorizacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "causaMotivoAtencion";
                                texto = Convert.ToString(consulta["causaMotivoAtencion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.causaMotivoAtencion = (string)consulta["causaMotivoAtencion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoPrincipalE";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipalE"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoPrincipalE = (string)consulta["codDiagnosticoPrincipalE"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoRelacionadoE1";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE1"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoRelacionadoE1 = (string)consulta["codDiagnosticoRelacionadoE1"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoRelacionadoE2";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE2"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoRelacionadoE2 = (string)consulta["codDiagnosticoRelacionadoE2"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoRelacionadoE3";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE3"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionadoE3 = (string)consulta["codDiagnosticoRelacionadoE3"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codComplicacion";
                                texto = Convert.ToString(consulta["codComplicacion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codComplicacion = (string)consulta["codComplicacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "condicionDestinoUsuarioEgreso";
                                texto = Convert.ToString(consulta["condicionDestinoUsuarioEgreso"]);
                                if (texto.Length <= 50)
                                {
                                    dato.condicionDestinoUsuarioEgreso = (string)consulta["condicionDestinoUsuarioEgreso"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoCausaMuerte";
                                texto = Convert.ToString(consulta["codDiagnosticoCausaMuerte"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoCausaMuerte = (string)consulta["codDiagnosticoCausaMuerte"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "fechaEgreso";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaEgreso"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaEgreso = (DateTime?)consulta["fechaEgreso"];
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


                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 9)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                Listado.Add(dato);

                                fila++;
                            }

                            if (Listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonHospitalizacion(cargue, Listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue HOSPITALIZACIÓN");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en HOSPITALIZACIÓN conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }

                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_medicamentos(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_medicamentos> listado = new List<fis_rips_cargue_medicamentos>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["medicamentos"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_medicamentos dato = new fis_rips_cargue_medicamentos();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(codPrestador))
                                        {
                                            dato.codPrestador = Convert.ToDecimal(codPrestador);
                                        }
                                    }

                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "numAutorizacion";
                                texto = Convert.ToString(consulta["numAutorizacion"]);
                                if (texto.Length <= 50)
                                {

                                    dato.numAutorizacion = (string)consulta["numAutorizacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "idMIPRES";
                                texto = Convert.ToString(consulta["idMIPRES"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.idMIPRES = (decimal)consulta["idMIPRES"];
                                    }
                                    else
                                    {
                                        dato.idMIPRES = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "fechaDispensAdmon";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaDispensAdmon"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaDispensAdmon = (DateTime?)consulta["fechaDispensAdmon"];
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

                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoRelacionado";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionado"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codDiagnosticoRelacionado = (string)consulta["codDiagnosticoRelacionado"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "tipoMedicamento";
                                texto = Convert.ToString(consulta["tipoMedicamento"]);
                                if (texto.Length <= 50)
                                {
                                    dato.tipoMedicamento = (string)consulta["tipoMedicamento"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "codTecnologiaSalud";
                                texto = Convert.ToString(consulta["codTecnologiaSalud"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codTecnologiaSalud = (string)consulta["codTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "nomTecnologiaSalud";
                                texto = Convert.ToString(consulta["nomTecnologiaSalud"]);
                                if (texto.Length <= 50)
                                {
                                    dato.nomTecnologiaSalud = (string)consulta["nomTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "concentracionMedicamento";
                                texto = Convert.ToString(consulta["concentracionMedicamento"]);
                                if (texto.Length <= 50)
                                {
                                    dato.concentracionMedicamento = (string)consulta["concentracionMedicamento"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "unidadMedida";
                                texto = Convert.ToString(consulta["unidadMedida"]);
                                if (texto.Length <= 50)
                                {
                                    dato.unidadMedida = (string)consulta["unidadMedida"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "formaFarmaceutica";
                                texto = Convert.ToString(consulta["formaFarmaceutica"]);
                                if (texto.Length <= 50)
                                {
                                    dato.formaFarmaceutica = (string)consulta["formaFarmaceutica"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "unidadMinDispensa";
                                texto = Convert.ToString(consulta["unidadMinDispensa"]);
                                if (texto.Length <= 50)
                                {
                                    dato.unidadMinDispensa = (string)consulta["unidadMinDispensa"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "cantidadMedicamento";
                                texto = Convert.ToString(consulta["cantidadMedicamento"]);
                                if (texto.Length <= 9)
                                {
                                    dato.cantidadMedicamento = (int)consulta["cantidadMedicamento"];
                                }
                                else

                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "diasTratamiento";
                                texto = Convert.ToString(consulta["diasTratamiento"]);
                                if (texto.Length <= 9)
                                {
                                    dato.diasTratamiento = (int)consulta["diasTratamiento"];
                                }
                                else

                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.tipoDocumentoIdentificacion = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["numDocumentoIdentificacion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "vrUnitMedicamento";
                                texto = Convert.ToString(consulta["vrUnitMedicamento"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.vrUnitMedicamento = (decimal)consulta["vrUnitMedicamento"];
                                    }
                                    else
                                    {
                                        dato.vrUnitMedicamento = null;
                                    }

                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "vrServicio";
                                texto = Convert.ToString(consulta["vrServicio"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                                    {
                                        dato.vrServicio = (decimal)consulta["vrServicio"];
                                    }
                                    else
                                    {
                                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "conceptoRecaudo";
                                texto = Convert.ToString(consulta["conceptoRecaudo"]);
                                if (texto.Length <= 50)
                                {
                                    dato.conceptoRecaudo = (string)consulta["conceptoRecaudo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "valorPagoModerador";
                                texto = Convert.ToString(consulta["valorPagoModerador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.valorPagoModerador = (decimal)consulta["valorPagoModerador"];
                                    }
                                    else
                                    {
                                        dato.valorPagoModerador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numFEVPagoModerador";
                                texto = Convert.ToString(consulta["numFEVPagoModerador"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numFEVPagoModerador = (string)consulta["numFEVPagoModerador"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonMedicamentos(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue MEDICAMENTOS");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en MEDICAMENTOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }

                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_otrosServicios(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_otros_servicios> listado = new List<fis_rips_cargue_otros_servicios>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["otrosServicios"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_otros_servicios dato = new fis_rips_cargue_otros_servicios();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }
                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "numAutorizacion";
                                texto = Convert.ToString(consulta["numAutorizacion"]);
                                if (texto.Length <= 50)
                                {

                                    dato.numAutorizacion = (string)consulta["numAutorizacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "idMIPRES";
                                texto = Convert.ToString(consulta["idMIPRES"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.idMIPRES = (decimal)consulta["idMIPRES"];
                                    }
                                    else
                                    {
                                        dato.idMIPRES = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "fechaSuministroTecnologia";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaSuministroTecnologia"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaSuministroTecnologia = (DateTime?)consulta["fechaSuministroTecnologia"];
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

                                columna = "tipoOS";
                                texto = Convert.ToString(consulta["tipoOS"]);
                                if (texto.Length <= 50)
                                {
                                    dato.tipoOS = (string)consulta["tipoOS"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codTecnologiaSalud";
                                texto = Convert.ToString(consulta["codTecnologiaSalud"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codTecnologiaSalud = (string)consulta["codTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "nomTecnologiaSalud";
                                texto = Convert.ToString(consulta["nomTecnologiaSalud"]);
                                if (texto.Length <= 100)
                                {
                                    dato.nomTecnologiaSalud = (string)consulta["nomTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "codTecnologiaSalud";
                                texto = Convert.ToString(consulta["codTecnologiaSalud"]);
                                if (texto.Length <= 50)
                                {
                                    dato.codTecnologiaSalud = (string)consulta["codTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "nomTecnologiaSalud";
                                texto = Convert.ToString(consulta["nomTecnologiaSalud"]);
                                if (texto.Length <= 50)
                                {
                                    dato.nomTecnologiaSalud = (string)consulta["nomTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "cantidadOS";
                                texto = Convert.ToString(consulta["cantidadOS"]);
                                if (texto.Length <= 9)
                                {
                                    dato.cantidadOS = (int)consulta["cantidadOS"];
                                }
                                else

                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.tipoDocumentoIdentificacion = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["numDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "vrUnitOS";
                                texto = Convert.ToString(consulta["vrUnitOS"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.vrUnitOS = (decimal)consulta["vrUnitOS"];
                                    }
                                    else
                                    {
                                        dato.vrUnitOS = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "vrServicio";
                                texto = Convert.ToString(consulta["vrServicio"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                                    {
                                        dato.vrServicio = (decimal)consulta["vrServicio"];
                                    }
                                    else
                                    {
                                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                                        throw new Exception(textError);
                                    }

                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "conceptoRecaudo";
                                texto = Convert.ToString(consulta["conceptoRecaudo"]);
                                if (texto.Length <= 50)
                                {
                                    dato.conceptoRecaudo = (string)consulta["conceptoRecaudo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "valorPagoModerador";
                                texto = Convert.ToString(consulta["valorPagoModerador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.valorPagoModerador = (decimal)consulta["valorPagoModerador"];
                                    }
                                    else
                                    {
                                        dato.valorPagoModerador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numFEVPagoModerador";
                                texto = Convert.ToString(consulta["numFEVPagoModerador"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numFEVPagoModerador = (string)consulta["numFEVPagoModerador"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonotrosServicios(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue SERVICIOS");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en OTROS SERVICIOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }

                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_procedimientos(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_procedimientos> listado = new List<fis_rips_cargue_procedimientos>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["procedimientos"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_procedimientos dato = new fis_rips_cargue_procedimientos();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }
                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                //columna = "fechaInicioAtencion";
                                //try
                                //{
                                //    fechas = Convert.ToDateTime(consulta["fechaInicioAtencion"]);
                                //    if (fechas != null)
                                //    {
                                //        dato.fechaInicioAtencion = (DateTime?)consulta["fechaInicioAtencion"];
                                //    }
                                //    else
                                //    {
                                //        textError = columna + ", no puede ir vacio";
                                //        throw new Exception(textError);
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    textError = columna + ", tiene formato incorrecto";
                                //    throw new Exception(textError);
                                //}
                                columna = "fechaInicioAtencion";
                                try
                                {
                                    var datoPrueba = consulta["fechaInicioAtencion"];

                                    if (datoPrueba != null)
                                    {
                                        string fechaTexto = datoPrueba.ToString().Trim();
                                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                        if (!string.IsNullOrEmpty(fechaTexto))
                                        {
                                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                            string[] formatos = {
                "d/M/yyyy hh:mm:ss tt",   // Día y mes como uno o dos dígitos
                "d/M/yyyy HH:mm:ss",
                "M/d/yyyy hh:mm:ss tt",   // Mes como uno o dos dígitos
                "M/d/yyyy HH:mm:ss",
                "d/M/yyyy",
                "M/d/yyyy",
                "dd/MM/yyyy HH:mm:ss", // Formato original para día y mes en dos dígitos
                "MM/dd/yyyy hh:mm:ss tt",
                "dd/MM/yyyy HH:mm:ss",
                "MM/dd/yyyy HH:mm:ss"
            };

                                            DateTime fechaEspecial;

                                            // Probar los diferentes formatos
                                            bool fechaValida = DateTime.TryParseExact(
                                                fechaTexto,
                                                formatos,
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                                out fechaEspecial
                                            );

                                            if (fechaValida)
                                            {
                                                dato.fechaInicioAtencion = fechaEspecial;
                                            }
                                            else
                                            {
                                                textError = columna + ", tiene formato incorrecto";
                                                throw new Exception(textError);
                                            }
                                        }
                                        else
                                        {
                                            textError = columna + ", no puede ir vacío";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }


                                columna = "idMIPRES";
                                texto = Convert.ToString(consulta["idMIPRES"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.idMIPRES = (decimal)consulta["idMIPRES"];
                                    }
                                    else
                                    {
                                        dato.idMIPRES = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "numAutorizacion";
                                texto = Convert.ToString(consulta["numAutorizacion"]);
                                if (texto.Length <= 50)
                                {

                                    dato.numAutorizacion = (string)consulta["numAutorizacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codProcedimiento";
                                texto = Convert.ToString(consulta["codProcedimiento"]);
                                if (texto.Length <= 100)
                                {

                                    dato.codProcedimiento = (string)consulta["codProcedimiento"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "viaIngresoServicioSalud";
                                texto = Convert.ToString(consulta["viaIngresoServicioSalud"]);
                                if (texto.Length <= 100)
                                {
                                    dato.viaIngresoServicioSalud = (string)consulta["viaIngresoServicioSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "modalidadGrupoServicioTecSal";
                                texto = Convert.ToString(consulta["modalidadGrupoServicioTecSal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.modalidadGrupoServicioTecSal = (string)consulta["modalidadGrupoServicioTecSal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "grupoServicios";
                                texto = Convert.ToString(consulta["grupoServicios"]);
                                if (texto.Length <= 100)
                                {
                                    dato.grupoServicios = (string)consulta["grupoServicios"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codServicio";
                                texto = Convert.ToString(consulta["codServicio"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codServicio = (string)consulta["codServicio"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "finalidadTecnologiaSalud";
                                texto = Convert.ToString(consulta["finalidadTecnologiaSalud"]);
                                if (texto.Length <= 100)
                                {
                                    dato.finalidadTecnologiaSalud = (string)consulta["finalidadTecnologiaSalud"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.finalidadTecnologiaSalud = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.tipoDocumentoIdentificacion = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["numDocumentoIdentificacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoRelacionado";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionado"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionado = (string)consulta["codDiagnosticoRelacionado"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codComplicacion";
                                texto = Convert.ToString(consulta["codComplicacion"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codComplicacion = (string)consulta["codComplicacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "vrServicio";
                                texto = Convert.ToString(consulta["vrServicio"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                                    {
                                        dato.vrServicio = (decimal?)consulta["vrServicio"];
                                    }
                                    else
                                    {
                                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "conceptoRecaudo";
                                texto = Convert.ToString(consulta["conceptoRecaudo"]);
                                if (texto.Length <= 50)
                                {
                                    dato.conceptoRecaudo = (string)consulta["conceptoRecaudo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "valorPagoModerador";
                                texto = Convert.ToString(consulta["valorPagoModerador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.valorPagoModerador = (decimal)consulta["valorPagoModerador"];
                                    }
                                    else
                                    {
                                        dato.valorPagoModerador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numFEVPagoModerador";
                                texto = Convert.ToString(consulta["numFEVPagoModerador"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numFEVPagoModerador = (string)consulta["numFEVPagoModerador"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonProcedimientos(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue PROCEDIMIENTOS");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en PROCEDIMIENTOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }

                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_recienNacidos(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_reciennacido> listado = new List<fis_rips_cargue_reciennacido>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["recienNacidos"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_reciennacido dato = new fis_rips_cargue_reciennacido();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }
                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }



                                columna = "tipoDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["tipoDocumentoIdentificacion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.tipoDocumentoIdentificacion = (string)consulta["tipoDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numDocumentoIdentificacion";
                                texto = Convert.ToString(consulta["numDocumentoIdentificacion"]);
                                if (texto.Length <= 50)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numDocumentoIdentificacion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "fechaNacimiento";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaNacimiento"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaNacimiento = (DateTime?)consulta["fechaNacimiento"];
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


                                columna = "edadGestacional";
                                texto = Convert.ToString(consulta["edadGestacional"]);
                                if (texto.Length <= 9)
                                {
                                    dato.edadGestacional = (int)consulta["edadGestacional"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numConsultasCPrenatal";
                                texto = Convert.ToString(consulta["numConsultasCPrenatal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numDocumentoIdentificacion = (string)consulta["numConsultasCPrenatal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codSexoBiologico";
                                texto = Convert.ToString(consulta["codSexoBiologico"]);
                                if (texto.Length <= 15)
                                {
                                    dato.codSexoBiologico = (string)consulta["codSexoBiologico"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 15 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "peso";
                                texto = Convert.ToString(consulta["peso"]);
                                if (texto.Length <= 9)
                                {
                                    dato.peso = (int)consulta["peso"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "condicionDestinoUsuarioEgreso";
                                texto = Convert.ToString(consulta["condicionDestinoUsuarioEgreso"]);
                                if (texto.Length <= 100)
                                {
                                    dato.condicionDestinoUsuarioEgreso = (string)consulta["condicionDestinoUsuarioEgreso"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoCausaMuerte";
                                texto = Convert.ToString(consulta["codDiagnosticoCausaMuerte"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoCausaMuerte = (string)consulta["codDiagnosticoCausaMuerte"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "fechaEgreso";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaEgreso"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaEgreso = (DateTime?)consulta["fechaEgreso"];
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


                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonRecienNacido(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue RECÍEN NACIDO");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en RECÍEN NACIDO conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }
                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_transaccion(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_transaccion> listado = new List<fis_rips_cargue_transaccion>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["transaccion"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_transaccion dato = new fis_rips_cargue_transaccion();

                                columna = "numDocumentoIdObligado";
                                texto = Convert.ToString(consulta["numDocumentoIdObligado"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numDocumentoIdObligado = (string)consulta["numDocumentoIdObligado"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "numFactura";
                                texto = Convert.ToString(consulta["numFactura"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numFactura = (string)consulta["numFactura"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "tipoNota";
                                texto = Convert.ToString(consulta["tipoNota"]);
                                if (texto.Length <= 100)
                                {
                                    dato.tipoNota = (string)consulta["tipoNota"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "numNota";
                                texto = Convert.ToString(consulta["numNota"]);
                                if (texto.Length <= 100)
                                {
                                    dato.numNota = (string)consulta["numNota"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonTransaccion(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos en cargue TRANSACCIÓN");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en TRANSACCIÓN conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }
                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_urgencias(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_urgencias> listado = new List<fis_rips_cargue_urgencias>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["urgencias"];

                        try
                        {
                            // Iterar sobre cada consulta
                            foreach (JObject consulta in consultas)
                            {
                                var texto = "";
                                var numero = 0;
                                DateTime fechas = new DateTime();
                                decimal decima = new decimal();

                                fis_rips_cargue_urgencias dato = new fis_rips_cargue_urgencias();

                                columna = "codPrestador";
                                texto = Convert.ToString(consulta["codPrestador"]);
                                if (texto.Length <= 13)
                                {
                                    if (texto != "null" && texto != null && texto != "")
                                    {
                                        dato.codPrestador = (decimal)consulta["codPrestador"];

                                        if (texto != codPrestador)
                                        {
                                            throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                                        }
                                    }
                                    else
                                    {
                                        dato.codPrestador = null;
                                    }
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }


                                //columna = "fechaInicioAtencion";
                                //try
                                //{
                                //    fechas = Convert.ToDateTime(consulta["fechaInicioAtencion"]);
                                //    if (fechas != null)
                                //    {
                                //        dato.fechaInicioAtencion = (DateTime?)consulta["fechaInicioAtencion"];
                                //    }
                                //    else
                                //    {
                                //        textError = columna + ", no puede ir vacio";
                                //        throw new Exception(textError);
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    textError = columna + ", tiene formato incorrecto";
                                //    throw new Exception(textError);
                                //}
                                columna = "fechaInicioAtencion";
                                try
                                {
                                    var datoPrueba = consulta["fechaInicioAtencion"];

                                    if (datoPrueba != null)
                                    {
                                        string fechaTexto = datoPrueba.ToString().Trim();
                                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                        if (!string.IsNullOrEmpty(fechaTexto))
                                        {
                                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                            string[] formatos = {
                "d/M/yyyy hh:mm:ss tt",   // Día y mes como uno o dos dígitos
                "d/M/yyyy HH:mm:ss",
                "M/d/yyyy hh:mm:ss tt",   // Mes como uno o dos dígitos
                "M/d/yyyy HH:mm:ss",
                "d/M/yyyy",
                "M/d/yyyy",
                "dd/MM/yyyy HH:mm:ss", // Formato original para día y mes en dos dígitos
                "MM/dd/yyyy hh:mm:ss tt",
                "dd/MM/yyyy HH:mm:ss",
                "MM/dd/yyyy HH:mm:ss"
            };

                                            DateTime fechaEspecial;

                                            // Probar los diferentes formatos
                                            bool fechaValida = DateTime.TryParseExact(
                                                fechaTexto,
                                                formatos,
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                                out fechaEspecial
                                            );

                                            if (fechaValida)
                                            {
                                                dato.fechaInicioAtencion = fechaEspecial;
                                            }
                                            else
                                            {
                                                textError = columna + ", tiene formato incorrecto";
                                                throw new Exception(textError);
                                            }
                                        }
                                        else
                                        {
                                            textError = columna + ", no puede ir vacío";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }


                                columna = "causaMotivoAtencion";
                                texto = Convert.ToString(consulta["causaMotivoAtencion"]);
                                if (texto.Length <= 9)
                                {
                                    dato.causaMotivoAtencion = (int)consulta["causaMotivoAtencion"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoPrincipal";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipal"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoPrincipal = (string)consulta["codDiagnosticoPrincipal"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoPrincipalE";
                                texto = Convert.ToString(consulta["codDiagnosticoPrincipalE"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoPrincipalE = (string)consulta["codDiagnosticoPrincipalE"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoRelacionadoE1";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE1"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionadoE1 = (string)consulta["codDiagnosticoRelacionadoE1"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoRelacionadoE2";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE2"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionadoE2 = (string)consulta["codDiagnosticoRelacionadoE2"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "codDiagnosticoRelacionadoE3";
                                texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE3"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoRelacionadoE3 = (string)consulta["codDiagnosticoRelacionadoE3"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "condicionDestinoUsuarioEgreso";
                                texto = Convert.ToString(consulta["condicionDestinoUsuarioEgreso"]);
                                if (texto.Length <= 100)
                                {
                                    dato.condicionDestinoUsuarioEgreso = (string)consulta["condicionDestinoUsuarioEgreso"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }

                                columna = "codDiagnosticoCausaMuerte";
                                texto = Convert.ToString(consulta["codDiagnosticoCausaMuerte"]);
                                if (texto.Length <= 100)
                                {
                                    dato.codDiagnosticoCausaMuerte = (string)consulta["codDiagnosticoCausaMuerte"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 100 caracteres.";
                                    throw new Exception(textError);
                                }


                                columna = "fechaEgreso";
                                try
                                {
                                    fechas = Convert.ToDateTime(consulta["fechaEgreso"]);
                                    if (fechas != null)
                                    {
                                        dato.fechaEgreso = (DateTime?)consulta["fechaEgreso"];
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


                                columna = "consecutivo";
                                texto = Convert.ToString(consulta["consecutivo"]);
                                if (texto.Length <= 13)
                                {
                                    dato.consecutivo = (int)consulta["consecutivo"];
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 13 caracteres.";
                                    throw new Exception(textError);
                                }

                                dato.fecha_digita = DateTime.Now;
                                dato.usuario_digita = SesionVar.UserName;
                                listado.Add(dato);

                                fila++;
                            }

                            if (listado.Count() > 0)
                            {
                                var Inserta = BusClass.GuardarCargueJsonUrgencias(cargue, listado);
                                if (Inserta != 0)
                                {
                                    mensajeResultado = "Datos del archivo procesados correctamente.";
                                }
                                else
                                {
                                    mensajeResultado = "Error al procesar el archivo";
                                }
                            }
                            //else
                            //{
                            //    throw new Exception("No se encontraron datos");
                            //}

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos en cargue URGENCIAS"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en URGENCIAS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }
                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }

        public string InsertarTipo_usuarios(HttpPostedFileBase archivo, fis_rips_cargue_LoteConsultas cargue, string codPrestador)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                List<fis_rips_cargue_usuarios> listado = new List<fis_rips_cargue_usuarios>();

                // Verificar si se proporcionó un archivo
                if (archivo != null)
                {
                    // Leer el archivo y convertirlo a una cadena JSON
                    using (var reader = new StreamReader(archivo.InputStream))
                    {
                        var jsonString = reader.ReadToEnd();

                        // Convertir la cadena JSON a un objeto JObject
                        JObject jsonObject = JObject.Parse(jsonString);

                        // Acceder a la propiedad "consultas" que contiene la lista de consultas
                        JArray consultas = (JArray)jsonObject["usuarios"];

                        try
                        {

                            if (consultas.Count() > 0)
                            {
                                // Iterar sobre cada consulta
                                foreach (JObject consulta in consultas)
                                {
                                    var texto = "";
                                    var numero = 0;
                                    DateTime fechas = new DateTime();
                                    decimal decima = new decimal();

                                    fis_rips_cargue_usuarios dato = new fis_rips_cargue_usuarios();

                                    columna = "TipoDocumentoIdentificacion";
                                    texto = Convert.ToString(consulta["TipoDocumentoIdentificacion"]);
                                    if (texto.Length <= 100)
                                    {

                                        dato.tipoDocumentoIdentificacion = (string)consulta["TipoDocumentoIdentificacion"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    columna = "NumDocumentoIdentificacion";
                                    texto = Convert.ToString(consulta["NumDocumentoIdentificacion"]);
                                    if (texto.Length <= 100)
                                    {

                                        dato.numDocumentoIdentificacion = (string)consulta["NumDocumentoIdentificacion"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }


                                    columna = "tipoUsuario";
                                    texto = Convert.ToString(consulta["tipoUsuario"]);
                                    if (texto.Length <= 100)
                                    {

                                        dato.tipoUsuario = (string)consulta["tipoUsuario"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }


                                    columna = "fechaNacimiento";
                                    try
                                    {
                                        fechas = Convert.ToDateTime(consulta["fechaNacimiento"]);
                                        if (fechas != null)
                                        {
                                            dato.fechaNacimiento = (DateTime?)consulta["fechaNacimiento"];
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


                                    columna = "codSexo";
                                    texto = Convert.ToString(consulta["codSexo"]);
                                    if (texto.Length <= 100)
                                    {

                                        dato.codSexo = (string)consulta["codSexo"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }



                                    columna = "codPaisResidencia";
                                    texto = Convert.ToString(consulta["codPaisResidencia"]);
                                    if (texto.Length <= 100)
                                    {
                                        dato.codPaisResidencia = (string)consulta["codPaisResidencia"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }


                                    columna = "codMunicipioResidencia";
                                    texto = Convert.ToString(consulta["codMunicipioResidencia"]);
                                    if (texto.Length <= 100)
                                    {
                                        dato.codMunicipioResidencia = (string)consulta["codMunicipioResidencia"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    columna = "codZonaTerritorialResidencia";
                                    texto = Convert.ToString(consulta["codDiagnosticoRelacionadoE1"]);
                                    if (texto.Length <= 100)
                                    {
                                        dato.codZonaTerritorialResidencia = (string)consulta["codZonaTerritorialResidencia"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    columna = "incapacidad";
                                    texto = Convert.ToString(consulta["incapacidad"]);
                                    if (texto.Length <= 100)
                                    {
                                        dato.incapacidad = (string)consulta["incapacidad"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 100 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    columna = "consecutivo";
                                    texto = Convert.ToString(consulta["consecutivo"]);
                                    if (texto.Length <= 13)
                                    {
                                        dato.consecutivo = (int)consulta["consecutivo"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 13 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    columna = "codPaisOrigen";
                                    texto = Convert.ToString(consulta["codPaisOrigen"]);
                                    if (texto.Length <= 50)
                                    {
                                        dato.codPaisOrigen = (string)consulta["codPaisOrigen"];
                                    }
                                    else
                                    {
                                        textError = columna + ", solo puede contener 50 caracteres.";
                                        throw new Exception(textError);
                                    }

                                    dato.fecha_digita = DateTime.Now;
                                    dato.usuario_digita = SesionVar.UserName;
                                    listado.Add(dato);

                                    fila++;
                                }

                                if (listado.Count() > 0)
                                {
                                    var Inserta = BusClass.GuardarCargueJsonUsuarios(cargue, listado);
                                    if (Inserta != 0)
                                    {
                                        mensajeResultado = "Datos del archivo procesados correctamente.";
                                    }
                                    else
                                    {
                                        mensajeResultado = "Error al procesar el archivo";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                            var mensaje = "";
                            if (error.Contains("No se encontraron datos"))
                            {
                                mensaje = error;
                            }
                            else
                            {
                                mensaje = "Error en USUARIOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                            }

                            throw new Exception(mensaje);
                        }
                    }
                }
                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return mensajeResultado;
        }
    }



    public class CUPSUNICOS
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

        #endregion PROPIEDADES

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

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

        public string mensajeResultado { get; set; }

        public fis_rips_cargue_transaccion ValidarEstructuraTransaccion(JObject TransaE, string numFactura, string codPrestador)
        {
            string columna = "";
            int fila = 1;
            var textError = "";

            fis_rips_cargue_transaccion dato = new fis_rips_cargue_transaccion();

            try
            {
                // Verificar si se proporcionó un archivo
                if (TransaE != null)
                {
                    try
                    {
                        var texto = "";
                        var numero = 0;
                        DateTime fechas = new DateTime();
                        decimal decima = new decimal();

                        columna = "numDocumentoIdObligado";
                        texto = Convert.ToString(TransaE["numDocumentoIdObligado"]);
                        if (texto.Length <= 100)
                        {
                            dato.numDocumentoIdObligado = (string)TransaE["numDocumentoIdObligado"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "numFactura";
                        texto = Convert.ToString(TransaE["numFactura"]);
                        if (texto.Length <= 100)
                        {
                            dato.numFactura = (string)TransaE["numFactura"];

                            if (dato.numFactura != numFactura)
                            {
                                throw new Exception($"{columna}, Num factura de transacción no es el mismo num factura de la factura.");
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "tipoNota";
                        texto = Convert.ToString(TransaE["tipoNota"]);
                        if (texto.Length <= 100)
                        {
                            dato.tipoNota = (string)TransaE["tipoNota"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "numNota";
                        texto = Convert.ToString(TransaE["numNota"]);
                        if (texto.Length <= 100)
                        {
                            dato.numNota = (string)TransaE["numNota"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        dato.fecha_digita = DateTime.Now;
                        dato.usuario_digita = SesionVar.UserName;

                        if (dato != null)
                        {
                            return dato;
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                        var mensaje = "";
                        if (error.Contains("No se encontraron datos"))
                        {
                            mensaje = error;
                        }
                        else
                        {
                            mensaje = "Error en TRANSACCIÓN conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                        }

                        throw new Exception(mensaje);
                    }
                }

                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return dato;
        }

        public fis_rips_cargue_usuarios ValidarEstructuraUsuario(JObject UsuarioE, string codPrestador)
        {
            string columna = "";
            int fila = 1;
            var textError = "";

            fis_rips_cargue_usuarios dato = new fis_rips_cargue_usuarios();
            try
            {
                // Verificar si se proporcionó un archivo
                if (UsuarioE != null)
                {
                    try
                    {
                        var texto = "";
                        var numero = 0;
                        DateTime fechas = new DateTime();
                        decimal decima = new decimal();

                        columna = "tipoDocumentoIdentificacion";
                        texto = Convert.ToString(UsuarioE["tipoDocumentoIdentificacion"]);
                        if (texto.Length <= 100)
                        {

                            dato.tipoDocumentoIdentificacion = (string)UsuarioE["tipoDocumentoIdentificacion"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "numDocumentoIdentificacion";
                        texto = Convert.ToString(UsuarioE["numDocumentoIdentificacion"]);
                        if (texto.Length <= 100)
                        {

                            dato.numDocumentoIdentificacion = (string)UsuarioE["numDocumentoIdentificacion"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "tipoUsuario";
                        texto = Convert.ToString(UsuarioE["tipoUsuario"]);
                        if (texto.Length <= 100)
                        {

                            dato.tipoUsuario = (string)UsuarioE["tipoUsuario"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        //columna = "fechaNacimiento";
                        //try
                        //{
                        //    fechas = Convert.ToDateTime(UsuarioE["fechaNacimiento"]);
                        //    if (fechas != null)
                        //    {
                        //        dato.fechaNacimiento = (DateTime?)UsuarioE["fechaNacimiento"];
                        //    }
                        //    else
                        //    {
                        //        textError = columna + ", no puede ir vacio";
                        //        throw new Exception(textError);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    textError = columna + ", tiene formato incorrecto";
                        //    throw new Exception(textError);
                        //}


                        columna = "fechaNacimiento";
                        try
                        {
                            var datoPrueba = UsuarioE["fechaNacimiento"];

                            if (datoPrueba != null)
                            {
                                string fechaTexto = datoPrueba.ToString().Trim();
                                fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                if (!string.IsNullOrEmpty(fechaTexto))
                                {
                                    // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                    string[] formatos = {
                                        "yyyy/MM/dd",
                                        "yyyy-MM-dd",
                                        "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas
                                    };

                                    DateTime fechaEspecial;

                                    // Probar los diferentes formatos
                                    bool fechaValida = DateTime.TryParseExact(
                                        fechaTexto,
                                        formatos,
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                        out fechaEspecial
                                    );

                                    if (fechaValida)
                                    {
                                        dato.fechaNacimiento = fechaEspecial;
                                    }
                                    else
                                    {
                                        textError = columna + ", tiene formato incorrecto";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", no puede ir vacío";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", tiene formato incorrecto";
                            throw new Exception(textError);
                        }


                        columna = "codSexo";
                        texto = Convert.ToString(UsuarioE["codSexo"]);
                        if (texto.Length <= 100)
                        {

                            dato.codSexo = (string)UsuarioE["codSexo"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "codPaisResidencia";
                        texto = Convert.ToString(UsuarioE["codPaisResidencia"]);
                        if (texto.Length <= 100)
                        {
                            dato.codPaisResidencia = (string)UsuarioE["codPaisResidencia"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "codMunicipioResidencia";
                        texto = Convert.ToString(UsuarioE["codMunicipioResidencia"]);
                        if (texto.Length <= 100)
                        {
                            dato.codMunicipioResidencia = (string)UsuarioE["codMunicipioResidencia"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "codZonaTerritorialResidencia";
                        texto = Convert.ToString(UsuarioE["codDiagnosticoRelacionadoE1"]);
                        if (texto.Length <= 100)
                        {
                            dato.codZonaTerritorialResidencia = (string)UsuarioE["codZonaTerritorialResidencia"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "incapacidad";
                        texto = Convert.ToString(UsuarioE["incapacidad"]);
                        if (texto.Length <= 100)
                        {
                            dato.incapacidad = (string)UsuarioE["incapacidad"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "consecutivo";
                        texto = Convert.ToString(UsuarioE["consecutivo"]);
                        if (texto.Length <= 13)
                        {
                            dato.consecutivo = (int)UsuarioE["consecutivo"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 13 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "codPaisOrigen";
                        texto = Convert.ToString(UsuarioE["codPaisOrigen"]);
                        if (texto.Length <= 50)
                        {
                            dato.codPaisOrigen = (string)UsuarioE["codPaisOrigen"];
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        dato.fecha_digita = DateTime.Now;
                        dato.usuario_digita = SesionVar.UserName;

                        if (dato != null)
                        {
                            return dato;
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                        var mensaje = "";
                        if (error.Contains("No se encontraron datos"))
                        {
                            mensaje = error;
                        }
                        else
                        {
                            mensaje = "Error en USUARIOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                        }

                        throw new Exception(mensaje);
                    }
                }

                else
                {
                    mensajeResultado = "No se proporcionó un archivo o el archivo está vacío.";
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                mensajeResultado = "Error al procesar el archivo: " + error;
            }

            return dato;
        }

        public fis_rips_cargue_consulta ValidarEstructuraConsulta(JObject ConsultaE, string codPrestador, List<fis_rips_cups> listadoCups, int? fila)
        {
            string columna = "";
            var textError = "";
            fis_rips_cargue_consulta dato = new fis_rips_cargue_consulta();


            try
            {
                if (ConsultaE != null)
                {
                    var texto = "";
                    var numero = 0;
                    DateTime fechas = new DateTime();
                    decimal decima = new decimal();

                    columna = "codPrestador";
                    texto = Convert.ToString(ConsultaE["codPrestador"]);
                    if (texto.Length <= 13)
                    {

                        if (texto != "null" && texto != null && texto != "")
                        {
                            dato.codPrestador = (decimal)ConsultaE["codPrestador"];

                            //if (texto != codPrestador)
                            //{
                            //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura: " + codPrestador);
                            //}

                        }
                        else
                        {
                            dato.codPrestador = null;
                        }
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 13 caracteres.";
                        throw new Exception(textError);
                    }

                    //columna = "fechaInicioAtencion";
                    //try
                    //{
                    //    fechas = Convert.ToDateTime(ConsultaE["fechaInicioAtencion"]);
                    //    if (fechas != null)
                    //    {
                    //        dato.fechaInicioAtencion = (DateTime?)ConsultaE["fechaInicioAtencion"];
                    //    }
                    //    else
                    //    {
                    //        textError = columna + ", no puede ir vacio";
                    //        throw new Exception(textError);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    textError = columna + ", tiene formato incorrecto";
                    //    throw new Exception(textError);
                    //}

                    columna = "fechaInicioAtencion";
                    try
                    {
                        var datoPrueba = ConsultaE["fechaInicioAtencion"];

                        if (datoPrueba != null)
                        {
                            string fechaTexto = datoPrueba.ToString().Trim();
                            fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                            if (!string.IsNullOrEmpty(fechaTexto))
                            {
                                string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };

                                DateTime fechaEspecial;

                                // Probar los diferentes formatos
                                bool fechaValida = DateTime.TryParseExact(
                                    fechaTexto,
                                    formatos,
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                    out fechaEspecial
                                );

                                if (fechaValida)
                                {
                                    dato.fechaInicioAtencion = fechaEspecial;
                                    dato.fecha_prestacion = fechaEspecial;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", no puede ir vacío";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    catch (Exception ex)
                    {
                        textError = columna + ", tiene formato incorrecto";
                        throw new Exception(textError);
                    }

                    columna = "numAutorizacion";
                    texto = Convert.ToString(ConsultaE["numAutorizacion"]);
                    if (texto.Length <= 100)
                    {

                        dato.numAutorizacion = (string)ConsultaE["numAutorizacion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "codConsulta";
                    try
                    {
                        texto = Convert.ToString(ConsultaE["codConsulta"]);
                        if (texto.Length <= 13)
                        {
                            dato.codConsulta = Convert.ToString(ConsultaE["codConsulta"]);
                            string codigoLimpio = dato.codConsulta.Replace("-0", "-");
                            if (codigoLimpio.StartsWith("0"))
                            {
                                codigoLimpio = codigoLimpio.Substring(1);
                            }

                            fis_rips_cups cups = listadoCups.FirstOrDefault(x => x.codigo_cups == codigoLimpio);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 13 caracteres.";
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

                    columna = "modalidadGrupoServicioTecSal";
                    texto = Convert.ToString(ConsultaE["modalidadGrupoServicioTecSal"]);
                    if (texto.Length <= 100)
                    {
                        dato.modalidadGrupoServicioTecSal = (string)ConsultaE["modalidadGrupoServicioTecSal"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "grupoServicios";
                    texto = Convert.ToString(ConsultaE["grupoServicios"]);
                    if (texto.Length <= 100)
                    {
                        dato.grupoServicios = (string)ConsultaE["grupoServicios"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codServicio";
                    texto = Convert.ToString(ConsultaE["codServicio"]);
                    if (texto.Length <= 100)
                    {
                        dato.codServicio = (string)ConsultaE["codServicio"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "finalidadTecnologiaSalud";
                    texto = Convert.ToString(ConsultaE["finalidadTecnologiaSalud"]);
                    if (texto.Length <= 100)
                    {
                        dato.finalidadTecnologiaSalud = (string)ConsultaE["finalidadTecnologiaSalud"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "causaMotivoAtencion";
                    texto = Convert.ToString(ConsultaE["causaMotivoAtencion"]);
                    if (texto.Length <= 100)
                    {
                        dato.causaMotivoAtencion = (string)ConsultaE["causaMotivoAtencion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoPrincipal";
                    texto = Convert.ToString(ConsultaE["codDiagnosticoPrincipal"]);
                    if (texto.Length <= 100)
                    {
                        dato.codDiagnosticoPrincipal = (string)ConsultaE["codDiagnosticoPrincipal"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoRelacionado1";
                    texto = Convert.ToString(ConsultaE["codDiagnosticoRelacionado1"]);
                    if (texto.Length <= 100)
                    {
                        dato.codDiagnosticoRelacionado1 = (string)ConsultaE["codDiagnosticoRelacionado1"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "codDiagnosticoRelacionado2";
                    texto = Convert.ToString(ConsultaE["codDiagnosticoRelacionado2"]);
                    if (texto.Length <= 100)
                    {
                        dato.codDiagnosticoRelacionado2 = (string)ConsultaE["codDiagnosticoRelacionado2"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "codDiagnosticoRelacionado3";
                    texto = Convert.ToString(ConsultaE["codDiagnosticoRelacionado3"]);
                    if (texto.Length <= 100)
                    {
                        dato.codDiagnosticoRelacionado3 = (string)ConsultaE["codDiagnosticoRelacionado3"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "tipoDiagnosficoPrincipal";
                    texto = Convert.ToString(ConsultaE["tipoDiagnosficoPrincipal"]);
                    if (texto.Length <= 100)
                    {
                        dato.tipoDiagnosticoPrincipal = (string)ConsultaE["tipoDiagnosficoPrincipal"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "tipoDocumentoIdentificacion";
                    texto = Convert.ToString(ConsultaE["tipoDocumentoIdentificacion"]);
                    if (texto.Length <= 100)
                    {
                        dato.tipoDocumentoIdentificacion = (string)ConsultaE["tipoDocumentoIdentificacion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "numDocumentoIdentificacion";
                    texto = Convert.ToString(ConsultaE["numDocumentoIdentificacion"]);
                    if (texto.Length <= 100)
                    {
                        dato.numDocumentoIdentificacion = (string)ConsultaE["numDocumentoIdentificacion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "vrServicio";
                    texto = Convert.ToString(ConsultaE["vrServicio"]);
                    if (texto.Length <= 13)
                    {
                        if (texto != "null" && texto != null && texto != "" && texto != "0")
                        {
                            dato.vrServicio = (decimal)ConsultaE["vrServicio"];
                        }
                        else
                        {
                            textError = columna + ", no pueden venir valores en 0 o vacíos.";
                            throw new Exception(textError);
                        }
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 13 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "conceptoRecaudo";
                    texto = Convert.ToString(ConsultaE["conceptoRecaudo"]);
                    if (texto.Length <= 100)
                    {
                        dato.conceptoRecaudo = (string)ConsultaE["conceptoRecaudo"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "valorPagoModerador";
                    texto = Convert.ToString(ConsultaE["valorPagoModerador"]);
                    if (texto.Length <= 13)
                    {
                        if (texto != "null" && texto != null && texto != "" && texto != "0")
                        {
                            dato.valorPagoModerador = (decimal)ConsultaE["valorPagoModerador"];
                        }
                        else
                        {
                            dato.valorPagoModerador = null;
                        }

                    }
                    else
                    {
                        textError = columna + ", solo puede contener 13 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "numFEVPagoModerador";
                    texto = Convert.ToString(ConsultaE["numFEVPagoModerador"]);
                    if (texto.Length <= 100)
                    {
                        dato.numFEVPagoModerador = (string)ConsultaE["numFEVPagoModerador"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "consecutivo";
                    texto = Convert.ToString(ConsultaE["consecutivo"]);
                    if (texto.Length <= 13)
                    {
                        if (texto != "null" && texto != null && texto != "")
                        {
                            dato.consecutivo = (decimal)ConsultaE["consecutivo"];
                        }
                        else
                        {
                            dato.consecutivo = null;
                        }
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 13 caracteres.";
                        throw new Exception(textError);
                    }

                    dato.cantidad = 1;
                    dato.fecha_digita = DateTime.Now;
                    dato.usuario_digita = SesionVar.UserName;
                }
            }

            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en CONSULTAS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }

                mensajeResultado = mensaje;
                throw new Exception(mensaje);
            }

            return dato;
        }

        public fis_rips_cargue_hospitalizacion ValidarEstructuraHospitalizacion(JObject HospitalizacionE, string codPrestador, int? fila)
        {
            string columna = "";
            var textError = "";
            fis_rips_cargue_hospitalizacion dato = new fis_rips_cargue_hospitalizacion();

            try
            {
                if (HospitalizacionE != null)
                {
                    var texto = "";
                    var numero = 0;
                    DateTime fechas = new DateTime();
                    decimal decima = new decimal();

                    columna = "codPrestador";
                    texto = Convert.ToString(HospitalizacionE["codPrestador"]);
                    if (texto.Length <= 13)
                    {

                        if (texto != "null" && texto != null && texto != "")
                        {
                            dato.codPrestador = (decimal)HospitalizacionE["codPrestador"];

                            //if (texto != codPrestador)
                            //{
                            //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                            //}

                        }
                        else
                        {
                            dato.codPrestador = null;
                        }
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 13 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "viaIngresoServicioSalud";
                    texto = Convert.ToString(HospitalizacionE["viaIngresoServicioSalud"]);
                    if (texto.Length <= 50)
                    {

                        dato.viaIngresoServicioSalud = (string)HospitalizacionE["viaIngresoServicioSalud"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }

                    //columna = "fechaInicioAtencion";
                    //try
                    //{
                    //    fechas = Convert.ToDateTime(HospitalizacionE["fechaInicioAtencion"]);
                    //    if (fechas != null)
                    //    {
                    //        dato.fechaInicioAtencion = (DateTime?)HospitalizacionE["fechaInicioAtencion"];
                    //    }
                    //    else
                    //    {
                    //        textError = columna + ", no puede ir vacio";
                    //        throw new Exception(textError);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    textError = columna + ", tiene formato incorrecto";
                    //    throw new Exception(textError);
                    //}
                    columna = "fechaInicioAtencion";
                    try
                    {
                        var datoPrueba = HospitalizacionE["fechaInicioAtencion"];

                        if (datoPrueba != null)
                        {
                            string fechaTexto = datoPrueba.ToString().Trim();
                            fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                            if (!string.IsNullOrEmpty(fechaTexto))
                            {
                                // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };

                                DateTime fechaEspecial;

                                // Probar los diferentes formatos
                                bool fechaValida = DateTime.TryParseExact(
                                    fechaTexto,
                                    formatos,
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                    out fechaEspecial
                                );

                                if (fechaValida)
                                {
                                    dato.fechaInicioAtencion = fechaEspecial;
                                    dato.fecha_prestacion = fechaEspecial;
                                }
                                else
                                {
                                    textError = columna + ", tiene formato incorrecto";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", no puede ir vacío";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    catch (Exception ex)
                    {
                        textError = columna + ", tiene formato incorrecto";
                        throw new Exception(textError);
                    }


                    columna = "numAutorizacion";
                    texto = Convert.ToString(HospitalizacionE["numAutorizacion"]);
                    if (texto.Length <= 100)
                    {

                        dato.numAutorizacion = (string)HospitalizacionE["numAutorizacion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 100 caracteres.";
                        throw new Exception(textError);
                    }



                    columna = "causaMotivoAtencion";
                    texto = Convert.ToString(HospitalizacionE["causaMotivoAtencion"]);
                    if (texto.Length <= 50)
                    {
                        dato.causaMotivoAtencion = (string)HospitalizacionE["causaMotivoAtencion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoPrincipal";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoPrincipal"]);
                    if (texto.Length <= 50)
                    {
                        dato.codDiagnosticoPrincipal = (string)HospitalizacionE["codDiagnosticoPrincipal"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "codDiagnosticoPrincipalE";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoPrincipalE"]);
                    if (texto.Length <= 50)
                    {
                        dato.codDiagnosticoPrincipalE = (string)HospitalizacionE["codDiagnosticoPrincipalE"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoRelacionadoE1";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoRelacionadoE1"]);
                    if (texto.Length <= 50)
                    {
                        dato.codDiagnosticoRelacionadoE1 = (string)HospitalizacionE["codDiagnosticoRelacionadoE1"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "codDiagnosticoRelacionadoE2";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoRelacionadoE2"]);
                    if (texto.Length <= 50)
                    {
                        dato.codDiagnosticoRelacionadoE2 = (string)HospitalizacionE["codDiagnosticoRelacionadoE2"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoRelacionadoE3";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoRelacionadoE3"]);
                    if (texto.Length <= 100)
                    {
                        dato.codDiagnosticoRelacionadoE3 = (string)HospitalizacionE["codDiagnosticoRelacionadoE3"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codComplicacion";
                    texto = Convert.ToString(HospitalizacionE["codComplicacion"]);
                    if (texto.Length <= 50)
                    {
                        dato.codComplicacion = (string)HospitalizacionE["codComplicacion"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "condicionDestinoUsuarioEgreso";
                    texto = Convert.ToString(HospitalizacionE["condicionDestinoUsuarioEgreso"]);
                    if (texto.Length <= 50)
                    {
                        dato.condicionDestinoUsuarioEgreso = (string)HospitalizacionE["condicionDestinoUsuarioEgreso"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }


                    columna = "codDiagnosticoCausaMuerte";
                    texto = Convert.ToString(HospitalizacionE["codDiagnosticoCausaMuerte"]);
                    if (texto.Length <= 50)
                    {
                        dato.codDiagnosticoCausaMuerte = (string)HospitalizacionE["codDiagnosticoCausaMuerte"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 50 caracteres.";
                        throw new Exception(textError);
                    }

                    columna = "fechaEgreso";
                    try
                    {
                        fechas = Convert.ToDateTime(HospitalizacionE["fechaEgreso"]);
                        if (fechas != null)
                        {
                            dato.fechaEgreso = (DateTime?)HospitalizacionE["fechaEgreso"];
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


                    columna = "consecutivo";
                    texto = Convert.ToString(HospitalizacionE["consecutivo"]);
                    if (texto.Length <= 9)
                    {
                        dato.consecutivo = (int)HospitalizacionE["consecutivo"];
                    }
                    else
                    {
                        textError = columna + ", solo puede contener 9 caracteres.";
                        throw new Exception(textError);
                    }

                    dato.fecha_digita = DateTime.Now;
                    dato.usuario_digita = SesionVar.UserName;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en HOSPITALIZACIÓN conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }

                mensajeResultado = mensaje;

                throw new Exception(mensaje);
            }

            return dato;
        }

        public fis_rips_cargue_procedimientos ValidarEstructuraProcedimiento(JObject ProcedimientoE, string codPrestador,List<fis_rips_cups> listadoCups, int? fila)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            var textError = "";
            fis_rips_cargue_procedimientos dato = new fis_rips_cargue_procedimientos();

            try
            {
                var texto = "";
                var numero = 0;
                DateTime fechas = new DateTime();
                decimal decima = new decimal();

                columna = "codPrestador";
                texto = Convert.ToString(ProcedimientoE["codPrestador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.codPrestador = (decimal)ProcedimientoE["codPrestador"];

                        //if (texto != codPrestador)
                        //{
                        //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                        //}
                    }
                    else
                    {
                        dato.codPrestador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "fechaInicioAtencion";
                try
                {
                    var datoPrueba = ProcedimientoE["fechaInicioAtencion"];

                    if (datoPrueba != null)
                    {
                        string fechaTexto = datoPrueba.ToString().Trim();
                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                        if (!string.IsNullOrEmpty(fechaTexto))
                        {
                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                            string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };
                            DateTime fechaEspecial;

                            // Probar los diferentes formatos
                            bool fechaValida = DateTime.TryParseExact(
                                fechaTexto,
                                formatos,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                out fechaEspecial
                            );

                            if (fechaValida)
                            {
                                dato.fechaInicioAtencion = fechaEspecial;
                                dato.fecha_prestacion = fechaEspecial;
                            }
                            else
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    else
                    {
                        textError = columna + ", no puede ir vacío";
                        throw new Exception(textError);
                    }
                }
                catch (Exception ex)
                {
                    textError = columna + ", tiene formato incorrecto";
                    throw new Exception(textError);
                }


                columna = "idMIPRES";
                texto = Convert.ToString(ProcedimientoE["idMIPRES"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.idMIPRES = (decimal)ProcedimientoE["idMIPRES"];
                    }
                    else
                    {
                        dato.idMIPRES = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numAutorizacion";
                texto = Convert.ToString(ProcedimientoE["numAutorizacion"]);
                if (texto.Length <= 50)
                {
                    dato.numAutorizacion = (string)ProcedimientoE["numAutorizacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codProcedimiento";
                texto = Convert.ToString(ProcedimientoE["codProcedimiento"]);
                if (texto.Length <= 100)
                {
                    dato.codProcedimiento = (string)ProcedimientoE["codProcedimiento"];

                    string codigoLimpio = dato.codProcedimiento.Replace("-0", "-");
                    if (codigoLimpio.StartsWith("0"))
                    {
                        codigoLimpio = codigoLimpio.Substring(1);
                    }

                    fis_rips_cups cups = listadoCups.FirstOrDefault(x => x.codigo_cups == codigoLimpio);
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "viaIngresoServicioSalud";
                texto = Convert.ToString(ProcedimientoE["viaIngresoServicioSalud"]);
                if (texto.Length <= 100)
                {
                    dato.viaIngresoServicioSalud = (string)ProcedimientoE["viaIngresoServicioSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "modalidadGrupoServicioTecSal";
                texto = Convert.ToString(ProcedimientoE["modalidadGrupoServicioTecSal"]);
                if (texto.Length <= 100)
                {
                    dato.modalidadGrupoServicioTecSal = (string)ProcedimientoE["modalidadGrupoServicioTecSal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "grupoServicios";
                texto = Convert.ToString(ProcedimientoE["grupoServicios"]);
                if (texto.Length <= 100)
                {
                    dato.grupoServicios = (string)ProcedimientoE["grupoServicios"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codServicio";
                texto = Convert.ToString(ProcedimientoE["codServicio"]);
                if (texto.Length <= 100)
                {
                    dato.codServicio = (string)ProcedimientoE["codServicio"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "finalidadTecnologiaSalud";
                texto = Convert.ToString(ProcedimientoE["finalidadTecnologiaSalud"]);
                if (texto.Length <= 100)
                {
                    dato.finalidadTecnologiaSalud = (string)ProcedimientoE["finalidadTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "tipoDocumentoIdentificacion";
                texto = Convert.ToString(ProcedimientoE["tipoDocumentoIdentificacion"]);
                if (texto.Length <= 100)
                {
                    dato.tipoDocumentoIdentificacion = (string)ProcedimientoE["tipoDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "tipoDocumentoIdentificacion";
                texto = Convert.ToString(ProcedimientoE["tipoDocumentoIdentificacion"]);
                if (texto.Length <= 100)
                {
                    dato.tipoDocumentoIdentificacion = (string)ProcedimientoE["tipoDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numDocumentoIdentificacion";
                texto = Convert.ToString(ProcedimientoE["numDocumentoIdentificacion"]);
                if (texto.Length <= 100)
                {
                    dato.numDocumentoIdentificacion = (string)ProcedimientoE["numDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                //List<management_fis_buscarExietenciaBeneficiarioResult> existeBeneficiario = BusClass.BuscarBeneficiarioEnFis(dato.fechaInicioAtencion, dato.tipoDocumentoIdentificacion, dato.numDocumentoIdentificacion);
                //if (existeBeneficiario.Count() == 0)
                //{
                //    var mensajeError = "El documento: " + dato.numDocumentoIdentificacion + " no existe en beneficiarios";
                //    throw new Exception(mensajeError);
                //}

                columna = "codDiagnosticoPrincipal";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoPrincipal"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoPrincipal = (string)ProcedimientoE["codDiagnosticoPrincipal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoRelacionado";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoRelacionado"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoRelacionado = (string)ProcedimientoE["codDiagnosticoRelacionado"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codComplicacion";
                texto = Convert.ToString(ProcedimientoE["codComplicacion"]);
                if (texto.Length <= 100)
                {
                    dato.codComplicacion = (string)ProcedimientoE["codComplicacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "vrServicio";
                texto = Convert.ToString(ProcedimientoE["vrServicio"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.vrServicio = (decimal?)ProcedimientoE["vrServicio"];
                    }
                    else
                    {
                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                        throw new Exception(textError);
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "conceptoRecaudo";
                texto = Convert.ToString(ProcedimientoE["conceptoRecaudo"]);
                if (texto.Length <= 50)
                {
                    dato.conceptoRecaudo = (string)ProcedimientoE["conceptoRecaudo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "valorPagoModerador";
                texto = Convert.ToString(ProcedimientoE["valorPagoModerador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.valorPagoModerador = (decimal)ProcedimientoE["valorPagoModerador"];
                    }
                    else
                    {
                        dato.valorPagoModerador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numFEVPagoModerador";
                texto = Convert.ToString(ProcedimientoE["numFEVPagoModerador"]);
                if (texto.Length <= 100)
                {
                    dato.numFEVPagoModerador = (string)ProcedimientoE["numFEVPagoModerador"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "consecutivo";
                texto = Convert.ToString(ProcedimientoE["consecutivo"]);
                if (texto.Length <= 13)
                {
                    dato.consecutivo = (int)ProcedimientoE["consecutivo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en PROCEDIMIENTOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }
                mensajeResultado = mensaje;
                throw new Exception(mensaje);
            }

            return dato;
        }

        public fis_rips_cargue_urgencias ValidarEstructuraUrgencias(JObject ProcedimientoE, string codPrestador, int? fila)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            var textError = "";

            fis_rips_cargue_urgencias dato = new fis_rips_cargue_urgencias();

            try
            {
                var texto = "";
                var numero = 0;
                DateTime fechas = new DateTime();
                decimal decima = new decimal();


                columna = "codPrestador";
                texto = Convert.ToString(ProcedimientoE["codPrestador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.codPrestador = (decimal)ProcedimientoE["codPrestador"];

                        //if (texto != codPrestador)
                        //{
                        //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                        //}
                    }
                    else
                    {
                        dato.codPrestador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }


                //columna = "fechaInicioAtencion";
                //try
                //{
                //    fechas = Convert.ToDateTime(ProcedimientoE["fechaInicioAtencion"]);
                //    if (fechas != null)
                //    {
                //        dato.fechaInicioAtencion = (DateTime?)ProcedimientoE["fechaInicioAtencion"];
                //    }
                //    else
                //    {
                //        textError = columna + ", no puede ir vacio";
                //        throw new Exception(textError);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    textError = columna + ", tiene formato incorrecto";
                //    throw new Exception(textError);
                //}
                columna = "fechaInicioAtencion";
                try
                {
                    var datoPrueba = ProcedimientoE["fechaInicioAtencion"];

                    if (datoPrueba != null)
                    {
                        string fechaTexto = datoPrueba.ToString().Trim();
                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                        if (!string.IsNullOrEmpty(fechaTexto))
                        {
                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                            string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };
                            DateTime fechaEspecial;

                            // Probar los diferentes formatos
                            bool fechaValida = DateTime.TryParseExact(
                                fechaTexto,
                                formatos,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                out fechaEspecial
                            );

                            if (fechaValida)
                            {
                                dato.fechaInicioAtencion = fechaEspecial;
                                dato.fecha_prestacion = fechaEspecial;
                            }
                            else
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    else
                    {
                        textError = columna + ", no puede ir vacío";
                        throw new Exception(textError);
                    }
                }
                catch (Exception ex)
                {
                    textError = columna + ", tiene formato incorrecto";
                    throw new Exception(textError);
                }


                columna = "causaMotivoAtencion";
                texto = Convert.ToString(ProcedimientoE["causaMotivoAtencion"]);
                if (texto.Length <= 9)
                {
                    dato.causaMotivoAtencion = (int)ProcedimientoE["causaMotivoAtencion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }


                columna = "codDiagnosticoPrincipal";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoPrincipal"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoPrincipal = (string)ProcedimientoE["codDiagnosticoPrincipal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }


                columna = "codDiagnosticoPrincipalE";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoPrincipalE"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoPrincipalE = (string)ProcedimientoE["codDiagnosticoPrincipalE"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoRelacionadoE1";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoRelacionadoE1"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoRelacionadoE1 = (string)ProcedimientoE["codDiagnosticoRelacionadoE1"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoRelacionadoE2";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoRelacionadoE2"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoRelacionadoE2 = (string)ProcedimientoE["codDiagnosticoRelacionadoE2"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }


                columna = "codDiagnosticoRelacionadoE3";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoRelacionadoE3"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoRelacionadoE3 = (string)ProcedimientoE["codDiagnosticoRelacionadoE3"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }


                columna = "condicionDestinoUsuarioEgreso";
                texto = Convert.ToString(ProcedimientoE["condicionDestinoUsuarioEgreso"]);
                if (texto.Length <= 100)
                {
                    dato.condicionDestinoUsuarioEgreso = (string)ProcedimientoE["condicionDestinoUsuarioEgreso"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoCausaMuerte";
                texto = Convert.ToString(ProcedimientoE["codDiagnosticoCausaMuerte"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoCausaMuerte = (string)ProcedimientoE["codDiagnosticoCausaMuerte"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }


                columna = "fechaEgreso";
                try
                {
                    fechas = Convert.ToDateTime(ProcedimientoE["fechaEgreso"]);
                    if (fechas != null)
                    {
                        dato.fechaEgreso = (DateTime?)ProcedimientoE["fechaEgreso"];
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


                columna = "consecutivo";
                texto = Convert.ToString(ProcedimientoE["consecutivo"]);
                if (texto.Length <= 13)
                {
                    dato.consecutivo = (int)ProcedimientoE["consecutivo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos en cargue URGENCIAS"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en URGENCIAS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }

                mensajeResultado = mensaje;
            }

            return dato;
        }

        public fis_rips_cargue_reciennacido ValidarEstructuraRecienNacido(JObject RecienNacidoE, string codPrestador, int? fila)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            var textError = "";
            fis_rips_cargue_reciennacido dato = new fis_rips_cargue_reciennacido();

            try
            {

                var texto = "";
                var numero = 0;
                DateTime fechas = new DateTime();
                decimal decima = new decimal();


                columna = "codPrestador";
                texto = Convert.ToString(RecienNacidoE["codPrestador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.codPrestador = (decimal)RecienNacidoE["codPrestador"];

                        //if (texto != codPrestador)
                        //{
                        //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                        //}
                    }
                    else
                    {
                        dato.codPrestador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }



                columna = "tipoDocumentoIdentificacion";
                texto = Convert.ToString(RecienNacidoE["tipoDocumentoIdentificacion"]);
                if (texto.Length <= 50)
                {
                    dato.tipoDocumentoIdentificacion = (string)RecienNacidoE["tipoDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numDocumentoIdentificacion";
                texto = Convert.ToString(RecienNacidoE["numDocumentoIdentificacion"]);
                if (texto.Length <= 50)
                {
                    dato.numDocumentoIdentificacion = (string)RecienNacidoE["numDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }


                columna = "fechaNacimiento";
                try
                {
                    fechas = Convert.ToDateTime(RecienNacidoE["fechaNacimiento"]);
                    if (fechas != null)
                    {
                        dato.fechaNacimiento = (DateTime?)RecienNacidoE["fechaNacimiento"];
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


                columna = "edadGestacional";
                texto = Convert.ToString(RecienNacidoE["edadGestacional"]);
                if (texto.Length <= 9)
                {
                    dato.edadGestacional = (int)RecienNacidoE["edadGestacional"];
                }
                else
                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numConsultasCPrenatal";
                texto = Convert.ToString(RecienNacidoE["numConsultasCPrenatal"]);
                if (texto.Length <= 100)
                {
                    dato.numDocumentoIdentificacion = (string)RecienNacidoE["numConsultasCPrenatal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codSexoBiologico";
                texto = Convert.ToString(RecienNacidoE["codSexoBiologico"]);
                if (texto.Length <= 15)
                {
                    dato.codSexoBiologico = (string)RecienNacidoE["codSexoBiologico"];
                }
                else
                {
                    textError = columna + ", solo puede contener 15 caracteres.";
                    throw new Exception(textError);
                }

                columna = "peso";
                texto = Convert.ToString(RecienNacidoE["peso"]);
                if (texto.Length <= 9)
                {
                    dato.peso = (int)RecienNacidoE["peso"];
                }
                else
                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoPrincipal";
                texto = Convert.ToString(RecienNacidoE["codDiagnosticoPrincipal"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoPrincipal = (string)RecienNacidoE["codDiagnosticoPrincipal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "condicionDestinoUsuarioEgreso";
                texto = Convert.ToString(RecienNacidoE["condicionDestinoUsuarioEgreso"]);
                if (texto.Length <= 100)
                {
                    dato.condicionDestinoUsuarioEgreso = (string)RecienNacidoE["condicionDestinoUsuarioEgreso"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoCausaMuerte";
                texto = Convert.ToString(RecienNacidoE["codDiagnosticoCausaMuerte"]);
                if (texto.Length <= 100)
                {
                    dato.codDiagnosticoCausaMuerte = (string)RecienNacidoE["codDiagnosticoCausaMuerte"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "fechaEgreso";
                try
                {
                    fechas = Convert.ToDateTime(RecienNacidoE["fechaEgreso"]);
                    if (fechas != null)
                    {
                        dato.fechaEgreso = (DateTime?)RecienNacidoE["fechaEgreso"];
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


                columna = "consecutivo";
                texto = Convert.ToString(RecienNacidoE["consecutivo"]);
                if (texto.Length <= 13)
                {
                    dato.consecutivo = (int)RecienNacidoE["consecutivo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en RECÍEN NACIDO conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }

                mensajeResultado = mensaje;
                throw new Exception(mensaje);
            }

            return dato;
        }

        public fis_rips_cargue_medicamentos ValidarEstructuraMedicamento(JObject MedicamentoE, string codPrestador, List<fis_rips_cups> listadoCups, int? fila)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            var textError = "";
            fis_rips_cargue_medicamentos dato = new fis_rips_cargue_medicamentos();

            try
            {

                var texto = "";
                var numero = 0;
                DateTime fechas = new DateTime();
                decimal decima = new decimal();


                columna = "codPrestador";
                texto = Convert.ToString(MedicamentoE["codPrestador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.codPrestador = (decimal)MedicamentoE["codPrestador"];

                        //if (texto != codPrestador)
                        //{
                        //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                        //}
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(codPrestador))
                        {
                            dato.codPrestador = Convert.ToDecimal(codPrestador);
                        }
                    }

                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }


                columna = "numAutorizacion";
                texto = Convert.ToString(MedicamentoE["numAutorizacion"]);
                if (texto.Length <= 50)
                {

                    dato.numAutorizacion = (string)MedicamentoE["numAutorizacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "idMIPRES";
                texto = Convert.ToString(MedicamentoE["idMIPRES"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.idMIPRES = (decimal)MedicamentoE["idMIPRES"];
                    }
                    else
                    {
                        dato.idMIPRES = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "fechaDispensAdmon";
                try
                {
                    var datoPrueba = MedicamentoE["fechaDispensAdmon"];

                    if (datoPrueba != null)
                    {
                        string fechaTexto = datoPrueba.ToString().Trim();
                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                        if (!string.IsNullOrEmpty(fechaTexto))
                        {
                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                            string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };

                            DateTime fechaEspecial;

                            // Probar los diferentes formatos
                            bool fechaValida = DateTime.TryParseExact(
                                fechaTexto,
                                formatos,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                out fechaEspecial
                            );

                            if (fechaValida)
                            {
                                dato.fechaDispensAdmon = fechaEspecial;
                                dato.fecha_prestacion = fechaEspecial;
                            }
                            else
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    else
                    {
                        textError = columna + ", no puede ir vacío";
                        throw new Exception(textError);
                    }
                }
                catch (Exception ex)
                {
                    textError = columna + ", tiene formato incorrecto";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoPrincipal";
                texto = Convert.ToString(MedicamentoE["codDiagnosticoPrincipal"]);
                if (texto.Length <= 50)
                {
                    dato.codDiagnosticoPrincipal = (string)MedicamentoE["codDiagnosticoPrincipal"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codDiagnosticoRelacionado";
                texto = Convert.ToString(MedicamentoE["codDiagnosticoRelacionado"]);
                if (texto.Length <= 50)
                {
                    dato.codDiagnosticoRelacionado = (string)MedicamentoE["codDiagnosticoRelacionado"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "tipoMedicamento";
                texto = Convert.ToString(MedicamentoE["tipoMedicamento"]);
                if (texto.Length <= 50)
                {
                    dato.tipoMedicamento = (string)MedicamentoE["tipoMedicamento"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "codTecnologiaSalud";
                texto = Convert.ToString(MedicamentoE["codTecnologiaSalud"]);
                if (texto.Length <= 50)
                {
                    dato.codTecnologiaSalud = (string)MedicamentoE["codTecnologiaSalud"];

                    string codigoLimpio = dato.codTecnologiaSalud.Replace("-0", "-");
                    if (codigoLimpio.StartsWith("0"))
                    {
                        codigoLimpio = codigoLimpio.Substring(1);
                    }

                    fis_rips_cups cups = listadoCups.FirstOrDefault(x => x.codigo_cups == codigoLimpio);
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "nomTecnologiaSalud";
                texto = Convert.ToString(MedicamentoE["nomTecnologiaSalud"]);
                if (texto.Length <= 50)
                {
                    dato.nomTecnologiaSalud = (string)MedicamentoE["nomTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "concentracionMedicamento";
                texto = Convert.ToString(MedicamentoE["concentracionMedicamento"]);
                if (texto.Length <= 50)
                {
                    dato.concentracionMedicamento = (string)MedicamentoE["concentracionMedicamento"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "unidadMedida";
                texto = Convert.ToString(MedicamentoE["unidadMedida"]);
                if (texto.Length <= 50)
                {
                    dato.unidadMedida = (string)MedicamentoE["unidadMedida"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "formaFarmaceutica";
                texto = Convert.ToString(MedicamentoE["formaFarmaceutica"]);
                if (texto.Length <= 50)
                {
                    dato.formaFarmaceutica = (string)MedicamentoE["formaFarmaceutica"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }


                columna = "unidadMinDispensa";
                texto = Convert.ToString(MedicamentoE["unidadMinDispensa"]);
                if (texto.Length <= 50)
                {
                    dato.unidadMinDispensa = (string)MedicamentoE["unidadMinDispensa"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "cantidadMedicamento";
                texto = Convert.ToString(MedicamentoE["cantidadMedicamento"]);
                if (texto.Length <= 9)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.cantidadMedicamento = (int)MedicamentoE["cantidadMedicamento"];
                    }
                    else
                    {
                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                        throw new Exception(textError);
                    }
                }
                else

                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }

                columna = "diasTratamiento";
                texto = Convert.ToString(MedicamentoE["diasTratamiento"]);
                if (texto.Length <= 9)
                {
                    dato.diasTratamiento = (int)MedicamentoE["diasTratamiento"];
                }
                else

                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }

                columna = "tipoDocumentoIdentificacion";
                texto = Convert.ToString(MedicamentoE["tipoDocumentoIdentificacion"]);
                if (texto.Length <= 50)
                {
                    dato.tipoDocumentoIdentificacion = (string)MedicamentoE["tipoDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numDocumentoIdentificacion";
                texto = Convert.ToString(MedicamentoE["numDocumentoIdentificacion"]);
                if (texto.Length <= 50)
                {
                    dato.numDocumentoIdentificacion = (string)MedicamentoE["numDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "vrUnitMedicamento";
                texto = Convert.ToString(MedicamentoE["vrUnitMedicamento"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.vrUnitMedicamento = (decimal)MedicamentoE["vrUnitMedicamento"];
                    }
                    else
                    {
                        dato.vrUnitMedicamento = null;
                    }

                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }


                columna = "vrServicio";
                texto = Convert.ToString(MedicamentoE["vrServicio"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.vrServicio = (decimal)MedicamentoE["vrServicio"];
                    }
                    else
                    {
                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                        throw new Exception(textError);
                    }

                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }


                columna = "conceptoRecaudo";
                texto = Convert.ToString(MedicamentoE["conceptoRecaudo"]);
                if (texto.Length <= 50)
                {
                    dato.conceptoRecaudo = (string)MedicamentoE["conceptoRecaudo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "valorPagoModerador";
                texto = Convert.ToString(MedicamentoE["valorPagoModerador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.valorPagoModerador = (decimal)MedicamentoE["valorPagoModerador"];
                    }
                    else
                    {
                        dato.valorPagoModerador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numFEVPagoModerador";
                texto = Convert.ToString(MedicamentoE["numFEVPagoModerador"]);
                if (texto.Length <= 100)
                {
                    dato.numFEVPagoModerador = (string)MedicamentoE["numFEVPagoModerador"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "consecutivo";
                texto = Convert.ToString(MedicamentoE["consecutivo"]);
                if (texto.Length <= 13)
                {
                    dato.consecutivo = (int)MedicamentoE["consecutivo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;
            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en MEDICAMENTOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }
                mensajeResultado = mensaje;

                throw new Exception(mensaje);
            }

            return dato;
        }

        public fis_rips_cargue_otros_servicios ValidarEstructuraOtroServicio(JObject OtroServicioE, string codPrestador, int? fila)
        {
            string mensajeResultado = string.Empty;
            string columna = "";
            var textError    = "";
            fis_rips_cargue_otros_servicios dato = new fis_rips_cargue_otros_servicios();

            try
            {
                var texto = "";
                var numero = 0;
                DateTime fechas = new DateTime();
                decimal decima = new decimal();


                columna = "codPrestador";
                texto = Convert.ToString(OtroServicioE["codPrestador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.codPrestador = (decimal)OtroServicioE["codPrestador"];

                        //if (texto != codPrestador)
                        //{
                        //    throw new Exception("Código de prestador no concuerda con el código de prestador de la factura");
                        //}
                    }
                    else
                    {
                        dato.codPrestador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }


                columna = "numAutorizacion";
                texto = Convert.ToString(OtroServicioE["numAutorizacion"]);
                if (texto.Length <= 50)
                {

                    dato.numAutorizacion = (string)OtroServicioE["numAutorizacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "idMIPRES";
                texto = Convert.ToString(OtroServicioE["idMIPRES"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.idMIPRES = (decimal)OtroServicioE["idMIPRES"];
                    }
                    else
                    {
                        dato.idMIPRES = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "fechaSuministroTecnologia";
                try
                {
                    var datoPrueba = OtroServicioE["fechaSuministroTecnologia"];

                    if (datoPrueba != null)
                    {
                        string fechaTexto = datoPrueba.ToString().Trim();
                        fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                        if (!string.IsNullOrEmpty(fechaTexto))
                        {
                            // Incluir más formatos de fecha con y sin hora, y con AM/PM
                            string[] formatos = {

                                "yyyy/MM/dd",
                                "yyyy-MM-dd HH:mm",
                                "dd/MM/yyyy HH:mm:ss" // Formato con AM/PM y horas en formato de 12 horas

                                };

                            DateTime fechaEspecial;

                            // Probar los diferentes formatos
                            bool fechaValida = DateTime.TryParseExact(
                                fechaTexto,
                                formatos,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                out fechaEspecial
                            );

                            if (fechaValida)
                            {
                                dato.fechaSuministroTecnologia = fechaEspecial;
                                dato.fecha_prestacion = fechaEspecial;
                            }
                            else
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", no puede ir vacío";
                            throw new Exception(textError);
                        }
                    }
                    else
                    {
                        textError = columna + ", no puede ir vacío";
                        throw new Exception(textError);
                    }
                }
                catch (Exception ex)
                {
                    textError = columna + ", tiene formato incorrecto";
                    throw new Exception(textError);
                }



                columna = "tipoOS";
                texto = Convert.ToString(OtroServicioE["tipoOS"]);
                if (texto.Length <= 50)
                {
                    dato.tipoOS = (string)OtroServicioE["tipoOS"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }


                columna = "codTecnologiaSalud";
                texto = Convert.ToString(OtroServicioE["codTecnologiaSalud"]);
                if (texto.Length <= 50)
                {
                    dato.codTecnologiaSalud = (string)OtroServicioE["codTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "nomTecnologiaSalud";
                texto = Convert.ToString(OtroServicioE["nomTecnologiaSalud"]);
                if (texto.Length <= 100)
                {
                    dato.nomTecnologiaSalud = (string)OtroServicioE["nomTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }



                columna = "codTecnologiaSalud";
                texto = Convert.ToString(OtroServicioE["codTecnologiaSalud"]);
                if (texto.Length <= 50)
                {
                    dato.codTecnologiaSalud = (string)OtroServicioE["codTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "nomTecnologiaSalud";
                texto = Convert.ToString(OtroServicioE["nomTecnologiaSalud"]);
                if (texto.Length <= 100)
                {
                    dato.nomTecnologiaSalud = (string)OtroServicioE["nomTecnologiaSalud"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "cantidadOS";
                texto = Convert.ToString(OtroServicioE["cantidadOS"]);
                if (texto.Length <= 9)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.cantidadOS = (int)OtroServicioE["cantidadOS"];
                    }
                    else
                    {
                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                        throw new Exception(textError);
                    }
                }
                else

                {
                    textError = columna + ", solo puede contener 9 caracteres.";
                    throw new Exception(textError);
                }

                columna = "tipoDocumentoIdentificacion";
                texto = Convert.ToString(OtroServicioE["tipoDocumentoIdentificacion"]);
                if (texto.Length <= 100)
                {
                    dato.tipoDocumentoIdentificacion = (string)OtroServicioE["tipoDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numDocumentoIdentificacion";
                texto = Convert.ToString(OtroServicioE["numDocumentoIdentificacion"]);
                if (texto.Length <= 100)
                {
                    dato.numDocumentoIdentificacion = (string)OtroServicioE["numDocumentoIdentificacion"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "vrUnitOS";
                texto = Convert.ToString(OtroServicioE["vrUnitOS"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.vrUnitOS = (decimal)OtroServicioE["vrUnitOS"];
                    }
                    else
                    {
                        dato.vrUnitOS = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "vrServicio";
                texto = Convert.ToString(OtroServicioE["vrServicio"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "" && texto != "0")
                    {
                        dato.vrServicio = (decimal)OtroServicioE["vrServicio"];
                    }
                    else
                    {
                        textError = columna + ", no pueden venir valores en 0 o vacíos.";
                        throw new Exception(textError);
                    }

                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "conceptoRecaudo";
                texto = Convert.ToString(OtroServicioE["conceptoRecaudo"]);
                if (texto.Length <= 50)
                {
                    dato.conceptoRecaudo = (string)OtroServicioE["conceptoRecaudo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 50 caracteres.";
                    throw new Exception(textError);
                }

                columna = "valorPagoModerador";
                texto = Convert.ToString(OtroServicioE["valorPagoModerador"]);
                if (texto.Length <= 13)
                {
                    if (texto != "null" && texto != null && texto != "")
                    {
                        dato.valorPagoModerador = (decimal)OtroServicioE["valorPagoModerador"];
                    }
                    else
                    {
                        dato.valorPagoModerador = null;
                    }
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                columna = "numFEVPagoModerador";
                texto = Convert.ToString(OtroServicioE["numFEVPagoModerador"]);
                if (texto.Length <= 100)
                {
                    dato.numFEVPagoModerador = (string)OtroServicioE["numFEVPagoModerador"];
                }
                else
                {
                    textError = columna + ", solo puede contener 100 caracteres.";
                    throw new Exception(textError);
                }

                columna = "consecutivo";
                texto = Convert.ToString(OtroServicioE["consecutivo"]);
                if (texto.Length <= 13)
                {
                    dato.consecutivo = (int)OtroServicioE["consecutivo"];
                }
                else
                {
                    textError = columna + ", solo puede contener 13 caracteres.";
                    throw new Exception(textError);
                }

                dato.fecha_digita = DateTime.Now;
                dato.usuario_digita = SesionVar.UserName;

            }
            catch (Exception ex)
            {
                var error = ex.Message.Contains("Error reading JObject from JsonReader.") ? "El archivo JSON tiene estructura erronea" : ex.Message;
                var mensaje = "";
                if (error.Contains("No se encontraron datos"))
                {
                    mensaje = error;
                }
                else
                {
                    mensaje = "Error en OTROS SERVICIOS conjunto #" + fila + " - Columna: " + columna + " - Detalles: " + error;
                }
                mensajeResultado = mensaje;
                throw new Exception(mensaje);
            }

            return dato;
        }

    }

    public class Facturacion_FIS
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

        #endregion PROPIEDADES

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

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

        public int idRips { get; set; }

        public int id_glosa { get; set; }

        public int id_cargue { get; set; }

        public int id_factura { get; set; }

        public int idUsuario { get; set; }

        public string cuv { get; set; }

        public string codPrestador { get; set; }

        public string codCups { get; set; }

        public string codigo_cuv { get; set; }

        public string tipo { get; set; }

        public int concepto_general { get; set; }

        public int concepto_especifico { get; set; }

        public int concepto_aplicacion { get; set; }

        public Decimal valor_glosado { get; set; }

        public Decimal valor_factura { get; set; }

        public int cantidad { get; set; }

        public int cantidadMaxima { get; set; }

        public string observacion { get; set; }

        public int tipoIngreso { get; set; }

        public string cie10 { get; set; }

        public string descripcioncie10 { get; set; }

        public string cie10_relacionado { get; set; }

        public string descripcioncie10_relacionado { get; set; }

        public string tiga { get; set; }

        public string descripcionTiga { get; set; }

        //Insertar factura fis
        public int idCargue { get; set; }
        public int idDetalle { get; set; }
        public string codigoPrestador { get; set; }
        public string nit { get; set; }
        public string prestador { get; set; }
        public string factura { get; set; }
        public string tipodocumento { get; set; }
        public string numerodocumento { get; set; }
        public string nombrebeneficiario { get; set; }
        public DateTime fechafactura { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }

        public string FI { get; set; }

        public string localidad { get; set; }

        public int idContrato { get; set; }
        public string numcontrato { get; set; }
        public string centroLogistico { get; set; }
        public string grupoCompras { get; set; }
        public string posicionContrato { get; set; }
        public string contratoOperativo { get; set; }
        public string estado { get; set; }
        public string anticipo { get; set; }
        public decimal iva { get; set; }
        public decimal valoranticipo { get; set; }
        public decimal baseiva { get; set; }
        public string hallazgo { get; set; }
        public decimal totalfactura { get; set; }
        public int regional { get; set; }


        public int gasto_integral { get; set; }
        public string tiga_integral { get; set; }

        public string descripcion_tiga_integral { get; set; }

        public int id_departamento { get; set; }

        public int id_municipio { get; set; }

        public string mensajeIngresoFacturasDetalle { get; set; }
        public int rtaIngresoFacturasDetalle { get; set; }
        public int id_registro { get; set; }

        public int glosaCompleta { get; set; }

        public string observacionGlosaCompleta { get; set; }

        public int pertinencia { get; set; }


        public string ExcelMasivoDetalles(DataTable dt2, fis_rips_sinJson_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_sinJson_detalle> Listado = new List<fis_rips_sinJson_detalle>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<fis_rips_cups> listadoCups = BusClass.TraerListadoCups();
            List<ref_cie10_fis> listadoCie10 = BusClass.listadoCie10FIS();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_sinJson_detalle obj = new fis_rips_sinJson_detalle();

                        fila++;
                        columna = "ID_detalle";

                        if (!string.IsNullOrEmpty(item["ID_detalle"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "ID_detalle";
                            try
                            {
                                texto = Convert.ToString(item["ID_detalle"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_detalle = Convert.ToInt32(item["ID_detalle"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "Número de la factura";
                            try
                            {
                                texto = Convert.ToString(item["Número de la factura"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_factura = Convert.ToString(item["Número de la factura"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Código de habilitación";
                            try
                            {
                                texto = Convert.ToString(item["Código de habilitación"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_habilitacion = Convert.ToString(item["Código de habilitación"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Tipo Doc";
                            try
                            {
                                texto = Convert.ToString(item["Tipo Doc"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.tipo_identificacion_usuario = Convert.ToString(item["Tipo Doc"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Numero Doc";
                            try
                            {
                                texto = Convert.ToString(item["Numero Doc"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_identificacion_usuario = Convert.ToString(item["Numero Doc"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Fecha atención";
                            try
                            {

                                string recibidaFecha = Convert.ToString(item["Fecha atención"]);
                                var datoPrueba = recibidaFecha.Split(' ')[0];

                                if (datoPrueba != null)
                                {
                                    string fechaTexto = datoPrueba.ToString().Trim().Replace(" 12:00:00 a. m.", "");
                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        string[] formatos = {
                                        //"dd/MM/yyyy",
                                        //"d/MM/yyyy"
                                        "MM/dd/yyyy"
                                        };
                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_atencion = fechaEspecial;
                                        }
                                        else
                                        {
                                            throw new Exception("Formato incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("No puede venir vacío");
                                    }
                                }
                                else
                                {
                                    throw new Exception("No puede venir vacío");
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Número de autorización";
                            try
                            {
                                texto = Convert.ToString(item["Número de autorización"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_autorizacion = Convert.ToString(item["Número de autorización"]);
                                }
                                else
                                {
                                    obj.numero_autorizacion = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Código servicio";
                            try
                            {
                                texto = Convert.ToString(item["Código servicio"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_cups = Convert.ToString(item["Código servicio"]);
                                }
                            }

                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Codigo Homologado FIS";
                            try
                            {
                                texto = Convert.ToString(item["Codigo Homologado FIS"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.cups_homologado = Convert.ToString(item["Codigo Homologado FIS"]);

                                    string codigoLimpio = obj.cups_homologado.Replace("-0", "-");
                                    if (codigoLimpio.StartsWith("0"))
                                    {
                                        codigoLimpio = codigoLimpio.Substring(1);
                                    }

                                    fis_rips_cups cups = listadoCups.FirstOrDefault(x => x.codigo_cups == codigoLimpio);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Descripción servicio";
                            try
                            {
                                texto = Convert.ToString(item["Descripción servicio"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.descripcion_cups = Convert.ToString(item["Descripción servicio"]);
                                }
                            }

                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Código del diagnóstico principal";
                            try
                            {
                                texto = Convert.ToString(item["Código del diagnóstico principal"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_diagnostico_principal = Convert.ToString(item["Código del diagnóstico principal"]);

                                    ref_cie10_fis cie10 = listadoCie10.FirstOrDefault(x => x.codigo == obj.codigo_diagnostico_principal);
                                    if (cie10 == null)
                                    {
                                        throw new Exception("Código del diagnóstico principal no existe");
                                    }
                                }
                                else
                                {
                                    obj.codigo_diagnostico_principal = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Diagnóstico relacionado";
                            try
                            {
                                texto = Convert.ToString(item["Diagnóstico relacionado"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo_diagnostico_relacionado = Convert.ToString(item["Diagnóstico relacionado"]);
                                }
                                else
                                {
                                    obj.codigo_diagnostico_relacionado = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Cantidad";
                            try
                            {
                                texto = Convert.ToString(item["Cantidad"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_unidades = Convert.ToInt32(item["Cantidad"]);

                                    if (obj.numero_unidades == 0)
                                    {
                                        throw new Exception("La cantidad debe ser mayor a 0");
                                    }
                                }
                                else
                                {
                                    obj.numero_unidades = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Valor Unitario";
                            try
                            {
                                texto = Convert.ToString(item["Valor Unitario"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_unitario_medicamento = Convert.ToDecimal(item["Valor Unitario"]);
                                    if (obj.valor_unitario_medicamento == 0)
                                    {
                                        throw new Exception("El valor unitario debe ser mayor a 0");
                                    }
                                }
                                else
                                {
                                    obj.valor_unitario_medicamento = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Valor total";
                            try
                            {
                                texto = Convert.ToString(item["Valor total"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_neto_pagar = Convert.ToDecimal(item["Valor total"]);

                                    if (obj.valor_neto_pagar == 0)
                                    {
                                        throw new Exception("El valor neto debe ser mayor a 0");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Fuente";
                            try
                            {
                                texto = Convert.ToString(item["Fuente"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.fuente = Convert.ToString(item["Fuente"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new fis_rips_sinJson_detalle();
                        }
                        else
                        {
                            throw new Exception("ID detalle no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        var conteo = 0;
                        string beneficiario = "";
                        string mensajeBen = "";

                        foreach (var item in Listado)
                        {
                            conteo++;

                            var existeBen = BusClass.ExisteBeneficiario(item.numero_identificacion_usuario);
                            if (existeBen == 0)
                            {
                                beneficiario += beneficiario == "" ? item.numero_identificacion_usuario : "," + item.numero_identificacion_usuario;
                            }

                            int existeDetale = BusClass.HayDetallesIdFactura(item.id_detalle);
                            if (existeDetale == 1)
                            {
                                var elimina = BusClass.EliminarCargueMasivoDetallesSinJson(item.id_detalle);
                            }
                        }

                        if (beneficiario != "")
                        {
                            columna = "Beneficiarios";
                            mensajeBen += "No existen los beneficiarios: " + "\n" + beneficiario;

                            throw new Exception(mensajeBen);
                        }

                        id_registro = BusClass.InsertarCargueDetalles(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS REGISTROS - CARGUE #" + id_registro;
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
                    }
                }

                catch (Exception ex)
                {
                    MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                    if (columna == "Beneficiarios")
                    {
                        MsgRes.DescriptionResponse = "Error en la validación de beneficiarios: " + ex.Message;
                    }
                    else
                    {
                        if (textError != "" && textError != null)
                        {
                            MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + textError;
                        }
                        else
                        {
                            MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                        }
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }

        public string ExcelMasivoIVM(DataTable dt2, fis_rips_cargueMasivo_ivm_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_cargueMasivo_ivm_registros> Listado = new List<fis_rips_cargueMasivo_ivm_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<fis_rips_cargueMasivo_ivm_registros> listadoIVM = BusClass.ExisteIVM();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_cargueMasivo_ivm_registros obj = new fis_rips_cargueMasivo_ivm_registros();

                        fila++;
                        columna = "Id detalle";

                        if (!string.IsNullOrEmpty(item["Id detalle"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Id detalle";
                            try
                            {
                                texto = Convert.ToString(item["Id detalle"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_factura = Convert.ToInt32(item["Id detalle"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Id VIM";
                            try
                            {
                                texto = Convert.ToString(item["Id VIM"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_vim = Convert.ToInt32(item["Id VIM"]);
                                    fis_rips_cargueMasivo_ivm_registros existe = listadoIVM.FirstOrDefault(x => x.id_vim == obj.id_vim);
                                    if (existe != null)
                                    {
                                        throw new Exception("Ya existen datos para este VIM");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Numero factura SAP";
                            try
                            {
                                texto = Convert.ToString(item["Numero factura SAP"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.numero_facturas_sap = Convert.ToString(item["Numero factura SAP"]);
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new fis_rips_cargueMasivo_ivm_registros();
                        }
                        else
                        {
                            throw new Exception("ID detalle no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {

                        id_registro = BusClass.InsertarCargueIVM(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS REGISTROS";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }

        public string ExcelMasivoPedidos(DataTable dt2, fis_rips_cargueMasivo_pedidos_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_cargueMasivo_pedidos_registros> Listado = new List<fis_rips_cargueMasivo_pedidos_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                List<fis_rips_cargueMasivo_pedidos_registros> listaPedido = BusClass.ListadoDatosFacturasContables();

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_cargueMasivo_pedidos_registros obj = new fis_rips_cargueMasivo_pedidos_registros();

                        fila++;
                        columna = "ID DETALLE";

                        if (!string.IsNullOrEmpty(item["ID DETALLE"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "ID DETALLE";
                            try
                            {
                                texto = Convert.ToString(item["ID DETALLE"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_factura = Convert.ToInt32(item["ID DETALLE"]);

                                    fis_rips_cargueMasivo_pedidos_registros existe = listaPedido.FirstOrDefault(x => x.id_factura == obj.id_factura);
                                    if (existe != null)
                                    {
                                        throw new Exception("Ya existe documento contable con el id de factura: " + obj.id_factura);
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "NUMERO PEDIDO";
                            try
                            {
                                texto = Convert.ToString(item["NUMERO PEDIDO"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    if (!string.IsNullOrEmpty(texto) && texto.All(char.IsDigit))
                                    {
                                        obj.numero_pedido = Convert.ToString(item["NUMERO PEDIDO"]);
                                    }
                                    else
                                    {
                                        throw new Exception("Solo se aceptan números");
                                    }
                                }
                                else
                                {
                                    throw new Exception("No puede quedar vacío");
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "VALOR PEDIDO";
                            try
                            {
                                texto = Convert.ToString(item["VALOR PEDIDO"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_pedido = Convert.ToDecimal(item["VALOR PEDIDO"]);
                                }
                                else
                                {
                                    obj.valor_pedido = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "FECHA PEDIDO";
                            try
                            {
                                var datoPrueba = item["FECHA PEDIDO"];

                                if (datoPrueba != null)
                                {
                                    string fechaTexto = datoPrueba.ToString().Trim();
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_pedido = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
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
                            obj = new fis_rips_cargueMasivo_pedidos_registros();
                        }
                        else
                        {
                            throw new Exception("ID detalle no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        id_registro = BusClass.InsertarCarguePedidos(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            BusClass.ActualizarFacturasAContabilizadas(id_registro);

                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS REGISTROS";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }


        public string ExcelMasivoCIE10(DataTable dt2, ref_cie10_fis_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<ref_cie10_fis> Listado = new List<ref_cie10_fis>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<ref_cie10_fis> listadoCIE = BusClass.listadoCie10FIS();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        ref_cie10_fis obj = new ref_cie10_fis();

                        fila++;
                        columna = "Codigo";

                        if (!string.IsNullOrEmpty(item["Codigo"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Codigo";
                            try
                            {
                                texto = Convert.ToString(item["Codigo"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.codigo = Convert.ToString(item["Codigo"]).ToUpper();
                                    ref_cie10_fis existe = listadoCIE.FirstOrDefault(x => x.codigo == obj.codigo);
                                    if (existe != null)
                                    {
                                        throw new Exception("Ya existen datos para este CIE10");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Descripcion";
                            try
                            {
                                texto = Convert.ToString(item["Descripcion"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.descripcion = Convert.ToString(item["Descripcion"]).ToUpper();
                                    ref_cie10_fis existe = listadoCIE.FirstOrDefault(x => x.descripcion == obj.descripcion);
                                    if (existe != null)
                                    {
                                        throw new Exception("Ya existen datos para este CIE10");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            obj.estado = 1;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new ref_cie10_fis();
                        }
                        else
                        {
                            throw new Exception("Codigo no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        id_registro = BusClass.InsertarCargueCIE10Masivo(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS REGISTROS";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }


        public string ExcelMasivoContratos(DataTable dt2, fis_rips_prestadores_contratos_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores_contratos> Listado = new List<fis_rips_prestadores_contratos>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<management_fis_listadoPrestadoresExistentesResult> listaPrestadores = BusClass.ListadoprestadoresFISeXISTENTES();
            List<management_fis_contratosExistentesResult> listadoContra = BusClass.ListadoContratosFISExistntes();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_prestadores_contratos obj = new fis_rips_prestadores_contratos();

                        fila++;
                        columna = "Id prestador";

                        if (!string.IsNullOrEmpty(item["Id prestador"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Id prestador";
                            try
                            {
                                texto = Convert.ToString(item["Id prestador"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_prestador = Convert.ToInt32(item["Id prestador"]);
                                    management_fis_listadoPrestadoresExistentesResult existePre = listaPrestadores.FirstOrDefault(x => x.id_prestador == obj.id_prestador);
                                    if (existePre == null)
                                    {
                                        throw new Exception("No existe este prestador");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Id contrato";
                            try
                            {
                                texto = Convert.ToString(item["Id contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_contrato = Convert.ToInt32(item["Id contrato"]);
                                    if (obj.id_contrato != 0 && obj.id_contrato != null)
                                    {
                                        management_fis_contratosExistentesResult existeContra = listadoContra.FirstOrDefault(x => x.id_contrato == obj.id_contrato);
                                        if (existeContra == null)
                                        {
                                            throw new Exception("No existe este contrato");
                                        }
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Numero contrato";
                            try
                            {
                                texto = Convert.ToString(item["Numero contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.num_contrato = Convert.ToString(item["Numero contrato"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Fecha suscripcion";
                            try
                            {
                                var datoPrueba = item["Fecha suscripcion"];

                                if (datoPrueba != null)
                                {
                                    var fecha = Convert.ToString(datoPrueba).Split(' ');
                                    string fechaTexto = fecha[0];
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_suscripcion = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Fecha inicial";
                            try
                            {
                                var datoPrueba = item["Fecha inicial"];

                                if (datoPrueba != null)
                                {
                                    var fecha = Convert.ToString(datoPrueba).Split(' ');
                                    string fechaTexto = fecha[0];
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_inicial = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Fecha final";
                            try
                            {
                                var datoPrueba = item["Fecha final"];

                                if (datoPrueba != null)
                                {
                                    var fecha = Convert.ToString(datoPrueba).Split(' ');
                                    string fechaTexto = fecha[0];
                                    fechaTexto = fechaTexto.Replace("a.m.", "AM").Replace("p.m.", "PM").Trim();

                                    if (!string.IsNullOrEmpty(fechaTexto))
                                    {
                                        // Incluir más formatos de fecha con y sin hora, y con AM/PM
                                        string[] formatos = {
                                        "dd/MM/yyyy",
                                        };

                                        DateTime fechaEspecial;

                                        // Probar los diferentes formatos
                                        bool fechaValida = DateTime.TryParseExact(
                                            fechaTexto,
                                            formatos,
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.AssumeLocal,  // Asegura que se manejen bien las horas locales
                                            out fechaEspecial
                                        );

                                        if (fechaValida)
                                        {
                                            obj.fecha_final = fechaEspecial;
                                        }
                                        else
                                        {
                                            textError = columna + ", tiene formato incorrecto";
                                            throw new Exception(textError);
                                        }
                                    }
                                    else
                                    {
                                        textError = columna + ", no puede ir vacío";
                                        throw new Exception(textError);
                                    }
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacío";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Objeto contrato";
                            try
                            {
                                texto = Convert.ToString(item["Objeto contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.objeto_contrato = Convert.ToString(item["Objeto contrato"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Adm contrato";
                            try
                            {
                                texto = Convert.ToString(item["Adm contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_adm_contrato = Convert.ToString(item["Adm contrato"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Nombre adm contrato";
                            try
                            {
                                texto = Convert.ToString(item["Nombre adm contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.nom_adm_contrato = Convert.ToString(item["Nombre adm contrato"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Apoyo transaccional";
                            try
                            {
                                texto = Convert.ToString(item["Apoyo transaccional"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_apoyo_transaccional = Convert.ToString(item["Apoyo transaccional"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Nombre apoyo transaccional";
                            try
                            {
                                texto = Convert.ToString(item["Nombre apoyo transaccional"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.nom_apoyo_transaccional = Convert.ToString(item["Nombre apoyo transaccional"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Interventor";
                            try
                            {
                                texto = Convert.ToString(item["Interventor"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_interventor = Convert.ToString(item["Interventor"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Nombre interventor";
                            try
                            {
                                texto = Convert.ToString(item["Nombre interventor"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.nom_interventor = Convert.ToString(item["Nombre interventor"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Valor contrato";
                            try
                            {
                                texto = Convert.ToString(item["Valor contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.valor_contrato = Convert.ToDecimal(item["Valor contrato"]);
                                }
                                else
                                {
                                    obj.valor_contrato = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Manual tarifario";
                            try
                            {
                                texto = Convert.ToString(item["Manual tarifario"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.manual_tarifario = Convert.ToString(item["Manual tarifario"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }
                            columna = "Negociacion";
                            try
                            {
                                texto = Convert.ToString(item["Negociacion"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.neogociacion = Convert.ToString(item["Negociacion"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Grupo compras";
                            try
                            {
                                texto = Convert.ToString(item["Grupo compras"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.grupo_compras = Convert.ToString(item["Grupo compras"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Centro logistico";
                            try
                            {
                                texto = Convert.ToString(item["Centro logistico"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.centro_logistico = Convert.ToString(item["Centro logistico"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }
                            columna = "Posicion contrato";
                            try
                            {
                                texto = Convert.ToString(item["Posicion contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.posicion_contrato = Convert.ToString(item["Posicion contrato"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }
                            columna = "Contrato operativo";
                            try
                            {
                                texto = Convert.ToString(item["Contrato operativo"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.contrato_operativo = Convert.ToString(item["Contrato operativo"]).ToUpper();
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            obj.estado = 1;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new fis_rips_prestadores_contratos();
                        }
                        else
                        {
                            throw new Exception("Codigo no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        List<fis_rips_prestadores_contratos> listaNuevos = Listado.Where(x => x.id_contrato == null || x.id_contrato == 0).ToList();
                        List<fis_rips_prestadores_contratos> listaExistentes = Listado.Where(x => x.id_contrato != null && x.id_contrato != 0).ToList();

                        if (listaExistentes.Count() > 0)
                        {
                            id_registro = BusClass.ActualizarCargueMasivoContratos(lote, listaExistentes, ref MsgRes);
                        }

                        if (listaNuevos.Count() > 0)
                        {
                            id_registro = BusClass.InsertarCargueMasivoContratos(lote, listaNuevos, ref MsgRes);
                        }

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS CONTRATOS";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }

        public string ExcelMasivoTigasContrato(DataTable dt2, fis_rips_prestadores_contrato_tigas_lote lote, ref MessageResponseOBJ MsgRes)
        {
            List<fis_rips_prestadores_contrato_tigas> Listado = new List<fis_rips_prestadores_contrato_tigas>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            List<ref_tigas_detallados> listaTig = BusClass.TraerTigasDetallados();
            List<management_fis_contratosExistentesResult> listadoContra = BusClass.ListadoContratosFISExistntes();
            List<fis_rips_prestadores_contrato_tigas> listadoTigasContrato = BusClass.listadoTIGAScontratoFIS();

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        fis_rips_prestadores_contrato_tigas obj = new fis_rips_prestadores_contrato_tigas();

                        fila++;
                        columna = "Id contrato";

                        if (!string.IsNullOrEmpty(item["Id contrato"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Id contrato";
                            try
                            {
                                texto = Convert.ToString(item["Id contrato"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_contrato = Convert.ToInt32(item["Id contrato"]);
                                    management_fis_contratosExistentesResult existeContrato = listadoContra.FirstOrDefault(x => x.id_contrato == obj.id_contrato);
                                    if (existeContrato == null)
                                    {
                                        throw new Exception("No existe contrato con este ID");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }

                            columna = "Id tiga";
                            try
                            {
                                texto = Convert.ToString(item["Id tiga"]);
                                if (!string.IsNullOrEmpty(texto))
                                {
                                    obj.id_tiga = Convert.ToInt32(item["Id tiga"]);
                                    ref_tigas_detallados existeTiga = listaTig.FirstOrDefault(x => x.id_tiga == obj.id_tiga);
                                    if (existeTiga == null)
                                    {
                                        throw new Exception("No existe TIGA con este ID");
                                    }
                                }
                                else
                                {
                                    mensajeIngresoFacturasDetalle = "No puede ir vacio";
                                    throw new Exception(mensajeIngresoFacturasDetalle);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                throw new Exception(error);
                            }


                            columna = "Validación existencia";
                            fis_rips_prestadores_contrato_tigas existeDato = listadoTigasContrato.FirstOrDefault(x => x.id_contrato == obj.id_contrato && x.id_tiga == obj.id_tiga);
                            if (existeDato != null)
                            {
                                throw new Exception($"Ya existe el TIGA {obj.id_tiga} para el contrato {obj.id_contrato}");
                            }

                            obj.estado = 1;
                            obj.fecha_digita = DateTime.Now;
                            obj.usuario_digita = SesionVar.UserName;

                            Listado.Add(obj);
                            obj = new fis_rips_prestadores_contrato_tigas();
                        }
                        else
                        {
                            throw new Exception("Codigo no puede ir vacio");
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        id_registro = BusClass.InsertarTIGASContratoMasivo(lote, Listado, ref MsgRes);

                        if (MsgRes.ResponseType == BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensajeIngresoFacturasDetalle = "SE INGRESARON CORRECTAMENTE LOS TIGAS DE CONTRATO";
                            rtaIngresoFacturasDetalle = 1;
                        }
                        else
                        {
                            mensajeIngresoFacturasDetalle = "ERROR EN EL INGRESO: " + MsgRes.DescriptionResponse;
                            rtaIngresoFacturasDetalle = 2;
                        }

                        return mensajeIngresoFacturasDetalle;
                    }
                    else
                    {
                        mensajeIngresoFacturasDetalle = "Hoja vacía.";
                        rtaIngresoFacturasDetalle = 2;
                        return mensajeIngresoFacturasDetalle;
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
                        MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " - " + ex.Message;
                    }

                    MsgRes.CodeError = ex.Message;

                    mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                    rtaIngresoFacturasDetalle = 2;
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

                mensajeIngresoFacturasDetalle = MsgRes.DescriptionResponse;
                rtaIngresoFacturasDetalle = 2;
            }

            return mensajeIngresoFacturasDetalle;
        }
    }
}
