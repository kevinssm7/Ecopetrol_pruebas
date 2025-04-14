using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;
using LinqToExcel;
using System.Data;
using System.Configuration;
using System.Text;
using Aspose.Cells;
using System.Net;
using AsaludEcopetrol.Models;
using static AsaludEcopetrol.Models.Cohortes.Cohortes;
using ECOPETROL_COMMON.ENUM;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;

namespace AsaludEcopetrol.Controllers.Cohortes
{
    [SessionExpireFilter]
    public class CohortesController : Controller
    {
        readonly Models.Cohortes.Cohortes Model = new Models.Cohortes.Cohortes();

        #region PROPIEDADES

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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();
        #endregion

        public ActionResult CargueCohortes()
        {
            ViewData["Alerta"] = "";
            ViewData["tipoalerta"] = 0;
            ViewBag.listaRegionales = BusClass.GetRefRegion();
            ViewBag.refcohortes = Model.Get_refCohortesSindh();
            return View();
        }

        [HttpPost]
        public ActionResult CargueCohortes(DateTime FechaCohorte, int tipocohorte, HttpPostedFileBase Archivo)
        {
            var ruta = "";
            var cohorte = Model.Get_refCohortes().Where(l => l.id_ref_cohortes == tipocohorte).FirstOrDefault();
            try
            {
                if (Archivo != null)
                {
                    ruta = DevolverRutaArchivo(Archivo);
                    var nombreArchivo = Archivo.FileName;

                    if (!nombreArchivo.Contains("cohorte"))
                    {
                        throw new Exception("Asegurese de que el archivo es el correcto. Este archivo no tiene en su nombre 'cohorte'");
                    }

                    string dirpath = Path.Combine(ruta);
                    WebClient User = new WebClient();
                    ruta = dirpath;
                    string filename = ruta;

                    var tipoArchivo = Path.GetExtension(Archivo.FileName);

                    Byte[] FileBuffer = User.DownloadData(filename);

                    byte[] array = new byte[0];
                    array = FileBuffer;
                    array = array.ToArray();

                    HttpPostedFileBase sigFile = (HttpPostedFileBase)new HttpPostedFileBaseCustom(array, Archivo.ContentType, Archivo.FileName + "." + tipoArchivo);

                    CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                    var asposeOptions = new Aspose.Cells.LoadOptions
                    {
                        MemorySetting = MemorySetting.MemoryPreference
                    };

                    Workbook wb = new Workbook(sigFile.InputStream, asposeOptions);
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

                    cohortes_cargue_base cargue = new cohortes_cargue_base();
                    cargue.fecha_cohorte = FechaCohorte;
                    cargue.id_ref_cohortes = tipocohorte;
                    cargue.fecha_digita = DateTime.Now;
                    cargue.usuario_digita = SesionVar.UserName;

                    var insercion = CargueMasivoDatos(dataTable, cargue, ref MsgRes);

                    var mensaje = "";

                    if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    {
                        ViewData["Alerta"] = "El cargue de registros de la cohorte ha sido exitoso! ";
                        ViewData["tipoalerta"] = 1;
                        mensaje = "Correcto";
                    }
                    else
                    {
                        ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + MsgRes.DescriptionResponse;
                        ViewData["tipoalerta"] = 2;
                        mensaje = "Error de cargue";
                    }




                    //if (tipocohorte == 1)
                    //{
                    //    List<cohortes_detalle_cargue_OK> resultadosExcel = EntidadEpoc(path);
                    //    if (resultadosExcel.Count() > 0)
                    //    {

                    //        Model.InsertCohortesEPOC(cargue, resultadosExcel, ref MsgRes);
                    //        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    //        {
                    //            ViewData["Alerta"] = "El cargue de registros de la cohorte ha sido exitoso! ";
                    //            ViewData["tipoalerta"] = 1;
                    //        }
                    //        else
                    //        {
                    //            ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + MsgRes.DescriptionResponse;
                    //            ViewData["tipoalerta"] = 2;
                    //        }
                    //    }
                    //}
                    //else if (tipocohorte == 2 || cohorte.descripcion.Contains("PAD"))
                    //{
                    //    List<cohortes_detalle_cargue_OK> resultadosExcel = EntidadPad(path);
                    //    if (resultadosExcel.Count() > 0)
                    //    {
                    //        cohortes_cargue_base cargue = new cohortes_cargue_base();
                    //        cargue.fecha_cohorte = FechaCohorte;
                    //        cargue.id_ref_cohortes = tipocohorte;
                    //        cargue.fecha_digita = DateTime.Now;
                    //        cargue.usuario_digita = SesionVar.UserName;
                    //        Model.InsertCohortesPAD(cargue, resultadosExcel, ref MsgRes);
                    //        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    //        {
                    //            ViewData["Alerta"] = "El cargue de registros de la cohorte ha sido exitoso! ";
                    //            ViewData["tipoalerta"] = 1;
                    //        }
                    //        else
                    //        {
                    //            ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + MsgRes.DescriptionResponse;
                    //            ViewData["tipoalerta"] = 2;
                    //        }
                    //    }
                    //}
                    //else if (tipocohorte == 3 || cohorte.descripcion.Contains("RCV"))
                    //{
                    //    List<cohortes_detalle_cargue_OK> resultadosExcel = EntidadRCV(path);
                    //    if (resultadosExcel.Count() > 0)
                    //    {
                    //        cohortes_cargue_base cargue = new cohortes_cargue_base();
                    //        cargue.fecha_cohorte = FechaCohorte;
                    //        cargue.id_ref_cohortes = tipocohorte;
                    //        cargue.fecha_digita = DateTime.Now;
                    //        cargue.usuario_digita = SesionVar.UserName;
                    //        Model.InsertCohortesRCV(cargue, resultadosExcel, ref MsgRes);
                    //        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    //        {
                    //            ViewData["Alerta"] = "El cargue de registros de la cohorte ha sido exitoso! ";
                    //            ViewData["tipoalerta"] = 1;
                    //        }
                    //        else
                    //        {
                    //            ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + MsgRes.DescriptionResponse;
                    //            ViewData["tipoalerta"] = 2;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    List<cohortes_detalle_cargue_OK> resultadosExcel = EntidadGestantes(path);
                    //    if (resultadosExcel.Count() > 0)
                    //    {
                    //        cohortes_cargue_base cargue = new cohortes_cargue_base();
                    //        cargue.fecha_cohorte = FechaCohorte;
                    //        cargue.id_ref_cohortes = tipocohorte;
                    //        cargue.fecha_digita = DateTime.Now;
                    //        cargue.usuario_digita = SesionVar.UserName;
                    //        Model.InsertCohortesGESTANTES(cargue, resultadosExcel, ref MsgRes);
                    //        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                    //        {
                    //            ViewData["Alerta"] = "El cargue de registros de la cohorte ha sido exitoso! ";
                    //            ViewData["tipoalerta"] = 1;
                    //        }
                    //        else
                    //        {
                    //            ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + MsgRes.DescriptionResponse;
                    //            ViewData["tipoalerta"] = 2;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("column name does not exist"))
                {
                    ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: El archivo excel, no tiene el formato correcto.";
                }
                else if (ex.Message.Contains("is not a valid worksheet name in file"))
                {
                    ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: El nombre de la hoja del archivo, no es el correcto con respecto al tipo de cohorte que  desea subir. El nombre correcto de la hoja de calculo debe ser: Cohortes";
                }
                else
                {
                    ViewData["Alerta"] = "Ha ocurrido un error en el cargue de la cohorte. Mensaje de error: " + ex.Message;
                }

                ViewData["tipoalerta"] = 2;
            }

            FileInfo fileDelete = new FileInfo(ruta);
            if (fileDelete != null)
            {
                fileDelete.Delete();
            }

            ViewBag.listaRegionales = BusClass.GetRefRegion();
            ViewBag.refcohortes = Model.Get_refCohortes().Where(l => l.aplica_adh == false).ToList();
            return View();
        }

        public Int32 CargueMasivoDatos(DataTable dt2, cohortes_cargue_base cargue, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            int idConteo = 0;
            List<cohortes_detalle_cargue_OK> OBJDetalle = new List<cohortes_detalle_cargue_OK>();
            string columna = "";
            var textError = "";

            List<Ref_regional> regional = BusClass.GetRefRegion();
            List<ref_meses_del_año> meses = BusClass.meses();
            List<ref_tipo_cohorte> tiposCohorte = BusClass.tipoCohortes();


            log_cargues_masivos logMas = new log_cargues_masivos();

            logMas.fecha_Cargue = DateTime.Now;
            logMas.nombre_digita = SesionVar.NombreUsuario;
            logMas.usuario_digita = SesionVar.UserName;
            logMas.tipo_registro = "Programas";

            //Mensaje log cargues masivos
            var mensaje = "";

            try
            {
                int conteo = dt2.Rows.Count;
                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                foreach (DataRow item in dt2.Rows)
                {
                    cohortes_detalle_cargue_OK obj = new cohortes_detalle_cargue_OK();
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["NO_IDENTIFICACIÓN"].ToString() != "")
                    {
                        var texto = "";
                        var entero = 0.0;
                        DateTime fecha = new DateTime();

                        columna = "NO_IDENTIFICACIÓN";

                        try
                        {
                            texto = Convert.ToString(item["NO_IDENTIFICACIÓN"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10)
                                {
                                    obj.no_identificacion = Convert.ToString(item["NO_IDENTIFICACIÓN"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("demasiado grande o demasiado pequeño para Int32"))
                            {
                                textError = columna + "Valor no admitido";
                            }
                            else
                            {
                                textError = columna + ex.Message;
                            }
                            throw new Exception(textError);
                        }

                        columna = "REGIONAL";
                        texto = Convert.ToString(item["REGIONAL"]);

                        if (texto.Length <= 5)
                        {
                            Ref_regional reg = new Ref_regional();
                            reg = regional.Where(x => x.indice == texto.ToUpper()).FirstOrDefault();
                            if (reg != null)
                            {
                                obj.regional = Convert.ToString(item["REGIONAL"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", no existe este índice de regional.";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MES_DE_LA_COHORTE";
                        texto = Convert.ToString(item["MES_DE_LA_COHORTE"]);
                        ref_meses_del_año mes = new ref_meses_del_año();

                        if (texto.Length <= 10)
                        {
                            mes = meses.Where(x => x.descripcion == texto.ToUpper()).FirstOrDefault();
                            if (mes != null)
                            {
                                obj.mes_cohorte = Convert.ToString(item["MES_DE_LA_COHORTE"]).ToUpper();
                                obj.id_mes_cohorte = mes.id_mes;
                            }
                            else
                            {
                                textError = columna + ", no existe este mes.";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 10 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "AÑO_DE_LA_COHORTE";
                        try
                        {
                            entero = Convert.ToInt32(item["AÑO_DE_LA_COHORTE"]);
                            if (entero != 0)
                            {
                                obj.id_año_cohorte = Convert.ToInt32(item["AÑO_DE_LA_COHORTE"]);
                                obj.año_cohorte = Convert.ToInt32(item["AÑO_DE_LA_COHORTE"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "TIPO_DE_COHORTE";
                        texto = Convert.ToString(item["TIPO_DE_COHORTE"]);

                        if (texto.Length <= 150)
                        {
                            ref_tipo_cohorte tipo = new ref_tipo_cohorte();
                            tipo = tiposCohorte.Where(x => x.descripcion.ToUpper() == texto.ToUpper()).FirstOrDefault();
                            if (tipo != null)
                            {
                                obj.tipo_cohorte = Convert.ToString(item["TIPO_DE_COHORTE"]);
                            }
                            else
                            {
                                textError = columna + ", no existe este tipo de cohorte.";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 150 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PERIODO_DE_LA_COHORTE";
                        try
                        {
                            obj.periodo_cohorte = new DateTime((int)obj.año_cohorte, (int)obj.id_mes_cohorte, 1);
                        }
                        catch (Exception ex)
                        {
                            textError = columna + " formato de fecha inválido. Debe ser mm/dd/yyyy";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cohortes_detalle_cargue_OK();
                        IntContador = IntContador + 1;
                        idConteo++;
                    }
                }

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    id_cargue = BusClass.InsertCohortesDatos(cargue, OBJDetalle, ref MsgRes);
                    mensaje = "Completado";
                   

                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    mensaje = "Error en el cargue";

                    var error = ex.Message;
                }

                logMas.id_cargue = id_cargue;
                logMas.estado_cargue = mensaje;
                logMas.periodo_cargue = cargue.fecha_cohorte;
                logMas.registros_Cargados = OBJDetalle.Count();
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                return id_cargue;
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

                if (ex.Message.Contains("no pertenece a la tabla"))
                {
                    MsgRes.DescriptionResponse += " No pertenece a la tabla";
                }

                if (ex.Message.Contains("El archivo no puede cargarse vacío"))
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.CodeError = ex.Message;

                logMas.id_cargue = id_cargue;
                logMas.estado_cargue = mensaje;
                logMas.periodo_cargue = cargue.fecha_cohorte;
                logMas.registros_Cargados = OBJDetalle.Count();
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                return 0;
            }
        }



        public string DevolverRutaArchivo(HttpPostedFileBase file)
        {

            string archivo = "";
            string rutaFinal = "";
            Models.CuentasMedicas.RadicacionElectronica Model = new Models.CuentasMedicas.RadicacionElectronica();
            List<Models.CuentasMedicas.SoportesClinicos> listasoportes = new List<Models.CuentasMedicas.SoportesClinicos>();

            ViewBag.af = false;
            try
            {
                string strRetorno = string.Empty;
                StringBuilder sbRutaDefinitiva;
                string strRutaDefinitiva = string.Empty;
                strRutaDefinitiva = ConfigurationManager.AppSettings["RutaTemporalArchivos"];
                sbRutaDefinitiva = new StringBuilder(strRutaDefinitiva);
                //string nombreSintilde = Regex.Replace(file.FileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                var extension = Path.GetExtension(file.FileName);
                string nombreSintilde = file.FileName.Replace(extension, "");

                string ruta = Path.Combine(Request.PhysicalApplicationPath, sbRutaDefinitiva + "\\" + nombreSintilde + extension);
                string dirpath = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva);

                MessageResponseOBJ MsgRes = new MessageResponseOBJ();
                string strError = string.Empty;

                DateTime fecha = DateTime.Now;
                archivo = string.Empty;
                String carpeta = "";

                if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
                {
                    carpeta = "ArchivoEPOC";
                }
                else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
                {
                    carpeta = "ArchivoEPOCPruebas";
                }
                ruta = Path.Combine(Request.PhysicalApplicationPath, strRutaDefinitiva + "\\" + carpeta);

                Models.CuentasMedicas.SoportesClinicos objGD = new Models.CuentasMedicas.SoportesClinicos();

                var nombre = Path.GetFileNameWithoutExtension(nombreSintilde);
                archivo = String.Format("{0}\\{1:yyyyMMdd_hhmmssfff}_{2}{3}", ruta,
                fecha, nombre, Path.GetExtension(file.FileName));

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                file.SaveAs(archivo);

                byte[] ExcelData = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    ExcelData = binaryReader.ReadBytes(file.ContentLength);
                }

                rutaFinal = archivo;
                return rutaFinal;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return rutaFinal;
            }
        }



        /// <summary>
        /// Mapea la entidad epoc a excel
        /// </summary>
        /// <param name="rutafichero"></param> 
        /// <returns></returns>
        //public List<cohortes_detalle_cargue_EPOC> EntidadEpoc(string rutafichero)
        //{
        //    List<cohortes_detalle_cargue_EPOC> result = new List<cohortes_detalle_cargue_EPOC>();
        //    var book = new ExcelQueryFactory(rutafichero);
        //    var resultado = (from row in book.Worksheet("Cargue EPOC")
        //                     let item = new ECOPETROL_COMMON.ENTIDADES.cohortes_detalle_cargue_EPOC
        //                     {
        //                         No_orden = row["No# De Orden"],
        //                         Coordinacion = row["Coordinación"],
        //                         Unis_actual = row["UNIS ACTUAL"],
        //                         Unis_anterior = row["UNIS ANTERIOR"],
        //                         Localidad = row["Localidad"],
        //                         Municipio_residencia = row["Municipio Residencia (DANE)"],
        //                         Tipo_documento = row["Tipo Doc"],
        //                         No_documento = row["No# Documento"],
        //                         Primer_nombre = row["Primer Nombre"],
        //                         Segundo_nombre = row["Segundo Nombre"],
        //                         Primer_apellido = row["Primer Apellido"],
        //                         Segundo_apellido = row["Segundo apellido"],
        //                         Direccion = row["Dirección"],
        //                         Telefono = row["Telefono"],
        //                         Tipo_de_beneficiario = row["Tipo de beneficiario"],
        //                         Fecha_nacimiento = row["Fecha Nacimiento"],
        //                         Genero = row["Genero"],
        //                         Mega_Responsable = row["Mega Responsable"],
        //                         Edad_Actual = row["Edad Actual"],
        //                         FR_Tabaquismo = row["FR: Tabaquismo"],
        //                         No_Cigarrillos_día = row["No# Cigarrillos día"],
        //                         Tiempo_que_lleva_fumando_en_años = row["Tiempo que lleva fumando en años"],
        //                         Indice_de_brinkmann = row["Indice de brinkmann"],
        //                         Calculo_Paquete_año = row["Cálculo Paquete/año"],
        //                         FR_Exposicion_Biomasa = row["FR: Exposición Biomasa"],
        //                         Tipo_biomasa = row["Tipo biomasa"],
        //                         Tiempo_exposicion_en_años = row["Tiempo exposición en años"],
        //                         FR_Exposicion_ocupacional = row["FR: Exposición ocupacional"],
        //                         Tipo_de_agente = row["Tipo de agente"],
        //                         Agente_exposicion_ocupacional = row["Agente exposición ocupacional"],
        //                         Agente_ocupacional_tiempo_de_exposición_en_años = row["Agente ocupacional: tiempo de exposición en años"],
        //                         FR_Contaminante_ambiental = row["FR: Contaminante ambiental"],
        //                         FR_Contaminante_ambiental_tipo = row["FR: Contaminante ambiental tipo"],
        //                         FR_Contaminante_ambiental_Tiempo_de_exposicion = row["FR: Contaminante ambiental _Tiempo de exposición"],
        //                         FR_Genetico_Deficiencia_alfa_antitripsina = row["FR: Genético, Deficiencia alfa/1/antitripsina"],
        //                         FR_Deficiencia_en_el_Crecimiento_y_desarrollo_pulmonar = row["FR: Deficiencia en el Crecimiento y desarrollo pulmonar"],
        //                         FR_Enfermedades_respiratorias_inferiores_en_la_infancia = row["FR:Enfermedades respiratorias inferiores en la infancia"],
        //                         FR_Tiene_antecedente_de_TB = row["FR: Tiene antecedente de TB?"],
        //                         FR_Tipo_de_TB_diagnosticada = row["FR: Tipo de TB diagnosticada"],
        //                         Fecha_diagnóstico_TBC = row["Fecha diagnóstico TBC (AAAA/MM/DD)"],
        //                         Recibio_tratamiento = row["Recibió tratamiento?"],
        //                         Fecha_Diagnóstico_EPOC = row["Fecha Diagnóstico de la EPOC_ (AAAA/MM/DD)"],
        //                         Espirometria_Diagnóstico_FVC = row["Espirometria de Diagnóstico: FVC _(describa el valor reportado)"],
        //                         Espirometria_Diagnóstico_FEV1 = row["Espirometria de Diagnóstico: FEV1_(describa el valor reportado)"],
        //                         Espirometria_Diagnóstico_FEV1_FVC = row["Espirometria de Diagnóstico: FEV1/FVC_(describa el valor reporta"],
        //                         Espirometria_Año_anterior_FEV1POS_BD = row["Espirometria de Diagnóstico: FEV1POS BD_(describa el valor repor"],
        //                         Espirometria_Diagnóstico_FEV1_FVCPOS_BD = row["Espirometria de Diagnóstico: FEV1/FVC POS BD_(describa el valor "],
        //                         Porcentaje_Limitación_pulmonar = row["% Limitación pulmonar "],
        //                         Clasificación_Limitación_Flujo_diagnóstico_según_GOLD = row["Clasificación Limitación Flujo del diagnóstico según GOLD"],
        //                         Fecha_realización_Escala_valoración_disnea_mMRC = row["Fecha realización Escala de valoración de la disnea mMRC "],
        //                         Resultado_Escala_valoración_disnea_mMRC = row["Resultado Escala de valoración de la disnea mMRC "],
        //                         Fecha_realización_Prueba_Evaluación_EPOC = row["Fecha realización Prueba de Evaluación de la EPOC (CAT)"], //cambiar el nombre de base de datos por cat 1
        //                         Resultado_Evaluación_EPOC = row["Resultado Evaluación de la EPOC (CAT)"],
        //                         Tratamiento_Instaurado_Diagnóstico = row["Tratamiento Instaurado al Diagnóstico"],
        //                         Se_trata_de_un_EPOC = row["Se trata de un EPOC"],
        //                         Espirometria_Año_anterior_FVC = row["Espirometria Año anterior: FVC"],
        //                         Espirometria_Año_anterior_FEV1 = row["Espirometria Año anterior: FEV1"],
        //                         Espirometria_Año_anterior_FEV1_FVC = row["Espirometria Año anterior: FEV1/FVC"],
        //                         //espirome = row["Espirometria Año anterior FEV1POS BD"],
        //                         Espirometria_Año_anterior_FEV1_FVCPOS_BD = row["Espirometria Año anterior: FEV1/FVC POS BD"],
        //                         Porcentaje_Limitación_Funcional = row["% Limitación Funcional"],
        //                         Clasificación_Limitación_Flujo_diagnóstico = row["Clasificación Limitación Flujo del diagnóstico"],
        //                         exacerbaciones_paciente_año_anterior = row["El año anterior Cuantos exacerbaciones presento el paciente?"],
        //                         tratamiento_exacerbaciones_causaron_hospitalizacion = row["El tratamiento de las exacerbaciones causaron hospitalización?"],
        //                         Tratamiento_Instaurado_año_anterior = row["Tratamiento Instaurado año anterior"],
        //                         Presenta_Comorbilidades = row["Presenta Comorbilidades"],
        //                         Comorbilidad_1 = row["Comorbilidad 1 (registre CIE 10)"],
        //                         Comorbilidad_2 = row["Comorbilidad 2(registre CIE 10)"],
        //                         Comorbilidad_3 = row["Comorbilidad 3 (registre CIE 10)"],
        //                         Comorbilidad_4 = row["Comorbilidad 4 (registre CIE 10)"],
        //                         Primer_Control_Fecha_atencion = row["Primer Control: Fecha de atención (AAAA/MM/DD)"],
        //                         Primero_Control_Profesional_realiza_atencion = row["Primero Control: Profesional que realiza atención"],
        //                         Primer_Control_Especialidad_medico_que_realiza_consulta = row["Especialidad medico que realiza consulta"],
        //                         Primero_Control_TAS = row["Primero Control: TAS"],
        //                         Primero_Control_TAD = row["Primero Control: TAD"],
        //                         Primero_Control_Peso = row["Primero Control: Peso"],
        //                         Primero_Control_Talla = row["Primero Control: Talla"],
        //                         Primer_Control_IMC = row["Primer Control: IMC"],
        //                         Primer_control_Habito_tabáquico = row["Primer control: Habito tabáquico?"],
        //                         Primer_Control_Numero_paquetes_año = row["Numero de paquetes año"],
        //                         Primer_control_Realiza_actividad_fisica = row["Primer control: Realiza actividad fisica"],
        //                         Primer_control_Terapia_oxigeno = row["Primer control: Terapia oxigeno"],
        //                         Primer_Control_Fecha_Prescripción_Oxigeno = row["Fecha Prescripción Oxigeno _(AAAA/MM/DD)"],
        //                         Primer_control_Terapia_oxigeno_tiempo_al_día = row["Primer control: _Terapia oxigeno, tiempo al día (horas)"],
        //                         Primer_control_Apoyo_ventilatorio = row["Primer control: Apoyo ventilatorio"],
        //                         Primer_control_Tipo_apoyo_ventilatorio = row["Primer control: Tipo apoyo ventilatorio"],
        //                         Primer_Control_Incluido_programa_rehabilitacion_pulmon = row["Primer Control: Incluido en el programa de rehabilitación pulmon"],
        //                         Primer_control_Causa_inclusión_programa_de_rehabilitacion = row["Primer control: Causa de inclusión en programa de rehabilitación"],
        //                         Primer_Control_Clasificación_del_paciente = row["Primer Control: Clasificación del paciente"],
        //                         Primer_control_Tratamiento_Farmacológico_Instaurado_Diagnóst = row["Primer control: Tratamiento Farmacológico Instaurado al Diagnóst"],
        //                         Primer_control_Tratamiento_NO_Farmacológico_Instaurado_al_Diagn = row["Primer control: Tratamiento NO Farmacológico Instaurado al Diagn"],
        //                         Segundo_control_Fecha_de_atención = row["Segundo control: Fecha de atención (AAAA/MM/DD)"],
        //                         Segundo_Control_Profesional_que_realiza_atención = row["Segundo Control: Profesional que realiza atención"],
        //                         Segundo_Control_Especialidad_medico_que_realiza_consulta1 = row["Especialidad medico que realiza consulta1"],
        //                         Segundo_Control_TAS = row["Segundo Control: TAS"],
        //                         Segundo_Control_TAD = row["Segundo  Control: TAD"],
        //                         Segundo_Control_Peso = row["Segundo  Control: Peso"],
        //                         Segundo_Control_Talla = row["Segundo Control: Talla"],
        //                         Segundo_Control_IMC = row["Segundo Control: IMC"],
        //                         Segundo_control_Habito_tabáquico = row["Segundo control: Habito tabáquico?"],
        //                         Segundo_control_No_Paquetes_año = row["No# Paquetes /año"],
        //                         Segundo_control_Realiza_actividad_fisica = row["Segundo control: Realiza actividad fisica"],
        //                         Segundo_control_Terapia_oxigeno = row["Segundo control: Terapia oxigeno"],
        //                         Segundo_Fecha_Prescripción_Oxigeno = row["Fecha Prescripción Oxigeno (AAAA/MM/DD)"],
        //                         Segundo_control_Terapia_oxigeno_tiempo_al_día = row["Segundo control: Terapia oxigeno, tiempo al día (horas)"],
        //                         Segundo_control_Apoyo_ventilatorio = row["Segundo control: Apoyo ventilatorio"],
        //                         Segundo_control_Tipo_apoyo_ventilatorio = row["Segundo control: Tipo apoyo ventilatorio"],
        //                         Segundo_control_Incluido_programa_de_rehabilitación_pulmo = row["Segundo control: Incluido en el programa de rehabilitación pulmo"],
        //                         Segundo_control_Causa_inclusión_programa_de_rehabilitación = row["Segundo control: Causa de inclusión en programa de rehabilitació"],
        //                         Segundo_control_Clasificación_del_paciente = row["Segundo control: Clasificación del paciente"],
        //                         Segundo_control_Tratamiento_Farmacológico_Instaurado = row["Segundo control: Tratamiento Farmacológico Instaurado"],
        //                         Segundo_control_Tratamiento_NO_Farmacológico_Instaurado_al_Diag = row["Segundo control: Tratamiento NO Farmacológico Instaurado al Diag"],
        //                         Tercer_control_Fecha_de_atención = row["Tercer control: Fecha de atención (AAAA/MM/DD)"],
        //                         Tercer_control_Profesional_que_realiza_atención = row["Tercer control: Profesional que realiza atención"],
        //                         Tercer_control_Especialidad_medico_que_realiza_consulta = row["Especialidad medico que realiza consulta2"],
        //                         Tercer_control_TAS = row["Tercer control: TAS"],
        //                         Tercer_Control_TAD = row["Tercer Control: TAD"],
        //                         Tercer_Control_Peso = row["Tercer Control: Peso"],
        //                         Tercer_control_Talla = row["Tercer control: Talla"],
        //                         Tercer_Control_IMC = row["Tercer Control: IMC"],
        //                         Tercer_control_Habito_tabáquico = row["Tercer control: Habito tabáquico?"],
        //                         Tercer_control_No_Paquetes_año = row["No# Paquetes /año1"],
        //                         Tercer_control_Realiza_actividad_fisica = row["Tercer control: Realiza actividad fisica"],
        //                         Tercer_control_Terapia_oxigeno = row["Tercer control: Terapia oxigeno"],
        //                         Tercer_control_Fecha_Prescripción_Oxigeno = row["Fecha Prescripción Oxigeno (AAAA/MM/DD)1"],
        //                         Tercer_control_Terapia_oxigeno_tiempo_al_día = row["Tercer control: Terapia oxigeno, tiempo al día (horas)"],
        //                         Tercer_control_Apoyo_ventilatorio = row["Tercer control: Apoyo ventilatorio"],
        //                         Tercer_control_Tipo_apoyo_ventilatorio = row["Tercer control: Tipo apoyo ventilatorio"],
        //                         Tercer_control_Incluido_programa_de_rehabilitación_pulmon = row["Tercer control: Incluido en el programa de rehabilitación pulmon"],
        //                         Tercer_control_Causa_inclusión_programa_de_rehabilitación = row["Tercer control: Causa de inclusión en programa de rehabilitación"],
        //                         Tercer_control_Clasificación_del_paciente = row["Tercer control: Clasificación del paciente"],
        //                         Tercer_control_Tratamiento_Farmacológico_Instaurado = row["Tercer control: Tratamiento Farmacológico Instaurado"],
        //                         Tercer_control_Tratamiento_NO_Farmacológico_Instaurado_al_Diagn = row["Tercer control: Tratamiento NO Farmacológico Instaurado al Diagn"],
        //                         Cuarto_control_Fecha_de_atención = row["Cuarto control: Fecha de atención (AAAA/MM/DD)"],
        //                         Cuartocontrol_Profesional_que_realiza_atención = row["Cuarto control: Profesional que realiza atención"],
        //                         Cuarto_control_Especialidad_medico_que_realiza_consulta = row["Especialidad medico que realiza consulta3"],
        //                         Cuarto_control_TAS = row["Cuarto control: TAS"],
        //                         Cuarto_Control_TAD = row["Cuarto Control: TAD"],
        //                         Cuarto_Control_Peso = row["Cuarto Control: Peso"],
        //                         Cuarto_control_Talla = row["Cuarto control: Talla"],
        //                         Cuarto_Control_IMC = row["Cuarto Control: IMC"],
        //                         Cuarto_control_Habito_tabáquico = row["Cuarto control: Habito tabáquico?"],
        //                         Cuarto_control_No_Paquetes_año = row["No# Paquetes /año2"],
        //                         Cuarto_control_Realiza_actividad_fisica = row["Cuarto control: Realiza actividad fisica"],
        //                         Cuarto_control_Terapia_oxigeno = row["Cuarto control: Terapia oxigeno"],
        //                         Cuarto_control_Fecha_Prescripción_Oxigeno = row["Fecha Prescripción Oxigeno (AAAA/MM/DD)2"],
        //                         Cuarto_control_Terapia_oxigeno_tiempo_al_día = row["Cuarto control: Terapia oxigeno, tiempo al día (horas)"],
        //                         Cuarto_control_Apoyo_ventilatorio = row["Cuarto control: Apoyo ventilatorio"],
        //                         Cuarto_control_Tipo_apoyo_ventilatorio = row["Cuarto control: Tipo apoyo ventilatorio"],
        //                         Cuarto_control_Incluido_programa_de_rehabilitación_pulmon = row["Cuarto control: Incluido en el programa de rehabilitación pulmon"],
        //                         Cuarto_control_Causa_inclusión_programa_de_rehabilitación = row["Cuarto control: Causa de inclusión en programa de rehabilitación"],
        //                         Cuarto_control_Clasificación_del_paciente = row[" Cuarto control: Clasificación del paciente"],
        //                         Cuarto_control_Tratamiento_Farmacológico_Instaurado = row["Cuarto  control: Tratamiento Farmacológico Instaurado"],
        //                         Cuarto_control_Tratamiento_NO_Farmacológico_Instaurado_al_Diagn = row["Cuarto control: Tratamiento NO Farmacológico Instaurado al Diagn"],
        //                         Quinto_control_Fecha_de_atención = row["Quinto control: Fecha de atención (AAAA/MM/DD)"],
        //                         Quinto_control_Profesional_que_realiza_atención = row["Quinto control: Profesional que realiza atención"],
        //                         Quinto_control_Especialidad_medico_que_realiza_consulta = row["Especialidad medico que realiza consulta4"],
        //                         Quinto_control_TAS = row["Quinto control: TAS"],
        //                         Quinto_Control_TAD = row["Quinto Control: TAD"],
        //                         Quinto_Control_Peso = row["Quinto  Control: Peso"],
        //                         Quinto_control_Talla = row["Quinto control: Talla"],
        //                         Quinto_Control_IMC = row["Quinto Control: IMC"],
        //                         Quinto_control_Habito_tabáquico = row["Quinto control: Habito tabáquico?"],
        //                         Quinto_control_No_Paquetes_año = row["No# Paquetes /año3"],
        //                         Quinto_control_Realiza_actividad_fisica = row["Quinto control: Realiza actividad fisica"],
        //                         Quinto_control_Terapia_oxigeno = row["Quinto control: Terapia oxigeno"],
        //                         Quinto_control_Fecha_Prescripción_Oxigeno = row["Fecha Prescripción Oxigeno (AAAA/MM/DD)3"],
        //                         Quinto_control_Terapia_oxigeno_tiempo_al_día = row["Quinto control: Terapia oxigeno, tiempo al día (horas)"],
        //                         Quinto_control_Apoyo_ventilatorio = row["Quinto control: Apoyo ventilatorio"],
        //                         Quinto_control_Tipo_apoyo_ventilatorio = row["Quinto control: Tipo apoyo ventilatorio"],
        //                         Quinto_control_Incluido_programa_de_rehabilitación_pulmon = row["Quinto control: Incluido en el programa de rehabilitación pulmon"],
        //                         Quinto_control_Causa_inclusión_programa_de_rehabilitación = row["Quinto control: Causa de inclusión en programa de rehabilitación"],
        //                         Quinto_control_Clasificación_del_paciente = row[" Quinto control: Clasificación del paciente"],
        //                         Quinto_control_Tratamiento_Farmacológico_Instaurado = row["Quinto  control: Tratamiento Farmacológico Instaurado"],
        //                         Quinto_control_Tratamiento_NO_Farmacológico_Instaurado_al_Diag = row["Quinto  control: Tratamiento NO Farmacológico Instaurado al Diag"],
        //                         Fecha_realización_Escala_valoración_disnea_mMRC1 = row["Fecha realización Escala de valoración de la disnea mMRC 1_(AAAA"],
        //                         Resultado_Escala_valoración_disnea_mMRC1 = row["Resultado Escala de valoración de la disnea mMRC1 "],
        //                         Fecha_realización_Prueba_Evaluación_EPOC_1 = row["Fecha realización Prueba de Evaluación de la EPOC (CAT) 1_ (AAAA"],
        //                         Resultado_Evaluación_EPOC_1 = row["Resultado Evaluación de la EPOC (CAT) 1"],
        //                         Fecha_realización_Escala_valoración_disnea_mMRC_2 = row["Fecha realización Escala de valoración de la disnea mMRC 2_(AAAA"],
        //                         ResultadoEscala_valoración_disnea_mMRC_2 = row["Resultado Escala de valoración de la disnea mMRC 2"],
        //                         Fecha_realizacion_Prueba_Evaluacion_EPOC_CAT_2 = row["Fecha realización Prueba de Evaluación de la EPOC (CAT) 2_ (AAAA"],
        //                         Resultado_Evaluacion_EPOC_CAT_2 = row["Resultado Evaluación de la EPOC (CAT) 2"],
        //                         Fecha_realizacion_Escala_valoracion_disnea_mMRC_3 = row["Fecha realización Escala de valoración de la disnea mMRC 3_(AAAA"],
        //                         Resultado_Escala_valoración_disnea_mMRC_3 = row["Resultado Escala de valoración de la disnea mMRC 3"],
        //                         Fecha_realizacion_Prueba_de_Evaluacion_EPOC_CAT_3 = row["Fecha realización Prueba de Evaluación de la EPOC (CAT) 3_ (AAAA"],
        //                         Resultado_Evaluacion_EPOC_CAT_3 = row["Resultado Evaluación de la EPOC (CAT) 3"],
        //                         Vacunacion_Neumococo = row["Vacunación:  Neumococo"],
        //                         Vacunacion_Fecha_Neumococo = row["Vacunación: Fecha Neumococo _(AAAA/MM/DD)"],
        //                         Vacunacion_Tipo_vacuna_neumococcica = row["Vacunación: Tipo de vacuna neumococcica"],
        //                         No_Dosis = row["No# Dosis"],
        //                         Vacunacion_Influenza = row["Vacunación:  Influenza"],
        //                         Vacunacion_Fecha_Influenza = row["Vacunación: Fecha Influenza_(AAAA/MM/DD)"],
        //                         Hospitalizacion = row["Hospitalización"],
        //                         Fecha_Hospitalizacion = row["Fecha Hospitalización (AAAA/MM/DD)"],
        //                         Corresponde_exacerbacion = row["Corresponde a una exacerbación"],
        //                         Causa_hospitalizacion_registreCIE10 = row["Causa hospitalización (registre CIE 10)"],
        //                         Dias_estancia_Hospitalaria = row["Dias de estancia Hospitalaria"],
        //                         Hospitalizacion1 = row["Hospitalización 1"],
        //                         Fecha_Hospitalizacion_1 = row["Fecha Hospitalización 1 (AAAA/MM/DD)"],
        //                         Corresponde_exacerbacion_1 = row["Corresponde a una exacerbación 1?"],
        //                         Causa_hospitalización_1_registreCIE10 = row["Causa hospitalización 1 (registre CIE 10)"],
        //                         Dias_estancia_Hospitalaria_1 = row["Dias de estancia Hospitalaria 1"],
        //                         Hospitalizacion_2 = row["Fecha Hospitalización 2 (AAAA/MM/DD)"],
        //                         Fecha_Hospitalización_2 = row["Hospitalización 2"],
        //                         Corresponde_exacerbacion_2 = row["Corresponde a una exacerbación 2?"],
        //                         Causa_hospitalizacion_2_registreCIE20 = row["Causa hospitalización 2 (registre CIE 20)"],
        //                         Dias_estancia_Hospitalaria_2 = row["Dias de estancia Hospitalaria 2"],
        //                         Atencion_Equipo_Interdisciplinario_Enfermeria_1 = row["Atención Equipo Interdisciplinario: Enfermería 1"],
        //                         Fecha_Atencion_Enfermería_1 = row["Fecha Atención Enfermería  1"],
        //                         Plan_esturcturado_educación_Enfermeria_1 = row["Plan esturcturado educación Enfermería 1"],
        //                         Avance_Plan_estruturado_de_Atencion_1 = row["Avance Plan estruturado de Atencion 1"],
        //                         Atencion_Equipo_Interdisciplinario_Enfermería_2 = row["Atención Equipo Interdisciplinario: Enfermería 2"],
        //                         Fecha_Atencion_Enfermeria_2 = row["Fecha Atención Enfermería  2"],
        //                         Plan_esturcturado_educacion_Enfermeria_2 = row["Plan esturcturado educación Enfermería 2"],
        //                         Avance_Plan_estruturado_de_Atencion_2 = row["Avance Plan estruturado de Atencion 2"],
        //                         Atencion_Equipo_Interdisciplinario_Psicología_1 = row["Atención Equipo Interdisciplinario: Psicología   1"],
        //                         Fecha_Atención_Psicología_1 = row["Fecha Atención Psicología 1"],
        //                         Atencion_Equipo_Interdisciplinario_Nutricion_1 = row["Atención Equipo Interdisciplinario: Nutrición  1"],
        //                         Fecha_Atencion_Nutricion_1 = row["Fecha Atención Nutrición 1"],
        //                         Estado_nutricional_1 = row["Estado nutricional 1"],

        //                     }
        //                     select item).ToList();

        //    result = resultado;
        //    System.IO.File.Delete(rutafichero);
        //    return result;

        //}

        public List<cohortes_detalle_cargue_OK> EntidadEpoc(string rutafichero)
        {
            List<cohortes_detalle_cargue_OK> result = new List<cohortes_detalle_cargue_OK>();
            var book = new ExcelQueryFactory(rutafichero);
            var resultado = (from row in book.Worksheet("Cohortes")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cohortes_detalle_cargue_OK
                             {
                                 no_identificacion = row["NO_IDENTIFICACIÓN"],
                                 regional = row["REGIONAL"],
                                 id_año_cohorte = int.Parse(row["ID_AÑO_DE_LA_COHORTE"]),
                                 año_cohorte = int.Parse(row["AÑO_DE_LA_COHORTE_"]),
                                 id_mes_cohorte = int.Parse(row["ID_MES_DE_LA_COHORTE"]),
                                 mes_cohorte = row["MES_DE_LA_COHORTE_"],
                             }
                             select item).ToList();

            result = resultado;
            if (System.IO.File.Exists(rutafichero))
            {
                System.IO.File.Delete(rutafichero);
            }

            return result;

        }
        /// <summary>
        /// Mapea la endidad pad a excel
        /// </summary>
        /// <param name="rutafichero"></param>
        /// <returns></returns>
        public List<cohortes_detalle_cargue_OK> EntidadPad(string rutafichero)
        {
            List<cohortes_detalle_cargue_OK> result = new List<cohortes_detalle_cargue_OK>();
            var book = new ExcelQueryFactory(rutafichero);
            var resultado = (from row in book.Worksheet("Cohortes")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cohortes_detalle_cargue_OK
                             {
                                 no_identificacion = row["NO_IDENTIFICACIÓN"],
                                 regional = row["REGIONAL"],
                                 id_año_cohorte = int.Parse(row["ID_AÑO_DE_LA_COHORTE"]),
                                 año_cohorte = int.Parse(row["AÑO_DE_LA_COHORTE_"]),
                                 id_mes_cohorte = int.Parse(row["ID_MES_DE_LA_COHORTE"]),
                                 mes_cohorte = row["MES_DE_LA_COHORTE_"],
                             }
                             select item).ToList();

            book.Dispose();
            result = resultado;
            if (System.IO.File.Exists(rutafichero))
            {
                System.IO.File.Delete(rutafichero);
            }

            return result;


        }

        /// <summary>
        /// Mapea la entidad gestantes a excel
        /// </summary>
        /// <param name="rutafichero"></param>
        /// <returns></returns>
        public List<cohortes_detalle_cargue_OK> EntidadGestantes(string rutafichero)
        {
            List<cohortes_detalle_cargue_OK> result = new List<cohortes_detalle_cargue_OK>();

            var book = new ExcelQueryFactory(rutafichero);
            var resultado = (from row in book.Worksheet("Cohortes")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cohortes_detalle_cargue_OK
                             {
                                 no_identificacion = row["NO_IDENTIFICACIÓN"],
                                 regional = row["REGIONAL"],
                                 id_año_cohorte = int.Parse(row["ID_AÑO_DE_LA_COHORTE"]),
                                 año_cohorte = int.Parse(row["AÑO_DE_LA_COHORTE_"]),
                                 id_mes_cohorte = int.Parse(row["ID_MES_DE_LA_COHORTE"]),
                                 mes_cohorte = row["MES_DE_LA_COHORTE_"],
                             }
                             select item).ToList();
            result = resultado;
            if (System.IO.File.Exists(rutafichero))
            {
                System.IO.File.Delete(rutafichero);
            }

            return result;
        }

        /// <summary>
        /// Mapea la entidad rcv a excel
        /// </summary>
        /// <param name="rutafichero"></param>
        /// <returns></returns> 
        /// 
        public List<cohortes_detalle_cargue_OK> EntidadRCV(string rutafichero)
        {
            List<cohortes_detalle_cargue_OK> result = new List<cohortes_detalle_cargue_OK>();

            var book = new ExcelQueryFactory(rutafichero);
            var resultado = (from row in book.Worksheet("Cohortes")
                             let item = new ECOPETROL_COMMON.ENTIDADES.cohortes_detalle_cargue_OK
                             {
                                 no_identificacion = row["NO_IDENTIFICACIÓN"],
                                 regional = row["REGIONAL"],
                                 id_año_cohorte = int.Parse(row["ID_AÑO_DE_LA_COHORTE"]),
                                 año_cohorte = int.Parse(row["AÑO_DE_LA_COHORTE_"]),
                                 id_mes_cohorte = int.Parse(row["ID_MES_DE_LA_COHORTE"]),
                                 mes_cohorte = row["MES_DE_LA_COHORTE_"],
                             }
                             select item).ToList();
            result = resultado;
            if (System.IO.File.Exists(rutafichero))
            {
                System.IO.File.Delete(rutafichero);
            }

            return result;
        }

        //public List<cohortes_detalle_cargue_RCV> EntidadRCV(string rutafichero)
        //{
        //    List<cohortes_detalle_cargue_RCV> result = new List<cohortes_detalle_cargue_RCV>();
        //    var book = new ExcelQueryFactory(rutafichero);

        //    var EData1 = (from c in book.WorksheetRange("A1", "IU50000", "cargue RCV") select c).ToList();
        //    var EData2 = (from c in book.WorksheetRange("IV1", "SP50000", "cargue RCV") select c).ToList();
        //    var EData3 = (from c in book.WorksheetRange("SQ1", "VZ50000", "cargue RCV") select c).ToList();

        //    for (var i = 0; i < EData1.Count; i++)
        //    {
        //        cohortes_detalle_cargue_RCV obj = new cohortes_detalle_cargue_RCV();
        //        var item = EData1[i];
        //        var item2 = EData2[i];
        //        var item3 = EData3[i];

        //        obj.No_De_Orden = item[0];
        //        obj.Coordinación_Actual = item[1];
        //        obj.UNISActual = item[2];
        //        obj.Ciudaddeserviciosmedicos = item[3];
        //        obj.TipoBeneficiario = item[4];
        //        obj.DepartamentoResidencia = item[5];
        //        obj.MunicipioResidencia = item[6];
        //        obj.FechaingresoalprogramaRCV_ = item[7];
        //        obj.ViadeCaptación = item[8];
        //        obj.PrimerApellido = item[9];
        //        obj.SegundoApellido = item[10];
        //        obj.PrimerNombre = item[11];
        //        obj.SegundoNombre = item[12];
        //        obj.TipoIdentificación = item[13];
        //        obj.No_Identificación = item[14];
        //        obj.FechaNacimiento_ = item[15];
        //        obj.SegundoControl2019_deTA = item[16];
        //        obj.EdadActual = item[17];
        //        obj.Sexo_M1_F0 = item[18];
        //        obj.Estadocivil = item[19];
        //        obj.Escolaridad = item[20];
        //        obj.DirecciónResidencia = item[21];
        //        obj.Telefonocontacto1 = item[22];
        //        obj.Telefonocontacto2 = item[23];
        //        obj.MEGAResponsable = item[24];
        //        obj.DX_HipertensiónArterial = item[25];
        //        obj.FechadiagnósticoHTA = item[26];
        //        obj.DX_DiabetesMellitus = item[27];
        //        obj.FechadiagnósticoDM = item[28];
        //        obj.Dx_DISLIPIDEMIAS = item[29];
        //        obj.FechadiagnósticoDislipidemia = item[30];
        //        obj.Dx_OBESIDAD = item[31];
        //        obj.FechadiagnósticoObesidad = item[32];
        //        obj.Dx_ERC = item[33];
        //        obj.FechadiagnósticoERC = item[34];
        //        obj.Dx_EVENTOSCEREBROCARDIO_VASCULAR = item[35];
        //        obj.FechadiagnósticoECCV = item[36];
        //        obj.ComorbilidadesPresentes = item[37];
        //        obj.ComorbilidadesCodigoCIE101 = item[38];
        //        obj.ComorbilidadesCodigoCIE102 = item[39];
        //        obj.ComorbilidadesCodigoCIE103 = item[40];
        //        obj.ComorbilidadesCodigoCIE104 = item[41];
        //        obj.FRCV_TABACO = item[42];
        //        obj.INDICEDEBrinkman = item[43];
        //        obj.FRCV_ALCOHOL = item[44];
        //        obj.FRCV_SEDENTARISMO = item[45];
        //        obj.FRCV_DIETA = item[46];
        //        obj.FRCV_SOBREPESO_OBESIDAD = item[47];
        //        obj.FRCV_OBESIDADABDOMINAL = item[48];
        //        obj.FRCV_ESTRÉS_CONDICIONESLABORALES = item[49];
        //        obj.ClasificaciónRiesgoCValingresoDx = item[50];
        //        obj.ValoraciónriesgodiabetesDx = item[51];
        //        obj.TipodeDiabetesDx = item[52];
        //        obj.InsulinorequirienteDx = item[53];
        //        obj.ProgramatelemonitoreoDx = item[54];
        //        obj.CriteriodeinclusiónalprogramadetelemonitoreoDx = item[55];
        //        obj.Peso_Dx = item[56];
        //        obj.Talla_Dx = item[57];
        //        obj.IMCDX = item[58];
        //        obj.TASistolicaDx = item[59];
        //        obj.TADiastólicaDx = item[60];
        //        obj.CinturaDx = item[61];
        //        obj.Cadera_Dx = item[62];
        //        obj.IndiceCintura_caderaDx = item[63];
        //        obj.OBESIDADABDOMILDx_Hombres = item[64];
        //        obj.GlicemiaBasalDX = item[65];
        //        obj.HemoglobinaGlicosiladaDx = item[66];
        //        obj.HematocritoDx = item[67];
        //        obj.HemoglobinaDx = item[68];
        //        obj.CreatininaensangreDx = item[69];
        //        obj.MicroalbuminuriaDx = item[70];
        //        obj.Creatinuria_Dx = item[71];
        //        obj.ColesterolTotalDx = item[72];
        //        obj.ColesterolHDLDx = item[73];
        //        obj.ColesterolLDLDx = item[74];
        //        obj.TrigliceridosDx = item[75];
        //        obj.ParatohormonaDx = item[76];
        //        obj.TFGDxCrokoft = item[77];
        //        obj.EstadioERCDx = item[78];
        //        obj.EKGDx = item[79];
        //        obj.OtrasalteracionesdelEKGDx = item[80];
        //        obj.RetinopatíaDx = item[81];
        //        obj.FechadiagnósticoRetinopatiaDx = item[82];
        //        obj.EnfermedadRenalDx = item[83];
        //        obj.FechadiagnósticoERalDx = item[84];
        //        obj.ConductaconERalDx = item[85];
        //        obj.ProveedorQueatiendealbeneficiario = item[86];
        //        obj.FechadiagnósticoCoductaERalDx = item[87];
        //        obj.Piediabético = item[88];
        //        obj.FechadiagnósticoPieDiabeticoDx = item[89];
        //        obj.ConductaPieDiabéticoDx = item[90];
        //        obj.FechadiagnósticoConductaPieDiabeticoDx = item[91];
        //        obj.EventocardiovascularDx = item[92];
        //        obj.FechadiagnósticoCardiovascularDx = item[93];
        //        obj.EventocerebrovascularDx = item[94];
        //        obj.FechadiagnósticoECVDx = item[95];
        //        obj.DiagnósticorealizadoenECP = item[96];
        //        obj.Tratamiento_Registrarporgrupofarmacéutico_separadosporcom = item[97];
        //        obj.NombredelMedicocabeceraUltimaconsultaañoanterior = item[98];
        //        obj.FechadelControlUltimocontrolañoanterior_ = item[99];
        //        obj.PacienteAtenciónDomiciliaria = item[100];
        //        obj.TABACOUltimoControl = item[101];
        //        obj.INDICEDEBrinkmanUltimoControl = item[102];
        //        obj.ALCOHOL_UltimoControl = item[103];
        //        obj.SEDENTARISMOUltimoControl = item[104];
        //        obj.DIETA_UltimoControl = item[105];
        //        obj.SOBREPESO_OBESIDAD_UltimoControl = item[106];
        //        obj.OBESIDADABDOMINAL_UltimoControl = item[107];
        //        obj.ESTRÉS_CONDICIONESLABORALES_UltimoControl = item[108];
        //        obj.UltimoPesoAñoanterior = item[109];
        //        obj.UltimaTallaañoanterior = item[110];
        //        obj.IMCUltimaAñoanterior = item[111];
        //        obj.CinturaUltimoAñoanterior = item[112];
        //        obj.CaderaUltimoAñoAnterior = item[113];
        //        obj.IndiceCintura_cadera = item[114];
        //        obj.OBESIDADABDOMINAL = item[115];
        //        obj.TASistólicaAñoAnterior = item[116];
        //        obj.TADiastóliccaAñoAnterior = item[117];
        //        obj.ControlTAAñoanterior = item[118];
        //        obj.ClasificaciónRiesgoCValingreso = item[119];
        //        obj.Valoraciónriesgodiabetes = item[120];
        //        obj.HemoglobinaGlicosiladaUltimoControl = item[121];
        //        obj.ColesterolTotalUltimoControl = item[122];
        //        obj.ColesterolHDLUltimocontrol = item[123];
        //        obj.ColesterolLDLUltimocontrol = item[124];
        //        obj.TrigliceridosUltimocontrol = item[125];
        //        obj.CreatininaenSangreUltimoControl = item[126];
        //        obj.Paratohormona_1 = item[127];
        //        obj.Microalbuminuria = item[128];
        //        obj.Edadalmomentodelaúltimaconsultadelaño2018 = item[129];
        //        obj.TFGAñoanterior_Crokoft = item[130];
        //        obj.EstadioAñoAnterior = item[131];
        //        obj.PrimerControl2019Año2019_Nombredelmedicodecabecera = item[132];
        //        obj.PrimerControl2019_FechadelControl2019_ = item[133];
        //        obj.PrimerControl2019EdadMomentodelaconsulta = item[134];
        //        obj.PrimerControl2019_Peso = item[135];
        //        obj.PrimerControl2019_Talla = item[136];
        //        obj.PrimerControl2019IMC = item[137];
        //        obj.PrimerControl2019TASistolica = item[138];
        //        obj.PrimerControl2019TADiastólica = item[139];
        //        obj.PrimerControl2019Control2019deTA = item[140];
        //        obj.PrimerControl2019Cintura = item[141];
        //        obj.PrimerControl2019Cadera = item[142];
        //        obj.PrimerControl2019IndiceCintura_cadera = item[143];
        //        obj.PrimerControl2019OBESIDADABDOMINAL = item[144];
        //        obj.PrimerControl2019ClasificaciónRiesgoCV = item[145];
        //        obj.PrimerControl2019Valoraciónriesgodiabetes = item[146];
        //        obj.PrimerControl2019_UsuarioenProgramadeAtenciónDomiciliar = item[147];
        //        obj.Primercontol_UsuariodelProgramadeAcondicionamientoFisic = item[148];
        //        obj.PrimerControl2019TFGCrokoft = item[149];
        //        obj.PrimerControl2019CKD_EPI = item[150];
        //        obj.PrimerControl2019EstadioERCCrokoft = item[151];
        //        obj.PrimerControl2019_Tratamiento = item[152];
        //        obj.PrimerControl2019CONDUCTA = item[153];
        //        obj.PrimerControl2019ADHERENCIAALPROGRAMA = item[154];
        //        obj.PrimerControl2019_SIRESPUESTAESNODESCRIBALACAUSA = item[155];
        //        obj.SegundoControl2019_Nombredelmedicodecabecera = item[156];
        //        obj.SegundoControl2019_FechadelControl2019_ = item[157];
        //        obj.SegundoControl2019_EdadMomentodelaconsulta = item[158];
        //        obj.SegundoControl2019_Peso = item[159];
        //        obj.SegundoControl2019_Talla = item[160];
        //        obj.SegundoControl2019IMC = item[161];
        //        obj.SegundoControl2019_TASistolica = item[162];
        //        obj.SegundoControl2019_TADiastólica = item[163];
        //        obj.SegundoControl2019_deTA1 = item[164];
        //        obj.SegundoControl2019_Cintura = item[165];
        //        obj.SegundoControl2019_Cadera = item[166];
        //        obj.SegundoControl2019_IndiceCintura_cadera = item[167];
        //        obj.SegundoControl2019OBESIDADABDOMINAL = item[168];
        //        obj.SegundoControl2019ClasificaciónRiesgoCV = item[169];
        //        obj.SegundoControl2019Valoraciónriesgodiabetes = item[170];
        //        obj.SegundoControl2019_UsuarioenProgramadeAtenciónDomicilia = item[171];
        //        obj.Segundocontol_usuariodelProgramadeAcondicionamientoFisi = item[172];
        //        obj.SegundoControl2019_TFGCrokoft = item[173];
        //        obj.SegundoControl2019_CKD_EPI = item[174];
        //        obj.SegundoControl2019_EstadioERC = item[175];
        //        obj.SegundoControl2019Tratamiento = item[176];
        //        obj.SegundoControl2019CONDUCTA = item[177];
        //        obj.SegundoControl2019ADHERENCIAALPROGRAMA = item[178];
        //        obj.SegundoControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item[179];
        //        obj.TercerControl2019_Nombredelmedicodecabecera = item[180];
        //        obj.TercerControl2019_FechadelControl2019_ = item[181];
        //        obj.TercerControl2019_EdadMomentodelaconsulta = item[182];
        //        obj.TercerControl2019_Peso = item[183];
        //        obj.TercerControl2019_Talla = item[184];
        //        obj.TercerControl2019_IMC = item[185];
        //        obj.TercerControl2019_TASistolica = item[186];
        //        obj.TercerControl2019_TADiastólica = item[187];
        //        obj.TercerControl2019Control2019deTA = item[188];
        //        obj.TercerControl2019_Cintura = item[189];
        //        obj.TercerControl2019_Cadera = item[190];
        //        obj.TercerControl2019_IndiceCintura_cadera = item[191];
        //        obj.TercerControl2019_OBESIDADABDOMINAL = item[192];
        //        obj.TercerControl2019ClasificaciónRiesgoCV = item[193];
        //        obj.TercerControl2019Valoraciónriesgodiabetes = item[194];
        //        obj.TercerControl2019_UsuarioenProgramadeAtenciónDomiciliar = item[195];
        //        obj.Tercercontrol_usuariodelProgramadeAcondicionamientoFisi = item[196];
        //        obj.TercerControl2019_TFGCROKOFT = item[197];
        //        obj.TercerControl2019_TFGCKD_EPI = item[198];
        //        obj.TercerControl2019_EstadioERC = item[199];
        //        obj.TercerControl2019Tratamiento = item[200];
        //        obj.TercerControl2019CONDUCTA = item[201];
        //        obj.TercerControl2019ADHERENCIAALPROGRAMA = item[202];
        //        obj.TercerControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item[203];
        //        obj.CuartoControl2019_Nombredelmedicodecabecera = item[204];
        //        obj.CuartoControl2019_FechadelControl2019_ = item[205];
        //        obj.CuartoControl2019_EdadMomentodelaconsulta = item[206];
        //        obj.CuartoControl2019_Peso = item[207];
        //        obj.CuartoControl2019_Talla = item[208];
        //        obj.CuartoControl2019_IMC = item[209];
        //        obj.CuartoControl2019_TASistolica = item[210];
        //        obj.CuartoControl2019_TADiastólica = item[211];
        //        obj.CuartoControl2019deTA = item[212];
        //        obj.CuartoControl2019_Cintura = item[213];
        //        obj.CuartoControl2019_Cadera = item[214];
        //        obj.CuartoControl2019_IndiceCintura_cadera = item[215];
        //        obj.CuartoControl2019OBESIDADABDOMINAL = item[216];
        //        obj.CuartoControl2019ClasificaciónRiesgoCV = item[217];
        //        obj.CuartoControl2019Valoraciónriesgodiabetes = item[218];
        //        obj.CuartoControl2019_UsuarioenProgramadeAtenciónDomiciliar = item[219];
        //        obj.Cuartocontol_UsuariodelProgramadeAcondicionamientoFisico = item[220];
        //        obj.CuartoControl2019_TFGCrokoft = item[221];
        //        obj.CuartoControl2019_TFGCDF_EPI = item[222];
        //        obj.CuartoControl2019_EstadioERC = item[223];
        //        obj.CuartoControl2019Tratamiento = item[224];
        //        obj.CuartoControl2019CONDUCTA = item[225];
        //        obj.CuartoControl2019ADHERENCIAALPROGRAMA = item[226];
        //        obj.CuartoControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item[227];
        //        obj.QuintoControl2019_Nombredelmedicodecabecera = item[228];
        //        obj.QuintoControl2019_FechadelControl2019_ = item[229];
        //        obj.QuintoControl2019_EdadMomentodelaconsulta = item[230];
        //        obj.QuintoControl2019_Peso = item[231];
        //        obj.QuintoControl2019_Talla = item[232];
        //        obj.QuintoControl2019_IMC = item[233];
        //        obj.QuintoControl2019_TASistolica = item[234];
        //        obj.QuintoControl2019_TADiastólica = item[235];
        //        obj.QuintoControl2019_Control2019deTA = item[236];
        //        obj.QuintoControl2019_Cintura = item[237];
        //        obj.QuintoControl2019_Cadera = item[238];
        //        obj.QuintoControl2019_IndiceCintura_cadera = item[239];
        //        obj.QuintoControl2019OBESIDADABDOMINAL = item[240];
        //        obj.QuintoControl2019ClasificaciónRiesgoCV = item[241];
        //        obj.QuintoControl2019Valoraciónriesgodiabetes = item[242];
        //        obj.QuintoControl2019_UsuarioenProgramadeAtenciónDomiciliar = item[243];
        //        obj.Quintocontol_usuariodelProgramadeAcondicionamientoFisic = item[244];
        //        obj.QuintoControl2019_TFGCrokoft = item[245];
        //        obj.QuintoControl2019_TFGCDK_EPI = item[246];
        //        obj.QuintoControl2019_EstadioERC = item[247];
        //        obj.QuintoControl2019_Tratamiento = item[248];
        //        obj.QuintoControl2019_Conducta = item[249];
        //        obj.QuintoControl2019_ADHERENCIAALPROGRAMA = item[250];
        //        obj.QuintoControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item[251];
        //        obj.SextoControl2019_Nombredelmedicodecabecera = item[252];
        //        obj.SextoControl2019_FechadelControl2019_ = item[253];
        //        obj.SextoControl2019_EdadMomentodelaconsulta = item[254];
        //        obj.SextoControl2019_Peso = item2[0];
        //        obj.SextoControl2019_Talla = item2[1];
        //        obj.SextoControl2019_IMC = item2[2];
        //        obj.SextoControl2019_TASistolica = item2[3];
        //        obj.SextoControl2019_TADiastólica = item2[4];
        //        obj.SextoControl2019_Control2019deTA = item2[5];
        //        obj.SextoControl2019_Cintura = item2[6];
        //        obj.SextoControl2019_Cadera = item2[7];
        //        obj.SextoControl2019_IndiceCintura_cadera = item2[8];
        //        obj.SextoControl2019_OBESIDADABDOMINAL = item2[9];
        //        obj.SextoControl2019ClasificaciónRiesgoCV = item2[10];
        //        obj.SextoControl2019_Valoraciónriesgodiabetes = item2[11];
        //        obj.SextoControl2019_UsuarioenProgramadeAtenciónDomiciliari = item2[12];
        //        obj.Sextocontol_usuariodelProgramadeAcondicionamientoFisico = item2[13];
        //        obj.SextoControl2019_TFGCrokoft = item2[14];
        //        obj.SextoControl2019_TFGCDK_EPI = item2[15];
        //        obj.SextoControl2019EstadioERC = item2[16];
        //        obj.SextoControl2019Tratamiento = item2[17];
        //        obj.SextoControl2019CONDUCTA = item2[18];
        //        obj.SextoControl2019_ADHERENCIAALPROGRAMA = item2[19];
        //        obj.SextoControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item2[20];
        //        obj.SéptimoControl2019Nombredelmedicodecabecera = item2[21];
        //        obj.SéptimoControl2019_FechadelControl2019_ = item2[22];
        //        obj.SéptimoControl2019_EdadMomentodelaconsulta = item2[23];
        //        obj.SéptimoControl2019_Peso = item2[24];
        //        obj.SéptimoControl2019_Talla = item2[25];
        //        obj.SéptimoControl2019_IMC = item2[26];
        //        obj.SéptimoControl2019_TASistolica = item2[27];
        //        obj.SéptimoControl2019_TADiastólica = item2[28];
        //        obj.SéptimoControl2019_Control2019deTA = item2[29];
        //        obj.SéptimoControl2019_Cintura = item2[30];
        //        obj.SéptimoControl2019_Cadera = item2[31];
        //        obj.SéptimoControl2019_IndiceCintura_cadera = item2[32];
        //        obj.SéptimoControl2019OBESIDADABDOMINAL = item2[33];
        //        obj.SéptimoControl2019ClasificaciónRiesgoCV = item2[34];
        //        obj.SéptimoControl2019Valoraciónriesgodiabetes = item2[35];
        //        obj.SéptimoControl2019_UsuarioenProgramadeAtenciónDomicilia = item2[36];
        //        obj.Séptimocontol_usuariodelProgramadeAcondicionamientoFisi = item2[37];
        //        obj.SéptimoControl2019_TFGCrokoft = item2[38];
        //        obj.SéptimoControl2019_TFGCDK_EPI = item2[39];
        //        obj.SéptimoControl2019_EstadioERC = item2[40];
        //        obj.SéptimoControl2019_TRATAMIENTO = item2[41];
        //        obj.SéptimoControl2019_CONDUCTA = item2[42];
        //        obj.SéptimoControl2019ADHERENCIAALPROGRAMA = item2[43];
        //        obj.SéptimoControl2019_SIRESPUESTAESNoDESCRIBALACAUSA = item2[44];
        //        obj.PrimecontrolEnfermerianombredelprofesional = item2[45];
        //        obj.FechaPrimerControlEnfermería = item2[46];
        //        obj.PrimerControlEnfermería2019_Peso = item2[47];
        //        obj.PrimerControlEnfermería2019_Talla = item2[48];
        //        obj.PrimerControlEnfermería2019_IMC = item2[49];
        //        obj.PrimerControlEnfermería2019_TASistolica = item2[50];
        //        obj.PrimerControlEnfermería2019_TADiastólica = item2[51];
        //        obj.PrimerControlEnfermería2019_Control2019deTA = item2[52];
        //        obj.PrimerControlEnfermería2019_Cintura = item2[53];
        //        obj.PrimerControlEnfermería2019_Cadera = item2[54];
        //        obj.PrimerControlEnfermería2019_IndiceCintura_cadera = item2[55];
        //        obj.PrimerControlEnfermería2019_OBESIDADABDOMINAL = item2[56];
        //        obj.PrimerControlEnfermería2019ClasificaciónRiesgoCV = item2[57];
        //        obj.PrimerControlEnfermería2019_Valoraciónriesgodiabetes = item2[58];
        //        obj.PrimerControlEnfermería2019_UsuarioenProgramadeAtenció = item2[59];
        //        obj.PrimerControlEnfermería2019_usuariodelProgramadeAcond = item2[60];
        //        obj.ALCOHOL_ControlEnfermería = item2[61];
        //        obj.SEDENTARISMOControlEnfermería = item2[62];
        //        obj.DIETA_ControlEnfermería = item2[63];
        //        obj.SOBREPESO_OBESIDAD_ControlEnfermería = item2[64];
        //        obj.OBESIDADABDOMINAL_ControlEnfermería = item2[65];
        //        obj.ESTRÉS_CONDICIONESLABORALES_ControlEnfermería = item2[66];
        //        obj.CuentaconPlanEstructuradodeAtención = item2[67];
        //        obj.PrimerControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[68];
        //        obj.PrimerControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUSA = item2[69];
        //        obj.SegunocontrolEnfermerianombredelprofesional = item2[70];
        //        obj.FechaSegundoControlEnfermería = item2[71];
        //        obj.SegundoControlEnfermería2019_Peso = item2[72];
        //        obj.SegundoControlEnfermería2019_Talla = item2[73];
        //        obj.SegundoControlEnfermería2019_IMC = item2[74];
        //        obj.SegundoControlEnfermería2019_TASistolica = item2[75];
        //        obj.SegundoControlEnfermería2019_TADiastólica = item2[76];
        //        obj.SegundoControlEnfermería2019_Control2019deTA = item2[77];
        //        obj.SegundoControlEnfermería2019_Cintura = item2[78];
        //        obj.SegundoControlEnfermería2019_Cadera = item2[79];
        //        obj.SegundoControlEnfermería2019_IndiceCintura_cadera = item2[80];
        //        obj.SegundoControlEnfermería2019_OBESIDADABDOMINAL = item2[81];
        //        obj.SegundoControlEnfermería2019ClasificaciónRiesgoCV = item2[82];
        //        obj.SegundoControlEnfermería2019_Valoraciónriesgodiabetes = item2[83];
        //        obj.SegundoControlEnfermería2019_UsuarioenProgramadeAtenci = item2[84];
        //        obj.SegundoControlEnfermería2019_usuariodelProgramadeAcon = item2[85];
        //        obj.CuentaconPlanEstructuradodeAtención1 = item2[86];
        //        obj.SegundoControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[87];
        //        obj.SegundoControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUS = item2[88];
        //        obj.TercercontrolEnfermerianombredelprofesional = item2[89];
        //        obj.FechaTercerControlEnfermería = item2[90];
        //        obj.TercerControlEnfermería2019_Peso = item2[91];
        //        obj.TercerControlEnfermería2019_Talla = item2[92];
        //        obj.TercerControlEnfermería2019_IMC = item2[93];
        //        obj.TercerControlEnfermería2019_TASistolica = item2[94];
        //        obj.TercerControlEnfermería2019_TADiastólica = item2[95];
        //        obj.TercerControlEnfermería2019_Control2019deTA = item2[96];
        //        obj.TercerControlEnfermería2019_Cintura = item2[97];
        //        obj.TercerControlEnfermería2019_Cadera = item2[98];
        //        obj.TercerControlEnfermería2019_IndiceCintura_cadera = item2[99];
        //        obj.TercerControlEnfermería2019_OBESIDADABDOMINAL = item2[100];
        //        obj.TercerControlEnfermería2019ClasificaciónRiesgoCV = item2[101];
        //        obj.TercerControlEnfermería2019_Valoraciónriesgodiabetes = item2[102];
        //        obj.TercerControlEnfermería2019_UsuarioenProgramadeAtenció = item2[103];
        //        obj.TercerControlEnfermería2019_usuariodelProgramadeAcond = item2[104];
        //        obj.CuentaconPlanEstructuradodeAtención2 = item2[105];
        //        obj.TercerControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[106];
        //        obj.TercerControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUSA = item2[107];
        //        obj.CuartocontrolEnfermerianombredelprofesional = item2[108];
        //        obj.FechaCuartoControlEnfermería = item2[109];
        //        obj.CuartoControlEnfermería2019_Peso = item2[110];
        //        obj.CuartoControlEnfermería2019_Talla = item2[111];
        //        obj.CuartoControlEnfermería2019_IMC = item2[112];
        //        obj.CuartoControlEnfermería2019_TASistolica = item2[113];
        //        obj.CuartoControlEnfermería2019_TADiastólica = item2[114];
        //        obj.CuartoControlEnfermería2019_Control2019deTA = item2[115];
        //        obj.CuartoControlEnfermería2019_Cintura = item2[116];
        //        obj.CuartoControlEnfermería2019_Cadera = item2[117];
        //        obj.CuartoControlEnfermería2019_IndiceCintura_cadera = item2[118];
        //        obj.CuartoControlEnfermería2019_OBESIDADABDOMINAL = item2[119];
        //        obj.CuartoControlEnfermería2019ClasificaciónRiesgoCV = item2[120];
        //        obj.CuartoControlEnfermería2019_Valoraciónriesgodiabetes = item2[121];
        //        obj.CuartoControlEnfermería2019_UsuarioenProgramadeAtenció = item2[122];
        //        obj.CuartoControlEnfermería2019_usuariodelProgramadeAcond = item2[123];
        //        obj.CuentaconPlanEstructuradodeAtención3 = item2[124];
        //        obj.CuartoControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[125];
        //        obj.CuartoControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUSA = item2[126];
        //        obj.QuintocontrolEnfermerianombredelprofesional = item2[127];
        //        obj.FechaQuintoControlEnfermería = item2[128];
        //        obj.QuintoControlEnfermería2019_Peso = item2[129];
        //        obj.QuintoControlEnfermería2019_Talla = item2[130];
        //        obj.QuintoControlEnfermería2019_IMC = item2[131];
        //        obj.QuintoControlEnfermería2019_TASistolica = item2[132];
        //        obj.QuintoControlEnfermería2019_TADiastólica = item2[133];
        //        obj.QuintoControlEnfermería2019_Control2019deTA = item2[134];
        //        obj.QuintoControlEnfermería2019_Cintura = item2[135];
        //        obj.QuintoControlEnfermería2019_Cadera = item2[136];
        //        obj.QuintoControlEnfermería2019_IndiceCintura_cadera = item2[137];
        //        obj.QuintoControlEnfermería2019_OBESIDADABDOMINAL = item2[138];
        //        obj.QuintoControlEnfermería2019ClasificaciónRiesgoCV = item2[139];
        //        obj.QuintoControlEnfermería2019_Valoraciónriesgodiabetes = item2[140];
        //        obj.QuintoControlEnfermería2019_UsuarioenProgramadeAtenció = item2[141];
        //        obj.QuintoControlEnfermería2019_usuariodelProgramadeAcond = item2[142];
        //        obj.CuentaconPlanEstructuradodeAtención4 = item2[143];
        //        obj.QuintoControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[144];
        //        obj.QuintoControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUSA = item2[145];
        //        obj.SextocontrolEnfermerianombredelprofesional = item2[146];
        //        obj.FechaSextoControlEnfermería = item2[147];
        //        obj.SextoControlEnfermería2019_Peso = item2[148];
        //        obj.SextoControlEnfermería2019_Talla = item2[149];
        //        obj.SextoControlEnfermería2019_IMC = item2[150];
        //        obj.SextoControlEnfermería2019_TASistolica = item2[151];
        //        obj.SextoControlEnfermería2019_TADiastólica = item2[152];
        //        obj.SextoControlEnfermería2019_Control2019deTA = item2[153];
        //        obj.SextoControlEnfermería2019_Cintura = item2[154];
        //        obj.SextoControlEnfermería2019_Cadera = item2[155];
        //        obj.SextoControlEnfermería2019_IndiceCintura_cadera = item2[156];
        //        obj.SextoControlEnfermería2019_OBESIDADABDOMINAL = item2[157];
        //        obj.SextoControlEnfermería2019ClasificaciónRiesgoCVg = item2[158];
        //        obj.SextoControlEnfermería2019_Valoraciónriesgodiabetesn = item2[159];
        //        obj.SextoControlEnfermería2019_UsuarioenProgramadeAtención = item2[160];
        //        obj.SextoControlEnfermería2019_usuariodelProgramadeAcondi = item2[161];
        //        obj.CuentaconPlanEstructuradodeAtención5 = item2[162];
        //        obj.SextoControlEnfermería2019_ADHERENCIAALPROGRAMA = item2[163];
        //        obj.SextoControlEnfermería_SIRESPUESTAESNoDESCRIBALACAUSA = item2[164];
        //        obj.GlicemiaBasal1 = item2[165];
        //        obj.FecharealizaciónGlicemia1 = item2[166];
        //        obj.GlicemiaBasal2 = item2[167];
        //        obj.FecharealizaciónGlicemia2 = item2[168];
        //        obj.HemoglobinaGlicosilada1 = item2[169];
        //        obj.ControldelaDiabetes1 = item2[170];
        //        obj.FecharealizaciónHbA1C1 = item2[171];
        //        obj.HemoglobinaGlicosilada2 = item2[172];
        //        obj.ControldelaDiabetes2 = item2[173];
        //        obj.FecharealizaciónHbA1C2 = item2[174];
        //        obj.HemoglobinaGlicosilada3 = item2[175];
        //        obj.ControldelaDiabetes3 = item2[176];
        //        obj.FecharealizaciónHbA1C3 = item2[177];
        //        obj.HemoglobinaGlicosilada4 = item2[178];
        //        obj.ControldelaDiabetes4 = item2[179];
        //        obj.FecharealizaciónHbA1C4 = item2[180];
        //        obj.Hematrocrito1 = item2[181];
        //        obj.FecharealizaciónHto1 = item2[182];
        //        obj.Hematrocrito2 = item2[183];
        //        obj.FecharealizaciónHto2 = item2[184];
        //        obj.Hematrocrito3 = item2[185];
        //        obj.FecharealizaciónHto3 = item2[186];
        //        obj.Hematrocrito4 = item2[187];
        //        obj.FecharealizaciónHto4 = item2[188];
        //        obj.Hemoglobina1 = item2[189];
        //        obj.FecharealizaciónHb1 = item2[190];
        //        obj.Hemoglobina2 = item2[191];
        //        obj.FecharealizaciónHb2 = item2[192];
        //        obj.Hemoglobina3 = item2[193];
        //        obj.FecharealizaciónHb3 = item2[194];
        //        obj.Hemoglobina4 = item2[195];
        //        obj.FecharealizaciónHb4 = item2[196];
        //        obj.Creatininaensangre_1 = item2[197];
        //        obj.FecharealizaciónCreatinina1 = item2[198];
        //        obj.Creatininaensangre_2 = item2[199];
        //        obj.FecharealizaciónCreatinina2 = item2[200];
        //        obj.Creatininaensangre_3 = item2[201];
        //        obj.FecharealizaciónCreatinina3 = item2[202];
        //        obj.ColesterolTotal1 = item2[203];
        //        obj.FecharealizaciónColeterolTotal1 = item2[204];
        //        obj.ColesterolTotal2 = item2[205];
        //        obj.FecharealizaciónColeterolTotal2 = item2[206];
        //        obj.ColesterolTotal3 = item2[207];
        //        obj.FecharealizaciónColeterolTotal3 = item2[208];
        //        obj.ColesterolTotal4 = item2[209];
        //        obj.FecharealizaciónColeterolTotal4 = item2[210];
        //        obj.ColesterolHDL1 = item2[211];
        //        obj.FecharealizaciónHDL1 = item2[212];
        //        obj.ColesterolHDL2 = item2[213];
        //        obj.FecharealizaciónHDL2 = item2[214];
        //        obj.ColesterolHDL3 = item2[215];
        //        obj.FecharealizaciónHDL3 = item2[216];
        //        obj.ColesterolHDL4 = item2[217];
        //        obj.FecharealizaciónHDL4 = item2[218];
        //        obj.ColesterolLDL1 = item2[219];
        //        obj.FecharealizaciónLDL1 = item2[220];
        //        obj.ColesterolLDL2 = item2[221];
        //        obj.FecharealizaciónLDL2 = item2[222];
        //        obj.ColesterolLDL3 = item2[223];
        //        obj.FecharealizaciónLDL3 = item2[224];
        //        obj.ColesterolLDL4 = item2[225];
        //        obj.FecharealizaciónLDL4 = item2[226];
        //        obj.Trigliceridos1 = item2[227];
        //        obj.FecharealizaciónTrigliceridos1 = item2[228];
        //        obj.Trigliceridos2 = item2[229];
        //        obj.FecharealizaciónTrigliceridos2 = item2[230];
        //        obj.Trigliceridos3 = item2[231];
        //        obj.FecharealizaciónTrigliceridos3 = item2[232];
        //        obj.Trigliceridos4 = item2[233];
        //        obj.FecharealizaciónTrigliceridos4 = item2[234];
        //        obj.TSH = item2[235];
        //        obj.FecharealizaciónTSH = item2[236];
        //        obj.AcidoUrico = item2[237];
        //        obj.FecharealizaciónAcUrico = item2[238];
        //        obj.Sodio1 = item2[239];
        //        obj.FecharealizaciónNa = item2[240];
        //        obj.Sodio2 = item2[241];
        //        obj.FecharealizaciónNa1 = item2[242];
        //        obj.Potasio1 = item2[243];
        //        obj.FecharealizaciónK = item2[244];
        //        obj.Potasio2 = item2[245];
        //        obj.FecharealizaciónK1 = item2[246];
        //        obj.Calcio = item2[247];
        //        obj.FecharealizaciónCa = item2[248];
        //        obj.Microalbuminuria1 = item2[249];
        //        obj.FecharealizaciónMicroalbuminuria1 = item2[250];
        //        obj.Microalbuminuria2 = item2[251];
        //        obj.FecharealizaciónMicroalbuminuria2 = item2[252];
        //        obj.Creatinuria = item2[253];
        //        obj.FecharealizaciónCreatinuria = item2[254];
        //        obj.Paratohormona_2 = item3[0];
        //        obj.FecharealizaciónParatohormona = item3[1];
        //        obj.PacialdeOrina = item3[2];
        //        obj.FecharealizaciónPdO = item3[3];
        //        obj.InmunizacionesInfluenza = item3[4];
        //        obj.InmunizacionesFechadeaplicaciónInfluenza_ = item3[5];
        //        obj.Inmunizaciones_Neumococo = item3[6];
        //        obj.InmunizacionesVacunadeNeumococoAplicada = item3[7];
        //        obj.InmunizacionesFecha1dosisNeumococo_ = item3[8];
        //        obj.InmunizacionesFecha2dosisNeumococo_ = item3[9];
        //        obj.IntevenciónEquipoOdontólogo = item3[10];
        //        obj.IntevenciónEquipoOdontología_fechaatención1 = item3[11];
        //        obj.IntevenciónEquipoOdontología_fechaatención2 = item3[12];
        //        obj.IntevenciónEquipoOdontología_fechaatención3 = item3[13];
        //        obj.IntevenciónEquipoNutrición = item3[14];
        //        obj.IntevenciónEquipoNutrición_fechaatención1 = item3[15];
        //        obj.IntevenciónEquipoNutrición_fechaatención2 = item3[16];
        //        obj.IntevenciónEquipoNutrición_fechaatención3 = item3[17];
        //        obj.IntevenciónEquipoNutrición_fechaatención4 = item3[18];
        //        obj.IntevenciónEquipoNutrición_fechaatención5 = item3[19];
        //        obj.IntevenciónEquipoNutrición_fechaatención6 = item3[20];
        //        obj.IntevenciónEquipoPsicología = item3[21];
        //        obj.IntevenciónEquipoPsicología_fechaatención1 = item3[22];
        //        obj.IntevenciónEquipoPsicología_fechaatención2 = item3[23];
        //        obj.IntevenciónEquipoTrabajosocial = item3[24];
        //        obj.IntevenciónEquipoTrabajoSocial_fechaatención1 = item3[25];
        //        obj.IntevenciónEquipoTrabajoSocial_fechaatención2 = item3[26];
        //        obj.IntevenciónEquipoQuímicofarmacéutico = item3[27];
        //        obj.IntevenciónEquipoQuímicofarmacéutico_fechaatención1 = item3[28];
        //        obj.IntevenciónEquipoQuímicofarmacéutico_fechaatención2 = item3[29];
        //        obj.IntevenciónEquipoQuímicofarmacéutico_fechaatención3 = item3[30];
        //        obj.ValoraciónMedicinaEspecizalizadaMedicinaInterna = item3[31];
        //        obj.FechadelaatenciónMed_Interna1_ = item3[32];
        //        obj.FechadelaatenciónMed_Interna2_ = item3[33];
        //        obj.FechadelaatenciónMed_Interna3_ = item3[34];
        //        obj.FechadelaatenciónMed_Interna4_ = item3[35];
        //        obj.FechadelaatenciónMed_Interna5_ = item3[36];
        //        obj.FechadelaatenciónMed_Interna6_ = item3[37];
        //        obj.FechadelaatenciónMed_Interna7_ = item3[38];
        //        obj.FechadelaatenciónMed_Interna8_ = item3[39];
        //        obj.ValoraciónMedicinaEspecializadaEndocrinología_Diabetología1 = item3[40];
        //        obj.FechadelaatenciónEndocrino_ = item3[41];
        //        obj.ValoraciiónMedicinaEspecizalizadaEndocrinología_Diabetología = item3[42];
        //        obj.FechadelaatenciónEndocrino_1 = item3[43];
        //        obj.ValoraciiónMedicinaEspecizalizadaEndocrinología_Diabetologí1 = item3[44];
        //        obj.FechadelaatenciónEndocrino_2 = item3[45];
        //        obj.ValoraciiónMedicinaEspecizalizadaEndocrinología_Diabetologí2 = item3[46];
        //        obj.FechadelaatenciónEndocrino_3 = item3[47];
        //        obj.ValoraciiónMedicinaEspecizalizadaEndocrinología_Diabetologí3 = item3[48];
        //        obj.FechadelaatenciónEndocrino_4 = item3[49];
        //        obj.ValoraciiónMedicinaEspecizalizadaEndocrinología_Diabetologí4 = item3[50];
        //        obj.FechadelaatenciónEndocrino_5 = item3[51];
        //        obj.ValoraciónMedicinaEspecializada_Oftalmología = item3[52];
        //        obj.FechadelaatenciónOftalmología_ = item3[53];
        //        obj.ValoraciónMedicinaEspecializada_Nefrología1 = item3[54];
        //        obj.FechadelaatenciónNefrología_ = item3[55];
        //        obj.ValoraciónMedicinaEspecializada_Nefrología2 = item3[56];
        //        obj.FechadelaatenciónNefrología_1 = item3[57];
        //        obj.ValoraciónMedicinaEspecializada_Nefrología3 = item3[58];
        //        obj.FechadelaatenciónNefrología_2 = item3[59];
        //        obj.ValoraciónMedicinaEspecializadaPsiquiatría = item3[60];
        //        obj.FechadelaatenciónPsiquiatria_ = item3[61];
        //        obj.ParticipaciónenActividadesEducativas = item3[62];
        //        obj.Fechadeactividadeducativa1 = item3[63];
        //        obj.Asistencia1 = item3[64];
        //        obj.Fechadeactividadeducativa2 = item3[65];
        //        obj.Asistencia2 = item3[66];
        //        obj.FRCVSeguimientoTABACO = item3[67];
        //        obj.INDICEDEBrinkmanSeguimiento = item3[68];
        //        obj.FRCVSeguimientoTABACO_Semodificó = item3[69];
        //        obj.FRCVSeguimiento_ALCOHOL = item3[70];
        //        obj.FRCVSeguimiento_Alcohol_Semodificó = item3[71];
        //        obj.FRCVSeguimientoSEDENTARISMO = item3[72];
        //        obj.FRCVSeguimiento_Sedentarismo_Semodificó = item3[73];
        //        obj.FRCVSeguimientoDIETAINADECUADA = item3[74];
        //        obj.FRCVSeguimientoDietaInadecuada_Semodificó = item3[75];
        //        obj.FRCVSeguimientoSOBREPESO_OBESIDAD = item3[76];
        //        obj.FRCVSeguimientoSobrepeso_obesidad_Semodificó = item3[77];
        //        obj.FRCVSeguimientoOBESIDADABDOMINAL = item3[78];
        //        obj.FRCVSeguimientoObesidadAbdominal_Semodificó = item3[79];
        //        obj.FRCVSeguimientoESTRÉS_CONDICIÓNLABORAL = item3[80];
        //        obj.FRCVSeguimientoEstrés_CondiciónLaboral_Semodificó = item3[81];
        //        obj.FECHARETIRODELPROGRAMA_ = item3[82];
        //        obj.MOTIVODERETIRO = item3[83];
        //        obj.TRASLADOACOORDINACIÓN = item3[84];
        //        obj.TRASLADOAUNIS = item3[85];
        //        obj.FECHADETRASLADODECOORDINACIÓN = item3[86];
        //        obj.OBSERVACIONES = item3[87];
        //        result.Add(obj);
        //    }

        //    book.Dispose();
        //    System.IO.File.Delete(rutafichero);
        //    return result;
        //}


        public ActionResult CargueDatosGRPC()
        {
            ViewBag.mes = BusClass.meses();
            ViewBag.tipo = BusClass.ListaTiposVCohorte();

            return View();
        }

        public JsonResult GuardarDatosMasivos(HttpPostedFileBase archivo, int? mes, int? año, int? tipo)
        {
            var mensaje = "";
            var rta = 0;
            Models.Cohortes.Cohortes Modelo = new Models.Cohortes.Cohortes();

            try
            {
                if (archivo == null)
                {
                    throw new Exception("Archivo no valido");
                }
                if (archivo.ContentLength > 0)
                {
                    try
                    {
                        CellsHelper.CustomImplementationFactory = new MemoryStreamMemoryManager();
                        var asposeOptions = new Aspose.Cells.LoadOptions
                        {
                            MemorySetting = MemorySetting.MemoryPreference
                        };

                        Workbook wb = new Workbook(archivo.InputStream, asposeOptions);
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

                        cargue_vigilanciaCohortes_lote obj = new cargue_vigilanciaCohortes_lote();
                        obj.mes = mes;
                        obj.año = año;
                        obj.id_tipo = tipo;
                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        if (tipo != 4)
                        {
                            Modelo.InsertarGrcp(dataTable, obj, ref MsgRes);
                        }
                        else
                        {
                            Modelo.InsertarGrcpEPOC(dataTable, obj, ref MsgRes);
                        }

                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto)
                        {
                            mensaje = "LOS DATOS SE CARGARON CORRECTAMENTE.";
                            rta = 1;
                        }
                        else
                        {
                            mensaje = MsgRes.DescriptionResponse;
                        }
                    }
                    catch (Exception e)
                    {
                        mensaje = "ERROR AL INGRESAR LOS REGISTROS: " + e.Message;
                    }
                }
                else
                {
                    mensaje = "DEBE SELECCIONAR UN ARCHIVO PARA REALIZAR EL CARGUE";
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
            }

            return Json(new { mensaje = mensaje, rta = rta });
        }

        public string ObtenerUnis(string regional)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_vigilancoaCohortes_busquedaLocalidadesResult> Unis = BusClass.GRPCBusquedaLocalidades(regional);
            foreach (var item in Unis)
            {
                result += "<option value='" + item.localidad_prestacion + "'>" + item.localidad_prestacion + "</option>";
            }

            return result;
        }

        public string ObtenerCiudades(string localidad)
        {
            string result = "<option value=''>- Seleccionar -</option>";

            List<management_vigilancoaCohortes_busquedaCiudadesResult> Ciudades = BusClass.GRPCBusquedaCiudades(localidad);
            foreach (var item in Ciudades)
            {
                result += "<option value='" + item.ciudad_prestacion + "'>" + item.ciudad_prestacion + "</option>";
            }

            return result;
        }

        public ActionResult TableroControlDatosGRPC(int? mes, int? año, string regional, string localidad, string municipio, int? tipo)
        {
            List<management_VigilanciaCohortes_TableroInicialResult> listado = new List<management_VigilanciaCohortes_TableroInicialResult>();
            List<management_VigilanciaCohortes_TableroInicialResult> listadoGES = new List<management_VigilanciaCohortes_TableroInicialResult>();
            List<management_VigilanciaCohortes_TableroInicialResult> listadoRCV = new List<management_VigilanciaCohortes_TableroInicialResult>();
            List<management_VigilanciaCohortes_TableroInicialResult> listadoSM = new List<management_VigilanciaCohortes_TableroInicialResult>();
            List<management_VigilanciaCohortes_TableroInicialResult> listadoEPOC = new List<management_VigilanciaCohortes_TableroInicialResult>();

            try
            {

                if ((mes != null && mes != 0) || (año != null && año != 0) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(localidad) || !string.IsNullOrEmpty(localidad) || (tipo != null && tipo != 0))
                {
                    listado = BusClass.ListadoVigilanciaCohortesInicial(mes, año, regional, localidad, municipio, tipo);
                }

                if (listado.Count() > 0)
                {
                    listadoGES = listado.Where(x => x.id_tipo == 1).ToList();
                    listadoRCV = listado.Where(x => x.id_tipo == 2).ToList();
                    listadoSM = listado.Where(x => x.id_tipo == 3).ToList();
                    listadoEPOC = listado.Where(x => x.id_tipo == 4).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);
            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.tipo = BusClass.ListaTiposVCohorte();
            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.rol = SesionVar.ROL;

            ViewBag.listadoGes = listadoGES;
            ViewBag.listadoRCV = listadoRCV;
            ViewBag.listadoSM = listadoSM;
            ViewBag.listadoEPOC = listadoEPOC;

            ViewBag.conteoGes = listadoGES.Count();
            ViewBag.conteoRCV = listadoRCV.Count();
            ViewBag.conteoSM = listadoSM.Count();
            ViewBag.conteoEPOC = listadoEPOC.Count();

            Session["ListadoGRCP"] = listado;

            return View();
        }

        public PartialViewResult ModalGestionGRCP(int? idLote, int? idRegistro)
        {
            management_VigilanciaCohortes_detalleParaGestionarResult dato = BusClass.DetallesVigilanciaCohortes(idLote, idRegistro);
            ViewBag.idLote = idLote;
            ViewBag.idRegistro = idRegistro;

            return PartialView(dato);
        }

        public JsonResult GuardarGestion(int? idLote, int? idRegistro, string gestion, DateTime? fecha_confirmacion, string observacion_confirmacion,
            string observacion_descarte)
        {
            var mensaje = "";
            var rta = 0;
            try
            {
                cargue_vigilanciaCohortes_registros_gestion obj = new cargue_vigilanciaCohortes_registros_gestion();
                obj.id_lote = idLote;
                obj.id_registro = idRegistro;
                obj.gestion = gestion;
                if (gestion == "Confirmado")
                {
                    obj.fecha_confirmacion = fecha_confirmacion;
                    obj.observacion_confirmacion = observacion_confirmacion;
                }
                else
                {
                    obj.fecha_descarte = DateTime.Now;
                    obj.observacion_descarte = observacion_descarte;
                }
                obj.fecha_digita = DateTime.Now;
                obj.usuario_digita = SesionVar.UserName;


                var inserta = BusClass.InsertarGestionVigiCohorte(obj);

                if (inserta != 0)
                {
                    mensaje = "GESTIÓN REALIZADA CORRECTAMENTE";
                    rta = 1;
                }
                else
                {
                    throw new Exception("NO SE PUDO REALIZAR LA GESTIÓN");
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                mensaje = "ERROR: " + error;
                rta = 0;
            }

            return Json(new { rta = rta, mensaje = mensaje });
        }

        public ActionResult TableroControlDatosGRPCRealizados(int? mes, int? año, string regional, string localidad, string municipio, int? tipo)
        {
            List<management_VigilanciaCohortes_TableroGestionadosResult> listado = new List<management_VigilanciaCohortes_TableroGestionadosResult>();
            List<management_VigilanciaCohortes_TableroGestionadosResult> listadoCon = new List<management_VigilanciaCohortes_TableroGestionadosResult>();
            List<management_VigilanciaCohortes_TableroGestionadosResult> listadoDes = new List<management_VigilanciaCohortes_TableroGestionadosResult>();
            List<management_VigilanciaCohortes_TableroGestionadosResult> listadoSin = new List<management_VigilanciaCohortes_TableroGestionadosResult>();

            try
            {
                if ((mes != null && mes != 0) || (año != null && año != 0) || !string.IsNullOrEmpty(regional) || !string.IsNullOrEmpty(localidad) || !string.IsNullOrEmpty(localidad) || (tipo != null && tipo != 0))
                {
                    listado = BusClass.ListadoVigilanciaCohortesGestionados(mes, año, regional, localidad, municipio, tipo);
                }

                if (listado.Count() > 0)
                {
                    listadoCon = listado.Where(x => x.gestion == "Confirmado").ToList();
                    listadoDes = listado.Where(x => x.gestion == "Descartado").ToList();
                    listadoSin = listado.Where(x => x.gestion == null).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            List<int> años = new List<int>();
            años.Add(DateTime.Now.Year - 1);
            años.Add(DateTime.Now.Year);
            años.Add(DateTime.Now.Year + 1);
            ViewBag.años = años;
            ViewBag.meses = BusClass.meses();
            ViewBag.regional = BusClass.GetRefRegion();
            ViewBag.tipo = BusClass.ListaTiposVCohorte();
            ViewBag.idUsuario = SesionVar.IDUser;
            ViewBag.rol = SesionVar.ROL;


            ViewBag.listadoCon = listadoCon;
            ViewBag.listadoDes = listadoDes;
            ViewBag.listadoSin = listadoSin;

            ViewBag.conteoCon = listadoCon.Count();
            ViewBag.conteoDes = listadoDes.Count();
            ViewBag.conteoSin = listadoSin.Count();

            Session["ListadoGRCPrelizadas"] = listado;

            return View();
        }

        public PartialViewResult ModalGestionRealizada(int? idLote, int? idRegistro)
        {
            management_VigilanciaCohortes_gestionesResult dato = BusClass.TraerGestionVigilanciaCohortes(idLote, idRegistro);
            management_VigilanciaCohortes_gestionesResult total = dato != null ? dato : new management_VigilanciaCohortes_gestionesResult();


            ViewBag.dato = dato;
            ViewBag.idLote = idLote;
            ViewBag.idRegistro = idRegistro;

            return PartialView(total);
        }

        public void ExcelGRCP()
        {
            List<management_VigilanciaCohortes_TableroInicialResult> listareporte = new List<management_VigilanciaCohortes_TableroInicialResult>();
            try
            {
                listareporte = (List<management_VigilanciaCohortes_TableroInicialResult>)Session["ListadoGRCP"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoGRCP");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AD1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AD1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AD1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AD1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AD1"].Style.Font.Name = "Century Gothic";

                //Sheet.Cells["A1"].Value = "LOTE";
                //Sheet.Cells["B1"].Value = "REGISTRO";
                Sheet.Cells["A1"].Value = "ID";
                Sheet.Cells["B1"].Value = "DOCUMENTO PACIENTE";
                Sheet.Cells["C1"].Value = "NOMBRE PACIENTE";
                Sheet.Cells["D1"].Value = "FECHA DE NACIMIENTO";
                Sheet.Cells["E1"].Value = "EDAD PACIENTE";
                Sheet.Cells["F1"].Value = "CIUDAD PRESTACIÓN";
                Sheet.Cells["G1"].Value = "LOCALIDAD PRESTACIÓN";
                Sheet.Cells["H1"].Value = "REGIONAL PRESTACION";
                Sheet.Cells["I1"].Value = "NOMBRE PRESTADOR";
                Sheet.Cells["J1"].Value = "ESPECIALIDAD";
                Sheet.Cells["K1"].Value = "FECHA DE PRESTACIÓN";
                Sheet.Cells["L1"].Value = "CODIGO DX PRINCIPAL";
                Sheet.Cells["M1"].Value = "TIPO DE DIAGNOSTICO";
                Sheet.Cells["N1"].Value = "CODIGO DX RELACIONADO 1";
                Sheet.Cells["O1"].Value = "CODIGO DX RELACIONADO 2";
                Sheet.Cells["P1"].Value = "CODIGO DX RELACIONADO 3";
                Sheet.Cells["Q1"].Value = "DESCRIPCIÓN CODIGO DX PRINCIPAL";
                Sheet.Cells["R1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 1";
                Sheet.Cells["S1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 2";
                Sheet.Cells["T1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 3";
                Sheet.Cells["U1"].Value = "POSICIÓN DIAGNOSTICO DETECTADO";
                Sheet.Cells["V1"].Value = "CODIGO DX DETECTADO";
                Sheet.Cells["W1"].Value = "DESCRIPCION DX DETECTADO";
                Sheet.Cells["X1"].Value = "CODIGO DE LA CONSULTA";
                Sheet.Cells["Y1"].Value = "DESCRIPCION DE LA CONSULTA";
                Sheet.Cells["Z1"].Value = "PROGRAMA POTENCIAL";
                Sheet.Cells["AA1"].Value = "FUENTE";
                Sheet.Cells["AB1"].Value = "ACCIÓN";
                Sheet.Cells["AC1"].Value = "LOTE";
                Sheet.Cells["AD1"].Value = "FECHA CARGUE";

                //Sheet.Cells["AE1"].Value = "TIPO";
                //Sheet.Cells["AF1"].Value = "MES";
                //Sheet.Cells["AG1"].Value = "AÑO";

                int row = 2;
                foreach (management_VigilanciaCohortes_TableroInicialResult item in listareporte)
                {

                    //Sheet.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    //Sheet.Cells[string.Format("B{0}", row)].Value = item.id_registro;
                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.documento_paciente;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.nombre_paciente;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_nacimiento;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.edad_paciente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.ciudad_prestacion;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.localidad_prestacion;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.regional_prestacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_prestador;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.especialidad;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_prestacion;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.codigo_dx_principal;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.tipo_diagnostico;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.codigo_dx_relacionado_1;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.codigo_dx_relacionado_2;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.codigo_dx_relacionado_3;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.descripcion_codigo_dx_principal;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcion_codigo_dx_relacionado_1;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.descripcion_codigo_dx_relacionado_2;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.descripcion_codigo_dx_relacionado_3;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.posicion_diagnostico_detectado;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.codigo_dx_detectado;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.descripcion_dx_detectado;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.codigo_de_consulta;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.descripcion_consulta;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.programa_potencial;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.fuente;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.accion;
                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.id_lote;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.fecha_digita;

                    //Sheet.Cells[string.Format("AE{0}", row)].Value = item.descripcionTipo;
                    //Sheet.Cells[string.Format("AF{0}", row)].Value = item.mes;
                    //Sheet.Cells[string.Format("AG{0}", row)].Value = item.año;
                    row++;
                }

                Sheet.Cells[string.Format("F{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("M{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("AD{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                Sheet.Cells["A" + row + ":AD1" + row].Style.Font.Name = "Century Gothic";

                Sheet.Cells["A:AD"].AutoFitColumns();

                var nombreArchivo = "ReporteGRCP_" + DateTime.Now.ToString("dd_MM_yyyy");
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

        public void ExcelGRCPGestionado()
        {
            List<management_VigilanciaCohortes_TableroGestionadosResult> listareporte = new List<management_VigilanciaCohortes_TableroGestionadosResult>();
            try
            {
                listareporte = (List<management_VigilanciaCohortes_TableroGestionadosResult>)Session["ListadoGRCPrelizadas"];

                if (listareporte.Count() == 0)
                {
                    string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('SIN DATOS PARA MOSTRAR');" +
                   "</script> ";
                    Response.Write(rta);
                    Response.End();
                }

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ResultadoGRCPGestionados");

                Color colFromHex = Color.FromArgb(22, 54, 92);
                Sheet.Cells["A1:AO1"].Style.Font.Bold = true;
                Sheet.Cells["A1:AO1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet.Cells["A1:AO1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                Sheet.Cells["A1:AO1"].Style.Font.Color.SetColor(Color.White);
                Sheet.Cells["A1:AO1"].Style.Font.Name = "Century Gothic";

                //Sheet.Cells["A1"].Value = "LOTE";
                //Sheet.Cells["B1"].Value = "REGISTRO";

                Sheet.Cells["A1"].Value = "ID";
                Sheet.Cells["B1"].Value = "DOCUMENTO PACIENTE";
                Sheet.Cells["C1"].Value = "NOMBRE PACIENTE";
                Sheet.Cells["D1"].Value = "FECHA DE NACIMIENTO";
                Sheet.Cells["E1"].Value = "EDAD PACIENTE";
                Sheet.Cells["F1"].Value = "CIUDAD PRESTACIÓN";
                Sheet.Cells["G1"].Value = "LOCALIDAD PRESTACIÓN";
                Sheet.Cells["H1"].Value = "REGIONAL PRESTACION";
                Sheet.Cells["I1"].Value = "NOMBRE PRESTADOR";
                Sheet.Cells["J1"].Value = "ESPECIALIDAD";
                Sheet.Cells["K1"].Value = "FECHA DE PRESTACIÓN";
                Sheet.Cells["L1"].Value = "CODIGO DX PRINCIPAL";
                Sheet.Cells["M1"].Value = "TIPO DE DIAGNOSTICO";
                Sheet.Cells["N1"].Value = "CODIGO DX RELACIONADO 1";
                Sheet.Cells["O1"].Value = "CODIGO DX RELACIONADO 2";
                Sheet.Cells["P1"].Value = "CODIGO DX RELACIONADO 3";
                Sheet.Cells["Q1"].Value = "DESCRIPCIÓN CODIGO DX PRINCIPAL";
                Sheet.Cells["R1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 1";
                Sheet.Cells["S1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 2";
                Sheet.Cells["T1"].Value = "DESCRIPCIÓN CODIGO DX RELACIONADO 3";
                Sheet.Cells["U1"].Value = "POSICIÓN DIAGNOSTICO DETECTADO";
                Sheet.Cells["V1"].Value = "CODIGO DX DETECTADO";
                Sheet.Cells["W1"].Value = "DESCRIPCION DX DETECTADO";
                Sheet.Cells["X1"].Value = "CODIGO DE LA CONSULTA";
                Sheet.Cells["Y1"].Value = "DESCRIPCION DE LA CONSULTA";
                Sheet.Cells["Z1"].Value = "PROGRAMA POTENCIAL";
                Sheet.Cells["AA1"].Value = "FUENTE";
                Sheet.Cells["AB1"].Value = "ACCIÓN";

                Sheet.Cells["AC1"].Value = "TIPO";
                Sheet.Cells["AD1"].Value = "MES";
                Sheet.Cells["AE1"].Value = "AÑO";
                Sheet.Cells["AF1"].Value = "ID GESTIÓN";
                Sheet.Cells["AG1"].Value = "GESTIÓN";
                Sheet.Cells["AH1"].Value = "FECHA CONFIRMACIÓN";
                Sheet.Cells["AI1"].Value = "JUSTIFICACIÓN CONFIRMACIÓN";
                Sheet.Cells["AJ1"].Value = "FECHA DESCARTE";
                Sheet.Cells["AK1"].Value = "OBSERVACIÓN DESCARTE";
                Sheet.Cells["AL1"].Value = "FECHA GESTIONA";
                Sheet.Cells["AM1"].Value = "USUARIO GESTIONA";

                Sheet.Cells["AN1"].Value = "LOTE";
                Sheet.Cells["AO1"].Value = "FECHA CARGUE";



                int row = 2;
                foreach (management_VigilanciaCohortes_TableroGestionadosResult item in listareporte)
                {
                    //Sheet.Cells[string.Format("A{0}", row)].Value = item.id_lote;
                    //Sheet.Cells[string.Format("B{0}", row)].Value = item.id_registro;

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.id;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.documento_paciente;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.nombre_paciente;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.fecha_nacimiento;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.edad_paciente;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.ciudad_prestacion;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.localidad_prestacion;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.regional_prestacion;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.nombre_prestador;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.especialidad;
                    Sheet.Cells[string.Format("K{0}", row)].Value = item.fecha_prestacion;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.codigo_dx_principal;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.tipo_diagnostico;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.codigo_dx_relacionado_1;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.codigo_dx_relacionado_2;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.codigo_dx_relacionado_3;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.descripcion_codigo_dx_principal;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.descripcion_codigo_dx_relacionado_1;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.descripcion_codigo_dx_relacionado_2;
                    Sheet.Cells[string.Format("T{0}", row)].Value = item.descripcion_codigo_dx_relacionado_3;
                    Sheet.Cells[string.Format("U{0}", row)].Value = item.posicion_diagnostico_detectado;
                    Sheet.Cells[string.Format("V{0}", row)].Value = item.codigo_dx_detectado;
                    Sheet.Cells[string.Format("W{0}", row)].Value = item.descripcion_dx_detectado;
                    Sheet.Cells[string.Format("X{0}", row)].Value = item.codigo_de_consulta;
                    Sheet.Cells[string.Format("Y{0}", row)].Value = item.descripcion_consulta;
                    Sheet.Cells[string.Format("Z{0}", row)].Value = item.programa_potencial;
                    Sheet.Cells[string.Format("AA{0}", row)].Value = item.fuente;
                    Sheet.Cells[string.Format("AB{0}", row)].Value = item.accion;

                    Sheet.Cells[string.Format("AC{0}", row)].Value = item.descripcionTipo;
                    Sheet.Cells[string.Format("AD{0}", row)].Value = item.mes;
                    Sheet.Cells[string.Format("AE{0}", row)].Value = item.año;
                    Sheet.Cells[string.Format("AF{0}", row)].Value = item.id_gestion;
                    Sheet.Cells[string.Format("AG{0}", row)].Value = item.gestion;
                    Sheet.Cells[string.Format("AH{0}", row)].Value = item.fecha_confirmacion;
                    Sheet.Cells[string.Format("AI{0}", row)].Value = item.observacion_confirmacion;
                    Sheet.Cells[string.Format("AJ{0}", row)].Value = item.fecha_descarte;
                    Sheet.Cells[string.Format("AK{0}", row)].Value = item.observacion_descarte;
                    Sheet.Cells[string.Format("AL{0}", row)].Value = item.fechaGestionDigita;
                    Sheet.Cells[string.Format("AM{0}", row)].Value = item.nombreDigitaGestion;
                    Sheet.Cells[string.Format("AN{0}", row)].Value = item.id_lote;
                    Sheet.Cells[string.Format("AO{0}", row)].Value = item.fecha_digita;

                    row++;
                }

                Sheet.Cells[string.Format("D{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("K{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("AL{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                Sheet.Cells[string.Format("AO{0}", row)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                Sheet.Cells["A" + row + ":AO1" + row].Style.Font.Name = "Century Gothic";
                Sheet.Cells["A:AO"].AutoFitColumns();

                var nombreArchivo = "ReporteGRCPGestionados_" + DateTime.Now.ToString("dd_MM_yyyy");
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ".xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                string rta = "<script LANGUAGE='JavaScript'>" +
                   "window.alert('ERROR EN LA DESCARGA');" +
                   "</script> ";
                Response.Write(rta);
                Response.End();
            }
        }

    }
}