using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.Data;
using LinqToExcel;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class GastosxServicio
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

        #region Funciones

        public int InsertarGastosPorServicio(gasto_por_servicio_cargue_base obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarGastosPorServicio(obj, ref MsgRes);
        }

        public void InsertarGastosPorServicioDtll(List<gasto_por_Servicio_cargue_dtll> dtll, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarGastosPorServicioDtll(dtll, ref MsgRes);
        }

        public gasto_por_servicio_cargue_base getcarguebase(int mes, int año, string regional)
        {
            return BusClass.getcarguebase(mes, año, regional);
        }


        public void InsertarDetallesGatosPorServicio(DataTable dt, gasto_por_servicio_cargue_base objbase, ref MessageResponseOBJ MsgRes)
        {

            Int32 IntContadorFilas = 0;
            Int32 id_cargue = 0;
            Int32 fila = 1;
            string columna = "";
            string texto = "";
            string textError = "";
            List<gasto_por_Servicio_cargue_dtll> result = new List<gasto_por_Servicio_cargue_dtll>();
            id_cargue = BusClass.InsertarGastosPorServicio(objbase, ref MsgRes);
            var mensaje = "";

            log_cargues_masivos logMas = new log_cargues_masivos();
            logMas.id_cargue = id_cargue;

            logMas.fecha_Cargue = DateTime.Now;
            logMas.periodo_cargue = new DateTime((int)objbase.año, (int)objbase.mes, 1);
            logMas.nombre_digita = SesionVar.NombreUsuario;
            logMas.usuario_digita = SesionVar.UserName;
            logMas.tipo_registro = "Gasto por servicios";



            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    gasto_por_Servicio_cargue_dtll obj = new gasto_por_Servicio_cargue_dtll();
                    fila = fila + 1;
                    IntContadorFilas = IntContadorFilas + 1;

                    if (!string.IsNullOrEmpty(item["Periodo"].ToString()))
                    {
                        obj.cargue_base_id = id_cargue;

                        columna = "Periodo";
                        try
                        {
                            texto = Convert.ToString(item["Periodo"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.Periodo = Convert.ToDateTime(new DateTime((int)objbase.año, (int)objbase.mes, 1));
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo fecha.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "LocPrestacion";
                        obj.LocPrestacion = Convert.ToString(item["LocPrestacion"]);

                        columna = "Analista";
                        try
                        {
                            texto = Convert.ToString(item["Analista"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.Analista = Convert.ToInt32(item["Analista"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "FecFac";
                        try
                        {
                            texto = Convert.ToString(item["FecFac"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
                            {

                                obj.FecFac = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecFac = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "FecRec";
                        try
                        {
                            texto = Convert.ToString(item["FecRec"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
                            {

                                obj.FecRec = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecRec = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "FecAut";
                        try
                        {
                            texto = Convert.ToString(item["FecAut"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
                            {
                                obj.FecAut = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecAut = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);

                        }

                        columna = "FecCon";
                        try
                        {
                            texto = Convert.ToString(item["FecCon"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
                            {
                                obj.FecCon = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecCon = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Factura";
                        obj.Factura = Convert.ToString(item["Factura"]);

                        columna = "ConsecPers";
                        try
                        {
                            texto = Convert.ToString(item["ConsecPers"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.ConsecPers = Convert.ToInt64(item["ConsecPers"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "TipoIDPrestador";
                        obj.TipoIDPrestador = Convert.ToString(item["TipoIDPrestador"]);

                        columna = "IDPrestador";
                        try
                        {
                            texto = Convert.ToString(item["IDPrestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.IDPrestador = Convert.ToInt64(item["IDPrestador"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "CodHab";
                        obj.CodHab = Convert.ToString(item["CodHab"]);

                        columna = "CodSAP";
                        try
                        {
                            texto = Convert.ToString(item["CodSAP"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.CodSAP = Convert.ToInt64(item["CodSAP"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "Pedido";
                        obj.Pedido = Convert.ToString(item["Pedido"]);

                        columna = "Prestador";
                        obj.Prestador = Convert.ToString(item["Prestador"]);

                        columna = "ConsecFac";
                        try
                        {
                            texto = Convert.ToString(item["ConsecFac"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.ConsecFac = Convert.ToInt64(item["ConsecFac"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "ConsecBen";
                        try
                        {
                            texto = Convert.ToString(item["ConsecBen"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.ConsecBen = Convert.ToInt64(item["ConsecBen"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "TipoIDUsuario";
                        obj.TipoIDUsuario = Convert.ToString(item["TipoIDUsuario"]);

                        columna = "ID_Usuario";
                        try
                        {
                            texto = Convert.ToString(item["ID_Usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.IDUsuario = Convert.ToString(item["ID_Usuario"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "HisUsuario";
                        obj.HisUsuario = Convert.ToString(item["HisUsuario"]);

                        columna = "NombreUsuario";
                        obj.NombreUsuario = Convert.ToString(item["NombreUsuario"]);

                        columna = "FecNac";
                        try
                        {
                            texto = Convert.ToString(item["FecNac"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO") && !texto.ToUpper().Contains("#N/A")))
                            {

                                obj.FecNac = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecNac = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Sexo";
                        obj.Sexo = Convert.ToString(item["Sexo"]);

                        columna = "CodLocUsuario";
                        try
                        {
                            texto = Convert.ToString(item["CodLocUsuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.CodLocUsuario = Convert.ToInt64(item["CodLocUsuario"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "LocUsuario";
                        obj.LocUsuario = Convert.ToString(item["LocUsuario"]);

                        columna = "CECOS";
                        obj.CECOS = Convert.ToString(item["CECOS"]);

                        columna = "SerSum";
                        obj.SerSum = Convert.ToString(item["SerSum"]);

                        columna = "CodSerSum";
                        obj.CodSerSum = Convert.ToString(item["CodSerSum"]);

                        columna = "NomSerSum";
                        obj.NomSerSum = Convert.ToString(item["NomSerSum"]);

                        columna = "TIGA";
                        if (!string.IsNullOrEmpty(Convert.ToString(item["TIGA"])))
                        {
                            Int64 tiga = Convert.ToInt64(item["TIGA"]);
                            obj.TIGA = Convert.ToString(item["TIGA"]);
                        }

                        columna = "TipoSer";
                        try
                        {
                            texto = Convert.ToString(item["TipoSer"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.TipoSer = Convert.ToInt64(item["TipoSer"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "FecPre";
                        try
                        {
                            texto = Convert.ToString(item["FecPre"]);
                            if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
                            {
                                obj.FecPre = CambiarFormatoFecha(texto);
                            }
                            else
                            {
                                obj.FecPre = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "CantidadSerSum";
                        try
                        {
                            texto = Convert.ToString(item["CantidadSerSum"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.CantidadSerSum = Convert.ToInt64(item["CantidadSerSum"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "Valor";

                        try
                        {
                            texto = Convert.ToString(item["Valor"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.Valor = Convert.ToDecimal(item["Valor"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }

                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);

                        }

                        columna = "Dx";
                        obj.Dx = Convert.ToString(item["Dx"]);

                        columna = "DescripDx";
                        obj.DescripDx = Convert.ToString(item["DescripDx"]);

                        columna = "GrupoDx";
                        obj.GrupoDx = Convert.ToString(item["GrupoDx"]);

                        columna = "Edad";
                        try
                        {
                            texto = Convert.ToString(item["Edad"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.Edad = Convert.ToInt32(item["Edad"]);
                            }
                            else
                            {
                                obj.Edad = null;
                                //textError = columna + ", formato incorrecto, es campo númerico";
                                //throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Quinquenio";
                        obj.Quinquenio = Convert.ToString(item["Quinquenio"]);

                        columna = "TIGADetalle";
                        obj.TIGADetalle = Convert.ToString(item["TIGADetalle"]);

                        columna = "TIGAGSD";
                        try
                        {
                            texto = Convert.ToString(item["TIGAGSD"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.TIGAGSD = Convert.ToInt32(item["TIGAGSD"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "DescripTIGAGSD";
                        obj.DescripTIGAGSD = Convert.ToString(item["DescripTIGAGSD"]);

                        columna = "RegPrestacion";
                        obj.RegPrestacion = Convert.ToString(item["RegPrestacion"]);

                        columna = "DescripCECOS";
                        obj.DescripCECOS = Convert.ToString(item["DescripCECOS"]);

                        columna = "TipoSalud";
                        obj.TipoSalud = Convert.ToString(item["TipoSalud"]);

                        columna = "TipoPoblacion";
                        obj.TipoPoblacion = Convert.ToString(item["TipoPoblacion"]);

                        columna = "UnicoID";
                        try
                        {
                            texto = Convert.ToString(item["UnicoID"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.UnicoID = Convert.ToInt32(item["UnicoID"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }


                        columna = "UnicoFactura";
                        try
                        {
                            texto = Convert.ToString(item["UnicoFactura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.UnicoFactura = Convert.ToInt32(item["UnicoFactura"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Eventos";
                        try
                        {
                            texto = Convert.ToString(item["Eventos"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.Eventos = Convert.ToInt32(item["Eventos"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "RegReporte";
                        obj.RegReporte = Convert.ToString(item["RegReporte"]);

                        columna = "IDQuinquenio";
                        try
                        {
                            texto = Convert.ToString(item["IDQuinquenio"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.IDQuinquenio = Convert.ToInt32(item["IDQuinquenio"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto, es campo númerico";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "UNIS";
                        obj.UNIS = Convert.ToString(item["UNIS"]);

                        columna = "CausadaOtraReg";
                        obj.CausadaOtraReg = Convert.ToString(item["CausadaOtraReg"]);

                        columna = "Latitud";
                        obj.Latitud = Convert.ToString(item["Latitud"]);

                        columna = "Longitud";
                        obj.Longitud = Convert.ToString(item["Longitud"]);

                        result.Add(obj);
                        obj = new gasto_por_Servicio_cargue_dtll();
                    }
                }

                BusClass.InsertarGastosPorServicioDtll(result, ref MsgRes);
                logMas.registros_Cargados = result.Count();

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                {
                    BusClass.EliminarCargueGastoPorServicio(id_cargue);
                    mensaje = "Error en el cargue";
                }
                else
                {
                    mensaje = "Cargue completado";
                }

                logMas.estado_cargue = mensaje;

                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna - " + columna + " : " + ex.Message;
                MsgRes.CodeError = ex.Message;

                mensaje = "Cargue completado";

                var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                try
                {
                    BusClass.EliminarCargueGastoPorServicio(id_cargue);
                }
                catch
                {
                    MsgRes.DescriptionResponse = "Error eliminando el cargue base";
                    MsgRes.CodeError = ex.Message;
                }
            }
        }


        /// <summary>
        /// Autor: Alexis Quiñones Caastillo
        /// Fecha: 19 de julio de 2022
        /// Metodo que inserta la base y el detalle del cargue de gastos por servicio
        /// </summary>
        /// <param name="dt2"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        //public void InsertarDetallesGatosPorServicio(DataTable dt2, gasto_por_servicio_cargue_base carguebase, ref MessageResponseOBJ MsgRes)
        //{
        //    Int32 id_cargue = 0;
        //    Int32 IntContador = 1;
        //    Int32 IntContadorFilas = 0;

        //    gasto_por_Servicio_cargue_dtll obj = new gasto_por_Servicio_cargue_dtll();
        //    List<gasto_por_Servicio_cargue_dtll> OBJDetalle = new List<gasto_por_Servicio_cargue_dtll>();
        //    string columna = "";
        //    var textError = "";
        //    var resultado = 0;

        //    id_cargue = BusClass.InsertarGastosPorServicio(carguebase, ref MsgRes);

        //    try
        //    {
        //        foreach (DataRow item in dt2.Rows)
        //        {
        //            IntContadorFilas = IntContadorFilas + 1;

        //            if (item["Periodo"].ToString() != "")
        //            {

        //                var texto = "";
        //                var fecha = new DateTime();
        //                var entero = 0;
        //                var bigint = 0;
        //                var decimales = new decimal();

        //                obj.cargue_base_id = id_cargue;

        //                columna = "Periodo";
        //                try
        //                {
        //                    texto = Convert.ToString(item["Periodo"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {
        //                        obj.Periodo = Convert.ToDateTime(item["Periodo"]);
        //                    }
        //                    else
        //                    {
        //                        obj.Periodo = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.Periodo = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }


        //                columna = "LocPrestacion";
        //                texto = Convert.ToString(item["LocPrestacion"]);
        //                if (texto.Length <= 300)
        //                {
        //                    obj.LocPrestacion = Convert.ToString(item["LocPrestacion"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "Analista";
        //                try
        //                {
        //                    texto = Convert.ToString(item["Analista"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {

        //                        obj.Analista = Convert.ToInt32(item["Analista"]);
        //                    }
        //                    else
        //                    {
        //                        obj.Analista = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "FecFac";
        //                try
        //                {
        //                    texto = Convert.ToString(item["FecFac"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {

        //                        obj.FecFac = Convert.ToDateTime(item["FecFac"]);
        //                    }
        //                    else
        //                    {
        //                        obj.FecFac = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.FecFac = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }

        //                columna = "FecAut";
        //                try
        //                {
        //                    texto = Convert.ToString(item["FecAut"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {
        //                        obj.FecAut = Convert.ToDateTime(item["FecAut"]);
        //                    }
        //                    else
        //                    {
        //                        obj.FecAut = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.FecAut = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }

        //                columna = "FecCon";
        //                try
        //                {
        //                    texto = Convert.ToString(item["FecCon"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {
        //                        obj.FecCon = Convert.ToDateTime(item["FecCon"]);
        //                    }
        //                    else
        //                    {
        //                        obj.FecCon = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.FecCon = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }

        //                columna = "Factura";
        //                texto = Convert.ToString(item["Factura"]);
        //                if (texto.Length <= 300)
        //                {
        //                    obj.Factura = Convert.ToString(item["Factura"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "ConsecPers";
        //                try
        //                {
        //                    texto = Convert.ToString(item["ConsecPers"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.ConsecPers = Convert.ToInt64(item["ConsecPers"]);
        //                    }
        //                    else
        //                    {
        //                        obj.ConsecPers = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "TipoIDPrestador";
        //                texto = Convert.ToString(item["TipoIDPrestador"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TipoIDPrestador = Convert.ToString(item["TipoIDPrestador"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "IDPrestador";
        //                try
        //                {
        //                    texto = Convert.ToString(item["IDPrestador"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.IDPrestador = Convert.ToInt64(item["IDPrestador"]);
        //                    }
        //                    else
        //                    {
        //                        obj.IDPrestador = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "CodHab";
        //                texto = Convert.ToString(item["CodHab"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TipoIDPrestador = Convert.ToString(item["CodHab"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "CodSAP";
        //                try
        //                {
        //                    texto = Convert.ToString(item["CodSAP"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.CodSAP = Convert.ToInt64(item["CodSAP"]);
        //                    }
        //                    else
        //                    {
        //                        obj.CodSAP = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Pedido";
        //                texto = Convert.ToString(item["Pedido"]);
        //                if (texto.Length <= 300)
        //                {
        //                    obj.Pedido = Convert.ToString(item["Pedido"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Prestador";
        //                texto = Convert.ToString(item["Prestador"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.Prestador = Convert.ToString(item["Prestador"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "ConsecFac";
        //                try
        //                {
        //                    texto = Convert.ToString(item["ConsecFac"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.ConsecFac = Convert.ToInt64(item["ConsecFac"]);
        //                    }
        //                    else
        //                    {
        //                        obj.ConsecFac = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "ConsecBen";
        //                try
        //                {
        //                    texto = Convert.ToString(item["ConsecBen"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.ConsecBen = Convert.ToInt64(item["ConsecBen"]);
        //                    }
        //                    else
        //                    {
        //                        obj.ConsecBen = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "TipoIDUsuario";
        //                texto = Convert.ToString(item["TipoIDUsuario"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TipoIDUsuario = Convert.ToString(item["TipoIDUsuario"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "ID_Usuario";
        //                try
        //                {
        //                    texto = Convert.ToString(item["ID_Usuario"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.IDUsuario = Convert.ToInt64(item["ID_Usuario"]);
        //                    }
        //                    else
        //                    {
        //                        obj.IDUsuario = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "HisUsuario";
        //                texto = Convert.ToString(item["HisUsuario"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.HisUsuario = Convert.ToString(item["HisUsuario"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "NombreUsuario";
        //                texto = Convert.ToString(item["NombreUsuario"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.NombreUsuario = Convert.ToString(item["NombreUsuario"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "FecNac";
        //                try
        //                {
        //                    texto = Convert.ToString(item["FecNac"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {
        //                        obj.FecNac = Convert.ToDateTime(item["FecNac"]);
        //                    }
        //                    else
        //                    {
        //                        obj.FecNac = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.FecNac = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }

        //                columna = "Sexo";
        //                texto = Convert.ToString(item["Sexo"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.NombreUsuario = Convert.ToString(item["Sexo"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "CodLocUsuario";
        //                try
        //                {
        //                    texto = Convert.ToString(item["CodLocUsuario"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.CodLocUsuario = Convert.ToInt64(item["CodLocUsuario"]);
        //                    }
        //                    else
        //                    {
        //                        obj.CodLocUsuario = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "LocUsuario";
        //                texto = Convert.ToString(item["LocUsuario"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.LocUsuario = Convert.ToString(item["LocUsuario"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "CECOS";
        //                texto = Convert.ToString(item["CECOS"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.CECOS = Convert.ToString(item["CECOS"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "SerSum";
        //                texto = Convert.ToString(item["SerSum"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.SerSum = Convert.ToString(item["SerSum"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "CodSerSum";
        //                texto = Convert.ToString(item["CodSerSum"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.CodSerSum = Convert.ToString(item["CodSerSum"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "NomSerSum";
        //                texto = Convert.ToString(item["NomSerSum"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.CodSerSum = Convert.ToString(item["NomSerSum"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "TIGA";
        //                texto = Convert.ToString(item["TIGA"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TIGA = Convert.ToString(item["TIGA"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "TipoSer";
        //                try
        //                {
        //                    texto = Convert.ToString(item["TipoSer"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.TipoSer = Convert.ToInt64(item["TipoSer"]);
        //                    }
        //                    else
        //                    {
        //                        obj.TipoSer = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "FecPre";
        //                try
        //                {
        //                    texto = Convert.ToString(item["FecPre"]);
        //                    if (!string.IsNullOrEmpty(texto) && (!texto.ToUpper().Contains("SIN DATO") && !texto.ToUpper().Contains("SIN_DATO")))
        //                    {
        //                        obj.FecPre = Convert.ToDateTime(item["FecPre"]);
        //                    }
        //                    else
        //                    {
        //                        obj.FecPre = new DateTime(1900, 01, 01);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        obj.FecPre = CambiarFormatoFecha(texto);
        //                    }
        //                    catch
        //                    {
        //                        textError = columna + ", formato incorrecto.";
        //                        throw new Exception(textError);
        //                    }
        //                }

        //                columna = "CantidadSerSum";
        //                try
        //                {
        //                    texto = Convert.ToString(item["CantidadSerSum"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.CantidadSerSum = Convert.ToInt64(item["CantidadSerSum"]);
        //                    }
        //                    else
        //                    {
        //                        obj.CantidadSerSum = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Valor";
        //                try
        //                {
        //                    texto = Convert.ToString(item["Valor"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.Valor = Convert.ToDecimal(item["Valor"]);
        //                    }
        //                    else
        //                    {
        //                        obj.Valor = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Dx";
        //                texto = Convert.ToString(item["Dx"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.Dx = Convert.ToString(item["Dx"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "DescripDx";
        //                texto = Convert.ToString(item["DescripDx"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.DescripDx = Convert.ToString(item["DescripDx"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "GrupoDx";
        //                texto = Convert.ToString(item["GrupoDx"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.GrupoDx = Convert.ToString(item["GrupoDx"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Edad";
        //                try
        //                {
        //                    texto = Convert.ToString(item["Edad"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.Edad = Convert.ToInt32(item["Edad"]);
        //                    }
        //                    else
        //                    {
        //                        obj.Edad = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Quinquenio";
        //                texto = Convert.ToString(item["Quinquenio"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.Quinquenio = Convert.ToString(item["Quinquenio"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "TIGADetalle";
        //                texto = Convert.ToString(item["TIGADetalle"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TIGADetalle = Convert.ToString(item["TIGADetalle"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "TIGAGSD";
        //                try
        //                {
        //                    texto = Convert.ToString(item["TIGAGSD"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.TIGAGSD = Convert.ToInt64(item["TIGAGSD"]);
        //                    }
        //                    else
        //                    {
        //                        obj.TIGAGSD = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "DescripTIGAGSD";
        //                texto = Convert.ToString(item["DescripTIGAGSD"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.DescripTIGAGSD = Convert.ToString(item["DescripTIGAGSD"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "RegPrestacion";
        //                texto = Convert.ToString(item["RegPrestacion"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.RegPrestacion = Convert.ToString(item["RegPrestacion"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "DescripCECOS";
        //                texto = Convert.ToString(item["DescripCECOS"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.DescripTIGAGSD = Convert.ToString(item["DescripCECOS"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "TipoSalud";
        //                texto = Convert.ToString(item["TipoSalud"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TipoSalud = Convert.ToString(item["TipoSalud"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "TipoPoblacion";
        //                texto = Convert.ToString(item["TipoPoblacion"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.TipoPoblacion = Convert.ToString(item["TipoPoblacion"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }


        //                columna = "UnicoID";
        //                try
        //                {
        //                    texto = Convert.ToString(item["UnicoID"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.UnicoID = Convert.ToInt64(item["UnicoID"]);
        //                    }
        //                    else
        //                    {
        //                        obj.UnicoID = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "UnicoFactura";
        //                try
        //                {
        //                    texto = Convert.ToString(item["UnicoFactura"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.UnicoFactura = Convert.ToInt64(item["UnicoFactura"]);
        //                    }
        //                    else
        //                    {
        //                        obj.UnicoFactura = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Eventos";
        //                try
        //                {
        //                    texto = Convert.ToString(item["Eventos"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.Eventos = Convert.ToInt64(item["Eventos"]);
        //                    }
        //                    else
        //                    {
        //                        obj.Eventos = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "RegReporte";
        //                texto = Convert.ToString(item["RegReporte"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.RegReporte = Convert.ToString(item["RegReporte"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "IDQuinquenio";
        //                try
        //                {
        //                    texto = Convert.ToString(item["IDQuinquenio"]);
        //                    if (!string.IsNullOrEmpty(texto))
        //                    {
        //                        obj.IDQuinquenio = Convert.ToInt64(item["IDQuinquenio"]);
        //                    }
        //                    else
        //                    {
        //                        obj.IDQuinquenio = 0;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    textError = columna + ", formato incorrecto.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "UNIS";
        //                texto = Convert.ToString(item["UNIS"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.CausadaOtraReg = Convert.ToString(item["UNIS"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "CausadaOtraReg";
        //                texto = Convert.ToString(item["CausadaOtraReg"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.CausadaOtraReg = Convert.ToString(item["CausadaOtraReg"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Latitud";
        //                texto = Convert.ToString(item["Latitud"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.Latitud = Convert.ToString(item["Latitud"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                columna = "Longitud";
        //                texto = Convert.ToString(item["Longitud"]);

        //                if (texto.Length <= 300)
        //                {
        //                    obj.Longitud = Convert.ToString(item["Longitud"]);
        //                }
        //                else
        //                {
        //                    textError = columna + ", solo puede contener 300 caracteres.";
        //                    throw new Exception(textError);
        //                }

        //                OBJDetalle.Add(obj);
        //                obj = new gasto_por_Servicio_cargue_dtll();
        //                IntContador = IntContador + 1;

        //                if (IntContadorFilas >= 30000)
        //                {
        //                    BusClass.InsertarGastosPorServicioDtll(OBJDetalle, ref MsgRes);
        //                    IntContadorFilas = 0;
        //                    OBJDetalle = new List<gasto_por_Servicio_cargue_dtll>();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (textError != "" && textError != null)
        //        {
        //            MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + textError;
        //        }
        //        else
        //        {
        //            MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + columna;
        //        }

        //        MsgRes.CodeError = ex.Message;
        //        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
        //    }
        //}


        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 19 de julio de 2022
        /// Metodo que obtiene el listado de cargues de gasto por servicio
        /// </summary>
        /// <returns></returns>
        public List<vw_consulta_gasto_por_servicio> ObtenerListadoCarguesGastoPorServicio()
        {
            return BusClass.ObtenerListadoCarguesGastoPorServicio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCargueBase"></param>
        /// <returns></returns>
        public List<Management_gasto_por_servicio_por_idCargueBaseResult> GetListGastoPorServicioDtllPorIdCargueBase(int idCargueBase)
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {

                db.CommandTimeout = 50000;
                return db.Management_gasto_por_servicio_por_idCargueBase(idCargueBase).ToList();
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 1 de agosto de 2022
        /// Metodo para obtener el consolidado de gasto por servicio
        /// </summary>
        /// <param name="idCargueBase"></param>
        /// <returns></returns>
        public List<Management_gasto_x_servicio_consolidadoResult> ObtenerConsolidadoGastoPorServicioPorIdCargueBase(int idCargueBase)
        {
            return BusClass.ObtenerConsolidadoGastoPorServicioPorIdCargueBase(idCargueBase);
        }


        /// <summary>
        /// Autor: Alexis QUIÑONES CASTILLO
        /// Fecha: 8 de agosto de 2022
        /// Metodo que se utiliza para devolver el formato correcto de la fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private DateTime CambiarFormatoFecha(string fecha)
        {
            DateTime enter_date = new DateTime();
            string strDate = fecha;
            string[] dateString = strDate.Split('/');
            if (dateString.Count() <= 1)
                dateString = strDate.Split('-');

            if (Convert.ToInt32(dateString[0]) > 12)
            {
                enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
            }
            else
            {
                enter_date = Convert.ToDateTime(dateString[0] + "/" + dateString[1] + "/" + dateString[2]);
            }

            return enter_date;
        }






        public void InsertarDetallesCierreContable(DataTable dt, cierre_contable_cargue_base objbase, ref MessageResponseOBJ MsgRes)
        {

            Int32 IntContadorFilas = 0;
            Int32 id_cargue = 0;
            Int32 fila = 1;
            string columna = "";
            string texto = "";
            string textError = "";
            List<cierre_contable_cargue_detalle> result = new List<cierre_contable_cargue_detalle>();
            id_cargue = BusClass.InsertarCierreContable(objbase, ref MsgRes);
            var mensaje = "";

            //log_cargues_masivos logMas = new log_cargues_masivos();
            //logMas.id_cargue = id_cargue;

            //logMas.fecha_Cargue = DateTime.Now;
            //logMas.periodo_cargue = new DateTime((int)objbase.año, (int)objbase.mes, 1);
            //logMas.nombre_digita = SesionVar.NombreUsuario;
            //logMas.usuario_digita = SesionVar.UserName;
            //logMas.tipo_registro = "Cierre contable";

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    cierre_contable_cargue_detalle obj = new cierre_contable_cargue_detalle();
                    fila = fila + 1;
                    IntContadorFilas = IntContadorFilas + 1;

                    if (!string.IsNullOrEmpty(item["FACTURA SUIS"].ToString()))
                    {
                        obj.id_cargue = id_cargue;

                        columna = "FACTURA SUIS";
                        try
                        {
                            texto = Convert.ToString(item["FACTURA SUIS"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.factura_suis = Convert.ToDecimal(item["FACTURA SUIS"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);

                        }

                        columna = "ID DETALLE SAMI";
                        try
                        {
                            texto = Convert.ToString(item["ID DETALLE SAMI"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 9)
                                {
                                    obj.id_detalle_sami = Convert.ToInt32(item["ID DETALLE SAMI"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "FACTURA SAP";
                        texto = Convert.ToString(item["FACTURA SAP"]);
                        if (texto.Length <= 199)
                        {
                            obj.factura_sap = Convert.ToString(item["FACTURA SAP"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA DE RECEPCIÓN";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE RECEPCIÓN"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_recepcion = Convert.ToDateTime(item["FECHA DE RECEPCIÓN"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo fecha.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "FECHA DE FACTURA";
                        try
                        {
                            texto = Convert.ToString(item["FECHA DE FACTURA"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_fgactura = Convert.ToDateTime(item["FECHA DE FACTURA"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo fecha.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "CÓDIGO SAP";
                        try
                        {
                            texto = Convert.ToString(item["CÓDIGO SAP"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.codigo_sap = Convert.ToDecimal(item["CÓDIGO SAP"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "NIT";
                        try
                        {
                            texto = Convert.ToString(item["NIT"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.nit = Convert.ToDecimal(item["NIT"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }

                        columna = "RAZÓN SOCIAL";
                        texto = Convert.ToString(item["RAZÓN SOCIAL"]);
                        if (texto.Length <= 199)
                        {
                            obj.razon_social = Convert.ToString(item["RAZÓN SOCIAL"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "CIUDAD";
                        texto = Convert.ToString(item["CIUDAD"]);
                        if (texto.Length <= 199)
                        {
                            obj.ciudad = Convert.ToString(item["CIUDAD"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "REGIONAL";
                        texto = Convert.ToString(item["REGIONAL"]);
                        if (texto.Length <= 199)
                        {
                            obj.regional = Convert.ToString(item["REGIONAL"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "VALOR TOTAL FACTURA";
                        try
                        {
                            texto = Convert.ToString(item["VALOR TOTAL FACTURA"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.valor_total_factura = Convert.ToDecimal(item["VALOR TOTAL FACTURA"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "PEDIDO";
                        texto = Convert.ToString(item["PEDIDO"]);
                        if (texto.Length <= 199)
                        {
                            obj.pedido = Convert.ToString(item["PEDIDO"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "UNIS";
                        texto = Convert.ToString(item["UNIS"]);
                        if (texto.Length <= 199)
                        {
                            obj.unis = Convert.ToString(item["UNIS"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "MES ID";
                        try
                        {
                            texto = Convert.ToString(item["MES ID"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 3)
                                {
                                    obj.mes_id = Convert.ToInt32(item["MES ID"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 2 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }


                        columna = "MES";
                        texto = Convert.ToString(item["MES"]);
                        if (texto.Length <= 199)
                        {
                            obj.mes = Convert.ToString(item["MES"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "AÑO";
                        try
                        {
                            texto = Convert.ToString(item["AÑO"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 5)
                                {
                                    obj.año = Convert.ToInt32(item["AÑO"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 4 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "ESTADO DE FACTURA";
                        texto = Convert.ToString(item["ESTADO DE FACTURA"]);
                        if (texto.Length <= 199)
                        {
                            obj.estado_factura = Convert.ToString(item["ESTADO DE FACTURA"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "PERIODO DE GESTIÓN";
                        texto = Convert.ToString(item["PERIODO DE GESTIÓN"]);
                        if (texto.Length <= 199)
                        {
                            obj.periodo_gestion = Convert.ToString(item["PERIODO DE GESTIÓN"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "OBSERVACIÓN PENDIENTES";
                        texto = Convert.ToString(item["OBSERVACIÓN PENDIENTES"]);
                        if (texto.Length <= 199)
                        {
                            obj.observacion_pendiente = Convert.ToString(item["OBSERVACIÓN PENDIENTES"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 199 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "FECHA CONTABILIZACIÓN";
                        try
                        {
                            texto = Convert.ToString(item["FECHA CONTABILIZACIÓN"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.fecha_contabilizacion = Convert.ToDateTime(item["FECHA CONTABILIZACIÓN"]);
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo fecha.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "DOCUMENTO CONTABLE";
                        try
                        {
                            texto = Convert.ToString(item["DOCUMENTO CONTABLE"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.documento_contable = Convert.ToDecimal(item["DOCUMENTO CONTABLE"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto") || error.Contains("caracteres"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "RIPS";
                        try
                        {
                            texto = Convert.ToString(item["RIPS"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 13)
                                {
                                    obj.rips = Convert.ToDecimal(item["RIPS"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 12 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto. Es campo númerico.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("formato incorrecto"))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        obj.fecha_digita = DateTime.Now;
                        obj.usuario_digita = SesionVar.UserName;

                        result.Add(obj);
                        obj = new cierre_contable_cargue_detalle();
                    }
                }

                BusClass.InsertarCierreContableDetalle(result, ref MsgRes);
                //logMas.registros_Cargados = result.Count();

                if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                {
                    BusClass.EliminarCargueCierreContable(id_cargue);
                    mensaje = "Error en el cargue";
                }
                else
                {
                    mensaje = "Cargue completado";
                }

                //logMas.estado_cargue = mensaje;

                //var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna - " + columna + " : " + ex.Message;
                MsgRes.CodeError = ex.Message;

                mensaje = "Error en el cargue";

                //var IngresoLogMasivo = BusClass.InsertarLogCarguesMasivos(logMas);

                try
                {
                    BusClass.EliminarCargueCierreContable(id_cargue);
                }
                catch
                {
                    MsgRes.DescriptionResponse = "Error eliminando el cargue base";
                    MsgRes.CodeError = ex.Message;
                }
            }
        }

    }
}
#endregion