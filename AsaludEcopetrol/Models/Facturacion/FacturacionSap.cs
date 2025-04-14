using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;

namespace AsaludEcopetrol.Models.Facturacion
{
    public class FacturacionSap
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

        private BussinesManager.SessionState _SesionVar;
        public BussinesManager.SessionState SesionVar
        {
            get
            {
                if (_SesionVar == null)
                {
                    _SesionVar = new BussinesManager.SessionState();
                }
                return _SesionVar;
            }
            set { _SesionVar = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion


        #region OPERACIONES
        public List<facturacion_sap_cargue> validarCargueFacturaSap(int? mes, int? año)
        {
            return BusClass.validarCargueFacturaSap(mes, año);
        }

        public int InsertarFacturacionSAP(List<facturacion_sap_dtll> List, facturacion_sap_cargue objbase, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturacionSAP(List, objbase, ref MsgRes);
        }

        public List<management_facturacionSAP_listaResult> ListarFacturacionSAP()
        {
            return BusClass.ListarFacturacionSAP();
        }

        public List<facturacion_sap_dtll> ValidacionArchivo(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {
            List<facturacion_sap_dtll> result = new List<facturacion_sap_dtll>();
            string columna = "";
            int fila = 1;
            var textError = "";

            try
            {
                foreach (DataRow item in dt2.Rows)
                {
                    facturacion_sap_dtll obj = new facturacion_sap_dtll();
                    fila++;

                    var texto = "";

                    columna = "Sociedad";
                    texto = Convert.ToString(item["Sociedad"]);

                    if (!string.IsNullOrEmpty(texto))
                    {
                        var numero = 0;
                        var decimales = new decimal();

                        columna = "Sociedad";
                        texto = Convert.ToString(item["Sociedad"]);

                        if (texto.Length <= 500)
                        {
                            obj.Sociedad = Convert.ToString(item["Sociedad"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Cuenta";
                        try
                        {
                            texto = Convert.ToString(item["Cuenta"]);
                            if (texto.Length != null)
                            {
                                obj.cuenta = Convert.ToInt32(item["Cuenta"]);
                            }
                            else
                            {
                                obj.cuenta = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "NIT";
                        try
                        {
                            texto = Convert.ToString(item["NIT"]);
                            if (texto.Length != null)
                            {
                                obj.nit = Convert.ToString(item["NIT"]).ToUpper();
                            }
                            else
                            {
                                obj.nit = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Cuenta de mayor";
                        try
                        {
                            texto = Convert.ToString(item["Cuenta de mayor"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.cuenta_de_mayor = Convert.ToString(item["Cuenta de mayor"]).ToUpper();
                            }
                            else
                            {
                                obj.cuenta_de_mayor = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Nº documento";
                        try
                        {
                            texto = Convert.ToString(item["Nº documento"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.nro_documento = Convert.ToString(item["Nº documento"]).ToUpper();
                            }
                            else
                            {
                                obj.nro_documento = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Clase de documento";
                        texto = Convert.ToString(item["Clase de documento"]);
                        if (texto.Length <= 100)
                        {
                            obj.clase_documento = Convert.ToString(item["Clase de documento"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Referencia";
                        texto = Convert.ToString(item["Referencia"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.referencia = Convert.ToString(item["Referencia"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Referencia sin letras";
                        try
                        {
                            texto = Convert.ToString(item["Referencia sin letras"]).ToUpper();
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.referencia_sin_letras = Convert.ToString(item["Referencia sin letras"]).ToUpper();
                            }
                            else
                            {
                                obj.referencia_sin_letras = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "Fe.contabilización";
                        try
                        {
                            texto = Convert.ToString(item["Fe.contabilización"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.fe_contabilizacion = Convert.ToDateTime(item["Fe.contabilización"]);
                            }
                            else
                            {
                                obj.fe_contabilizacion = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Fecha compensación";
                        try
                        {
                            texto = Convert.ToString(item["Fecha compensación"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.fecha_compensacion = Convert.ToDateTime(item["Fecha compensación"]);
                            }
                            else
                            {
                                obj.fecha_compensacion = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "Fecha de pago";
                        try
                        {
                            texto = Convert.ToString(item["Fecha de pago"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.fecha_pago = Convert.ToDateTime(item["Fecha de pago"]);
                            }
                            else
                            {
                                obj.fecha_pago = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Moneda del documento";
                        texto = Convert.ToString(item["Moneda del documento"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.moneda_documento = Convert.ToString(item["Moneda del documento"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Importe en moneda doc.";
                        try
                        {
                            texto = Convert.ToString(item["Importe en moneda doc."]).ToUpper();
                            if (decimales != null)
                            {
                                obj.importe_moneda_doc = Convert.ToString(item["Importe en moneda doc."]).ToUpper();
                            }
                            else
                            {
                                obj.importe_moneda_doc = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "Nombre del usuario";
                        texto = Convert.ToString(item["Nombre del usuario"]).ToUpper();
                        if (texto.Length <= 200)
                        {
                            obj.nombre_usuario = Convert.ToString(item["Nombre del usuario"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Texto";
                        texto = Convert.ToString(item["Texto"]).ToUpper();
                        if (texto.Length <= 500)
                        {
                            obj.texto = Convert.ToString(item["Texto"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 500 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Importe en moneda local";
                        try
                        {
                            texto = Convert.ToString(item["Importe en moneda local"]).ToUpper();

                            if (texto.Length != null)
                            {
                                obj.importe_moneda_local = Convert.ToString(item["Importe en moneda local"]).ToUpper();
                            }
                            else
                            {
                                obj.importe_moneda_local = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "Base p. plazo pago";
                        try
                        {
                            texto = Convert.ToString(item["Base p. plazo pago"]);
                            if (texto.Length != null && texto.Count() != 0)
                            {
                                obj.base_p_plazo_pago = Convert.ToDateTime(item["Base p. plazo pago"]);
                            }
                            else
                            {
                                obj.base_p_plazo_pago = new DateTime(1900, 01, 01);
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }


                        columna = "Clave referencia 3";
                        texto = Convert.ToString(item["Clave referencia 3"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.clave_referencia_3 = Convert.ToString(item["Clave referencia 3"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Año";
                        try
                        {
                            texto = Convert.ToString(item["Año"]);
                            if (texto.Length != null)
                            {
                                obj.año = Convert.ToInt32(item["Año"]);
                            }
                            else
                            {
                                obj.año = 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Mes";
                        try
                        {
                            texto = Convert.ToString(item["Mes"]).ToUpper();
                            if (texto.Length != null)
                            {
                                obj.mes = Convert.ToString(item["Mes"]).ToUpper();
                            }
                            else
                            {
                                obj.mes = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            textError = columna + ", formato incorrecto.";
                            throw new Exception(textError);
                        }

                        columna = "Regional";
                        texto = Convert.ToString(item["Regional"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.regional = Convert.ToString(item["Regional"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis";
                        texto = Convert.ToString(item["Unis"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.unis = Convert.ToString(item["Unis"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Localidad";
                        texto = Convert.ToString(item["Localidad"]).ToUpper();
                        if (texto.Length <= 100)
                        {
                            obj.localidad = Convert.ToString(item["Localidad"]).ToUpper();
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        obj.usuario_digita = SesionVar.UserName;
                        obj.fecha_digita = DateTime.Now;

                        result.Add(obj);
                        obj = new facturacion_sap_dtll();
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

            return result;
        }


        #endregion  OPERACIONES

    }
}