using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.EventosSalud
{

    public class EventosSalud
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
        #endregion

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #region CAMPOS
        public int id_evento { get; set; }
        public int id_cargue { get; set; }
        public string dependencia_de_salud { get; set; }
        public int anio { get; set; }
        public int id_mes { get; set; }
        public string mes { get; set; }
        public decimal consecutivo { get; set; }
        public DateTime fecha_de_reporte { get; set; }
        public DateTime fecha_de_ocurrencia_del_evento { get; set; }
        public string localidad_de_servicios_de_salud { get; set; }
        public string fuente_del_reporte { get; set; }
        public string reportante_nombre_de_quien_realiza_el_reporte { get; set; }
        public string nombre_de_municipio_donde_ocurrio_el_evento { get; set; }
        public decimal codigo_municipal_donde_ocurrio_el_evento { get; set; }
        public string reportante_identificacion_de_quien_realiza_el_reporte { get; set; }
        public string ambito_de_ocurrencia_del_evento { get; set; }
        public string nombre_del_prestador_en_donde_ocurrio_el_evento_adverso { get; set; }
        public string nit_del_prestador_en_donde_ocurrio_el_evento_adverso { get; set; }
        public decimal numero_de_identificacion_del_prestador_codigo_sap { get; set; }
        public string tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento { get; set; }
        public string numero_de_identificacion_del_beneficiario { get; set; }
        public string nombre_del_beneficiario { get; set; }
        public int edad_del_beneficiario { get; set; }
        public string descripcion_del_evento { get; set; }
        public string clasificacion_del_evento_de_la_atencion_en_salud { get; set; }
        public string categoria_del_evento { get; set; }
        public string subcategoria_del_evento { get; set; }
        public string resultado_negativo_de_la_medicacion { get; set; }
        public string confirmacion_evento_prevenible_no_prevenible { get; set; }
        public string severidad_del_desenlace { get; set; }
        public string probabilidad_de_repeticion { get; set; }
        public string concepto_auditoria { get; set; }
        public string gestion_de_la_gestoria_integral_de_la_calidad { get; set; }
        public string plan_de_mejora_al_prestador_si_o_no { get; set; }
        public DateTime fecha_radicacion_del_plan_de_mejora { get; set; }
        public DateTime fecha_programada_para_revision_de_plan_de_mejora { get; set; }
        public decimal costo_de_no_calidad { get; set; }
        public string descripcion_de_costo_de_no_calidad { get; set; }
        public string seguimiento { get; set; }
        public string novedades { get; set; }
        public int edicion_regional { get; set; }
        public int edicion_nacional { get; set; }

        public int tipoActualizacion { get; set; }
        #endregion


        public int ExcelMasivoAlertasDispe(DataTable dt2, evento_salud_cargue carg, ref MessageResponseOBJ MsgRes)
        {
            List<eventos_salud_registros> Listado = new List<eventos_salud_registros>();
            var RtaInsercion = 0;
            var usuario = SesionVar.NombreUsuario;
            var mensajeLog = "";

            try
            {
                string columna = "";
                int fila = 1;
                var textError = "";

                try
                {
                    foreach (DataRow item in dt2.Rows)
                    {
                        eventos_salud_registros obj = new eventos_salud_registros();

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
                            if (texto.Length <= 200)
                            {
                                obj.dependencia_de_salud = Convert.ToString(item["DEPENDENCIA DE SALUD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "AÑO";
                            texto = Convert.ToString(item["AÑO"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 4)
                                {
                                    obj.anio = Convert.ToInt32(item["AÑO"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 4 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }


                            columna = "ID MES";
                            texto = Convert.ToString(item["ID MES"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 2)
                                {
                                    obj.id_mes = Convert.ToInt32(item["ID MES"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 2 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "MES";
                            texto = Convert.ToString(item["MES"]);
                            if (texto.Length <= 15)
                            {
                                obj.mes = Convert.ToString(item["MES"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 15 caracteres.";
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


                            columna = "FECHA DE REPORTE";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA DE REPORTE"]);
                                if (fechas != null)
                                {
                                    obj.fecha_de_reporte = Convert.ToDateTime(item["FECHA DE REPORTE"]);

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



                            columna = "FECHA DE OCURRENCIA DEL EVENTO";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA DE OCURRENCIA DEL EVENTO"]);
                                if (fechas != null)
                                {
                                    obj.fecha_de_ocurrencia_del_evento = Convert.ToDateTime(item["FECHA DE OCURRENCIA DEL EVENTO"]);

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

                            columna = "LOCALIDAD DE SERVICIOS DE SALUD";
                            texto = Convert.ToString(item["LOCALIDAD DE SERVICIOS DE SALUD"]);
                            if (texto.Length <= 200)
                            {
                                obj.localidad_de_servicios_de_salud = Convert.ToString(item["LOCALIDAD DE SERVICIOS DE SALUD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "FUENTE DEL REPORTE";
                            texto = Convert.ToString(item["FUENTE DEL REPORTE"]);
                            if (texto.Length <= 200)
                            {
                                obj.fuente_del_reporte = Convert.ToString(item["FUENTE DEL REPORTE"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE)";
                            texto = Convert.ToString(item["REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE)"]);
                            if (texto.Length <= 200)
                            {
                                obj.reportante_nombre_de_quien_realiza_el_reporte = Convert.ToString(item["REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE)"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "NOMBRE DE MUNICIPIO  DONDE OCURRIO EL EVENTO";
                            texto = Convert.ToString(item["NOMBRE DE MUNICIPIO  DONDE OCURRIO EL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_de_municipio_donde_ocurrio_el_evento = Convert.ToString(item["NOMBRE DE MUNICIPIO  DONDE OCURRIO EL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }



                            columna = "CÓDIGO MUNICIPAL DONDE OCURRIO EL EVENTO";
                            texto = Convert.ToString(item["CÓDIGO MUNICIPAL DONDE OCURRIO EL EVENTO"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.codigo_municipal_donde_ocurrio_el_evento = Convert.ToDecimal(item["CÓDIGO MUNICIPAL DONDE OCURRIO EL EVENTO"]);
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


                            columna = "REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE)";
                            texto = Convert.ToString(item["REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE)"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.reportante_identificacion_de_quien_realiza_el_reporte = Convert.ToString(item["REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE)"]);
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

                            columna = "ÁMBITO DE OCURRENCIA DEL EVENTO";
                            texto = Convert.ToString(item["ÁMBITO DE OCURRENCIA DEL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.ambito_de_ocurrencia_del_evento = Convert.ToString(item["ÁMBITO DE OCURRENCIA DEL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NOMBRE DEL PRESTADOR EN DONDE OCURRIÓ EL EVENTO ADVERSO";
                            texto = Convert.ToString(item["NOMBRE DEL PRESTADOR EN DONDE OCURRIÓ EL EVENTO ADVERSO"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_del_prestador_en_donde_ocurrio_el_evento_adverso = Convert.ToString(item["NOMBRE DEL PRESTADOR EN DONDE OCURRIÓ EL EVENTO ADVERSO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "NÚMERO DE IDENTIFICACIÓN DEL PRESTADOR (CÓDIGO SAP)";
                            texto = Convert.ToString(item["NÚMERO DE IDENTIFICACIÓN DEL PRESTADOR (CÓDIGO SAP)"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.numero_de_identificacion_del_prestador_codigo_sap = Convert.ToDecimal(item["NÚMERO DE IDENTIFICACIÓN DEL PRESTADOR (CÓDIGO SAP)"]);
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


                            columna = "TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO EN EL CUAL OCURRIÓ EL EVENTO";
                            texto = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO EN EL CUAL OCURRIÓ EL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.tipo_de_identificacion_del_beneficiario_en_el_cual_ocurrio_el_evento = Convert.ToString(item["TIPO DE IDENTIFICACIÓN DEL BENEFICIARIO EN EL CUAL OCURRIÓ EL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NÚMERO DE IDENTIFICACIÓN DEL BENEFICIARIO";
                            texto = Convert.ToString(item["NÚMERO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.numero_de_identificacion_del_beneficiario = Convert.ToString(item["NÚMERO DE IDENTIFICACIÓN DEL BENEFICIARIO"]);
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


                            columna = "NOMBRE DEL BENEFICIARIO";
                            texto = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_del_beneficiario = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "EDAD DEL BENEFICIARIO";
                            texto = Convert.ToString(item["EDAD DEL BENEFICIARIO"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.edad_del_beneficiario = Convert.ToInt32(item["EDAD DEL BENEFICIARIO"]);
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


                            columna = "DESCRIPCIÓN DEL EVENTO";
                            texto = Convert.ToString(item["DESCRIPCIÓN DEL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_del_evento = Convert.ToString(item["DESCRIPCIÓN DEL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "CLASIFICACIÓN DEL EVENTO DE LA ATENCIÓN EN SALUD";
                            texto = Convert.ToString(item["CLASIFICACIÓN DEL EVENTO DE LA ATENCIÓN EN SALUD"]);
                            if (texto.Length <= 200)
                            {
                                obj.clasificacion_del_evento_de_la_atencion_en_salud = Convert.ToString(item["CLASIFICACIÓN DEL EVENTO DE LA ATENCIÓN EN SALUD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "CATEGORÍA DEL EVENTO";
                            texto = Convert.ToString(item["CATEGORÍA DEL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.categoria_del_evento = Convert.ToString(item["CATEGORÍA DEL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "SUBCATEGORÍA DEL EVENTO";
                            texto = Convert.ToString(item["SUBCATEGORÍA DEL EVENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.subcategoria_del_evento = Convert.ToString(item["SUBCATEGORÍA DEL EVENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "RESULTADO NEGATIVO DE LA MEDICACIÓN";
                            texto = Convert.ToString(item["RESULTADO NEGATIVO DE LA MEDICACIÓN"]);
                            if (texto.Length <= 200)
                            {
                                obj.resultado_negativo_de_la_medicacion = Convert.ToString(item["RESULTADO NEGATIVO DE LA MEDICACIÓN"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CONFIRMACIÓN EVENTO (PREVENIBLE /NO PREVENIBLE)";
                            texto = Convert.ToString(item["CONFIRMACIÓN EVENTO (PREVENIBLE /NO PREVENIBLE)"]);
                            if (texto.Length <= 200)
                            {
                                obj.confirmacion_evento_prevenible_no_prevenible = Convert.ToString(item["CONFIRMACIÓN EVENTO (PREVENIBLE /NO PREVENIBLE)"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "SEVERIDAD DEL DESENLACE";
                            texto = Convert.ToString(item["SEVERIDAD DEL DESENLACE"]);
                            if (texto.Length <= 200)
                            {
                                obj.severidad_del_desenlace = Convert.ToString(item["SEVERIDAD DEL DESENLACE"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "PROBABILIDAD DE REPETICIÓN";
                            texto = Convert.ToString(item["PROBABILIDAD DE REPETICIÓN"]);
                            if (texto.Length <= 200)
                            {
                                obj.probabilidad_de_repeticion = Convert.ToString(item["PROBABILIDAD DE REPETICIÓN"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "CONCEPTO AUDITORIA";
                            texto = Convert.ToString(item["CONCEPTO AUDITORIA"]);
                            if (texto.Length <= 200)
                            {
                                obj.concepto_auditoria = Convert.ToString(item["CONCEPTO AUDITORIA"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "GESTIÓN DE LA GESTORÍA INTEGRAL DE LA CALIDAD";
                            texto = Convert.ToString(item["GESTIÓN DE LA GESTORÍA INTEGRAL DE LA CALIDAD"]);
                            if (texto.Length <= 200)
                            {
                                obj.gestion_de_la_gestoria_integral_de_la_calidad = Convert.ToString(item["GESTIÓN DE LA GESTORÍA INTEGRAL DE LA CALIDAD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "PLAN DE MEJORA AL PRESTADOR (SI O NO)";
                            texto = Convert.ToString(item["PLAN DE MEJORA AL PRESTADOR (SI O NO)"]);
                            if (texto.Length <= 200)
                            {
                                obj.plan_de_mejora_al_prestador_si_o_no = Convert.ToString(item["PLAN DE MEJORA AL PRESTADOR (SI O NO)"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "FECHA RADICACIÓN DEL PLAN DE MEJORA.";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA RADICACIÓN DEL PLAN DE MEJORA."]);
                                if (fechas != null)
                                {
                                    obj.fecha_radicacion_del_plan_de_mejora = Convert.ToDateTime(item["FECHA RADICACIÓN DEL PLAN DE MEJORA."]);

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


                            columna = "FECHA PROGRAMADA PARA REVISIÓN DE PLAN DE MEJORA.";
                            try
                            {
                                fechas = Convert.ToDateTime(item["FECHA PROGRAMADA PARA REVISIÓN DE PLAN DE MEJORA."]);
                                if (fechas != null)
                                {
                                    obj.fecha_programada_para_revision_de_plan_de_mejora = Convert.ToDateTime(item["FECHA PROGRAMADA PARA REVISIÓN DE PLAN DE MEJORA."]);

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

                            columna = "COSTO DE NO CALIDAD";
                            texto = Convert.ToString(item["COSTO DE NO CALIDAD"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 18)
                                {
                                    obj.costo_de_no_calidad = Convert.ToDecimal(item["COSTO DE NO CALIDAD"]);
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

                            columna = "DESCRIPCIÓN DE COSTO DE NO CALIDAD";
                            texto = Convert.ToString(item["DESCRIPCIÓN DE COSTO DE NO CALIDAD"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_de_costo_de_no_calidad = Convert.ToString(item["DESCRIPCIÓN DE COSTO DE NO CALIDAD"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "SEGUIMIENTO";
                            texto = Convert.ToString(item["SEGUIMIENTO"]);
                            if (texto.Length <= 200)
                            {
                                obj.seguimiento = Convert.ToString(item["SEGUIMIENTO"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "NOVEDADES";
                            texto = Convert.ToString(item["NOVEDADES"]);
                            if (texto.Length <= 200)
                            {
                                obj.novedades = Convert.ToString(item["NOVEDADES"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            obj.usuario_digita = SesionVar.UserName;
                            obj.fecha_digita = DateTime.Now;

                            Listado.Add(obj);
                            obj = new eventos_salud_registros();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.CargueEventosSalud(carg, Listado, ref MsgRes);
                        return RtaInsercion;

                    }
                    else
                    {
                        var mensaje = "";
                        mensaje = "Hoja vacía.";
                        MsgRes.DescriptionResponse = mensaje;
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;

                        return RtaInsercion;
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

            return RtaInsercion;
        }
    }
}