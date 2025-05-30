using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class InventarioContabilizadas
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
        #endregion

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo que inserta me forma masiva los registros leidos desde el excel de inventario facturas
        /// </summary>
        /// <param name="objbase"></param>
        /// <param name="dt"></param>
        /// <param name="MsgRes"></param>
        public void GuardarInventarioFacturasContabilizadas(inventario_facturas_contabilizadas_carguebase objbase, DataTable dt, ref MessageResponseOBJ MsgRes)
        {
            String columna = "";
            Int32 fila = 1;
            String msgError = "";

            Int32 id_cargue = BusClass.InsertarInventarioFacturasContabilizadasCargueBase(objbase, ref MsgRes);
            //Mensaje log cargues masivos

            var mensaje = "";
            var mensajeLog = "";
            log_cargues_masivos logMas = new log_cargues_masivos();
            logMas.id_cargue = id_cargue;
            logMas.fecha_Cargue = DateTime.Now;
            logMas.periodo_cargue = new DateTime(objbase.año, objbase.mes, 1);
            logMas.nombre_digita = SesionVar.NombreUsuario;
            logMas.usuario_digita = SesionVar.UserName;
            logMas.tipo_registro = "Cargue facturas contabilizadas archivo digital";

            try
            {
                if (id_cargue != 0)
                {
                    List<inventario_facturas_contabilizadas_carguedtll> result = new List<inventario_facturas_contabilizadas_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        inventario_facturas_contabilizadas_carguedtll obj = new inventario_facturas_contabilizadas_carguedtll();
                        fila = fila + 1;
                        string texto = "";


                        if (!string.IsNullOrEmpty(item["Fecha de contabilización"].ToString()))
                        {
                            obj.inventario_cargue_base_id = id_cargue;

                            columna = "Fecha de contabilización";
                            texto = Convert.ToString(item["Fecha de contabilización"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_contabilizacion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }

                            columna = "Documento contable";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Documento contable"]))) { obj.documento_contable = Convert.ToInt64(item["Documento contable"]); }

                            columna = "Número de Factura SAP";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Número de Factura SAP"])))
                            {
                                texto = Convert.ToString(item["Número de Factura SAP"]);
                                if (texto.Length <= 100)
                                {
                                    obj.numero_factura_sap = texto;
                                }
                                else
                                {
                                    msgError = "La longitud del campo no puede exceder los 100 caracteres.";
                                    break;
                                }

                            }

                            columna = "Código SAP";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Código SAP"]))) { obj.cod_sap = Convert.ToInt64(item["Código SAP"]); }

                            columna = "NIT";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["NIT"]))) { obj.nit = Convert.ToInt64(item["NIT"]); }

                            columna = "Razón Social";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Razón Social"])))
                            {

                                texto = Convert.ToString(item["Razón Social"]);
                                if (texto.Length <= 150)
                                {
                                    obj.razon_social = texto;
                                }
                                else
                                {
                                    msgError = "La longitud del campo no puede exceder los 150 caracteres.";
                                    break;
                                }
                            }

                            columna = "Ciudad";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Ciudad"])))
                            {

                                texto = Convert.ToString(item["Ciudad"]);
                                if (texto.Length <= 50)
                                {
                                    obj.ciudad = texto;
                                }
                                else
                                {
                                    msgError = "La longitud del campo no puede exceder los 50 caracteres.";
                                    break;
                                }

                            }

                            columna = "Valor Total Factura";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Valor Total Factura"]))) { obj.valor_total_factura = Convert.ToInt64(item["Valor Total Factura"]); }

                            columna = "Regional";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Regional"])))
                            {

                                texto = Convert.ToString(item["Regional"]);
                                if (texto.Length <= 50)
                                {
                                    obj.regional = texto;
                                }
                                else
                                {
                                    msgError = "La longitud del campo no puede exceder los 50 caracteres.";
                                    break;
                                }
                            }

                            columna = "Fecha de Recepción";
                            texto = Convert.ToString(item["Fecha de Recepción"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_recepcion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }

                            columna = "UNIS";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["UNIS"])))
                            {

                                texto = Convert.ToString(item["UNIS"]);
                                if (texto.Length <= 100)
                                {
                                    obj.unis = texto;
                                }
                                else
                                {
                                    msgError = "La longitud del campo no puede exceder los 50 caracteres.";
                                    break;
                                }
                            }

                            columna = "Mes_ID";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["Mes_ID"]))) { obj.mes_id = Convert.ToInt32(item["Mes_ID"]); }

                            columna = "id detalle sami";
                            if (!string.IsNullOrEmpty(Convert.ToString(item["id detalle sami"])))
                            {
                                obj.id_cargue_dtll = Convert.ToInt32(item["id detalle sami"]);
                            }

                            obj.id_estado = 1;

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Lo sentimos. La fecha de contabilizacion es escencial para realizar el cargue.";
                            mensaje = "Error en el cargue";

                            EliminarCargueBase(id_cargue);
                            break;
                        }
                    }

                    /*Me valida que se inserten todas las filas*/
                    if (result.Count == dt.Rows.Count)
                    {
                        List<int> idCargues = result.Select(l => l.id_cargue_dtll.Value).ToList();
                        var seen = new HashSet<int>();
                        var duplicados = idCargues.Where(x => !seen.Add(x));

                        if (!string.IsNullOrEmpty(String.Join(", ", duplicados)))
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Lo sentimos! No se ha realizado el cargue porque hay IDs de factura duplicados en el documento excel de cargue masivo.";
                            mensaje = "Error en el cargue";

                            EliminarCargueBase(id_cargue);
                        }
                        else
                        {
                            BusClass.InsertarInventarioFacturasContabilizadasCargueDtll(result, ref MsgRes);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Lo sentimos! No se ha realizado el cargue porque uno de los registros tuvo error: Error [Fila = '" + fila + "', Columna='" + columna + "', Error='" + msgError + "'  ]";
                        mensaje = "Error en el cargue";
                        EliminarCargueBase(id_cargue);
                    }

                    logMas.registros_Cargados = result.Count();
                    logMas.estado_cargue = mensaje;
                    var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);
                }
            }
            catch (Exception e)
            {
                string msgError2 = e.Message;

                if (e.Message.Contains("Input string was not in a correct format"))
                {
                    msgError2 = "La columna no tiene el formato correcto de ingreso.";
                }

                if (e.Message.Contains("The string was not recognized as a valid DateTime"))
                {
                    msgError2 = "El formato de la fecha no es valido.";
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la Fila='" + fila.ToString() + "', Columna: '" + columna + "'. Error: '" + msgError2 + "'";

                logMas.registros_Cargados = 0;
                logMas.estado_cargue = mensaje;
                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                EliminarCargueBase(id_cargue);
            }
        }

        public void EliminarCargueBase(int idCargueBase)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                var cargueBase = db.inventario_facturas_contabilizadas_carguebase.Where(l => l.id_invertario_cargue_base == idCargueBase).First();
                db.inventario_facturas_contabilizadas_carguebase.DeleteOnSubmit(cargueBase);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 25 de noviembre de 2022
        /// Metodo para consultar el estado de facturas medianto su estado
        /// </summary>
        /// <param name="idEstado"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadasResult> ConsultarInventarioFacturasPorEstado(int idEstado, DateTime? fechainicio, DateTime? fechafinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultarInventarioFacturasPorEstado(idEstado, fechainicio, fechafinal, regional, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 28 de noviembre de 2022
        /// Metodo utilizado para guardar la gestion hecha a una factura en el inventario
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void GuardarGestionInvetarioFacturaContabilizada(inventario_facturas_contabilizadas_gestion obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.GuardarGestionInvetarioFacturaContabilizada(obj, ref MsgRes);
        }

        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el estado de facturas 
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_cordinacionResult> ConsultarInventarioFacturasCordinacion(int mes, int año, int regional, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultarInventarioFacturasCordinacion(mes, año, regional, ref MsgRes);
        }

        /// <summary>
        ///Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar el inventario de facturas contabilizadas pero consolidado por mes, año y regional
        /// </summary>
        /// <returns></returns>
        public List<Management_inventario_facturas_contabilizadas_consolidadoResult> ConsultarInventarioFacturasConsolidado()
        {
            return BusClass.ConsultarInventarioFacturasConsolidado();
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 29 de noviembre de 2022
        /// Metodo para consultar la gestion realizada a una factura contabilizada que fue cargada a traves del id Detalle
        /// </summary>
        /// <param name="idDetalle"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_gestion consultarGestionFacturaContabilizadaporIdDetalle(int idDetalle)
        {
            return BusClass.consultarGestionFacturaContabilizadaporIdDetalle(idDetalle);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para porder insertar los datos del archivo cargado en el tablero de inventario facturas contabilizadas consolidado
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="MsgRes"></param>
        public void insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(inventario_facturas_contabilizadas_gestor_documental doc, ref MessageResponseOBJ MsgRes)
        {
            BusClass.insertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado(doc, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 30 de noviembre de 2022
        /// Metodo utilizado para consultar los archivos cargados por id
        /// </summary>
        /// <param name="idArchivo"></param>
        /// <param name=""></param>
        public inventario_facturas_contabilizadas_gestor_documental ConsultarRegistroArchivoCargadoPorId(int idArchivo, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultarRegistroArchivoCargadoPorId(idArchivo, ref MsgRes);
        }

        public List<inventario_facturas_contabilizadas_gestor_documental> ConsultarRegistroArchivoCargadoPorIdLista(int? mes, int? año, int? regional, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultarRegistroArchivoCargadoPorIdLista(mes, año, regional, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 02 de diciembre de 2022
        /// Metodo utilizado para validar la existencia de un periodo que vayana a cargar
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="año"></param>
        /// <param name="regional"></param>
        /// <returns></returns>
        public inventario_facturas_contabilizadas_carguebase ConsultarExistenciaPeriodoCargado(int mes, int año, int regional)
        {
            return BusClass.ConsultarExistenciaPeriodoCargado(mes, año, regional);
        }
    }
}