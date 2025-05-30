using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ANALITICA_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class RipsDepurados
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

        #endregion

        #region  CREACION PRESTADOR

        public double nit { get; set; }
        public string nombre_prestador { get; set; }
        public string TipoID { get; set; }
        public string IDPrestador { get; set; }
        public string regional { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }

        #endregion

        #region METODOS

        public void CargarRipsDepuradosAC(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {
                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_ac_carguedtll> result = new List<rips_depurados_ac_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_ac_carguedtll obj = new rips_depurados_ac_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";
                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {

                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_de_la_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_prestador_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Tipo de identificación del usuario";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.tipo_identificacion_del_usuario = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Número de identificación del usuario";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_identificacion_usuario = Convert.ToDecimal(texto); } else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Fecha de la consulta";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de la consulta"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_consulta = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); } else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Número de autorización";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de autorización"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.numero_de_autorizacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Código de la consulta";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código de la consulta"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_consulta = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Finalidad de la consulta";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Finalidad de la consulta"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.finalidad_consulta = Convert.ToDecimal(texto); }

                            columna = "Causa externa";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Causa externa"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.causa_externa = Convert.ToDecimal(texto); }

                            columna = "Código del diagnóstico principal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_diagnostico_principal = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else
                            {
                                msgError = "El valor de la columna no puede estar vacio"; break;
                            }

                            columna = "Código del diagnóstico relacionado No. 1";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del diagnóstico relacionado No. 1"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_diagnostico_relacionado_no_1 = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Código del diagnóstico relacionado No. 2";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del diagnóstico relacionado No. 2"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_diagnostico_relacionado_no_2 = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Código del diagnóstico relacionado No. 3";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del diagnóstico relacionado No. 3"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_diagnostico_relacionado_no_3 = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }


                            columna = "Tipo de diagnóstico principal";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Tipo de diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.tipo_diagnostico_principal = Convert.ToInt32(texto);
                            }
                            else
                            {
                                obj.tipo_diagnostico_principal = 0;
                            }


                            columna = "Valor de la consulta";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor de la consulta"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.valor_consulta = Convert.ToDecimal(texto);
                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Valor de la cuota moderadora";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor de la cuota moderadora"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.valor_cuota_moderadora = Convert.ToDecimal(texto);
                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Valor neto a pagar";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor neto a pagar"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.valor_neto_a_pagar = Convert.ToDecimal(texto);
                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Descripción de la consulta";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción de la consulta"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_consulta = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción de la finalidad de la consulta";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción de la finalidad de la consulta"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_finalidad_de_la_consulta = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción de la causa externa";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción de la causa externa"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_causa_externa = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción tipo de diagnóstico principal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción tipo de diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_tipo_diagnostico_principal = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico principal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_del_diagnostico_principal = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico relacionado No. 1";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico relacionado No. 1"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_del_diagnostico_relacionado_no_1 = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico relacionado No. 2";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico relacionado No. 2"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_del_diagnostico_relacionado_no_2 = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico relacionado No. 3";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico relacionado No. 3"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_del_diagnostico_relacionado_no_3 = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Grupo Dx";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Grupo Dx"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.grupo_dx = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }

                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo. ";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += "[Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += "[Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += "[Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }

                            throw new Exception("");
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAC(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        throw new Exception("");
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        public void CargarRipsDepuradosAP(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {

                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_ap_carguedtll> result = new List<rips_depurados_ap_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_ap_carguedtll obj = new rips_depurados_ap_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";

                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {
                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_prestador_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Tipo de identificación del usuario";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.tipo_identificacion_usuario = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Número de identificación del usuario en el sistema";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación del usuario en el sistema"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_identificacion_usuario = Convert.ToDecimal(texto); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Fecha del procedimiento";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_procedimiento = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Número de autorización";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de autorización"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.numero_autorizacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Código del procedimiento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_procedimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else
                            {
                                msgError = "El valor de la columna no puede estar vacio"; break;
                            }

                            columna = "Ámbito de realización del procedimiento";
                            texto = Convert.ToString(item["Ámbito de realización del procedimiento"]);
                            tipoColumna = "Númerico";
                            if (!string.IsNullOrEmpty(texto)) { obj.ambito_realizacion_procedimiento = Convert.ToDecimal(texto); }

                            columna = "Finalidad del procedimiento";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Finalidad del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.finalidad_procedimiento = Convert.ToDecimal(texto); }

                            columna = "Personal que atiende";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Personal que atiende"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.personal_que_atiende = Convert.ToDecimal(texto); }

                            columna = "Diagnóstico principal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_principal = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico relacionado";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Complicación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Complicación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.complicacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }


                            columna = "Forma de realización del acto quirúrgico";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Forma de realización del acto quirúrgico"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.forma_realizacion_acto_quirurgico = Convert.ToDecimal(texto); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Valor del procedimiento";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.valor_del_procedimiento = Convert.ToDecimal(texto);
                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Descripción código del procedimiento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción código del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_codigo_del_procedimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción ámbito de realización del procedimiento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción ámbito de realización del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_ambito_de_realizacion_del_procedimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción finalidad del procedimiento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción finalidad del procedimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_finalidad_del_procedimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción Personal que atiende";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción Personal que atiende"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_personal_que_atiende = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción forma de realización del acto quirúrgico";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción forma de realización del acto quirúrgico"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_forma_de_realizacion_del_acto_quirurgico = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico principal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico principal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_principal = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción del diagnóstico relacionado";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción del diagnóstico relacionado"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Descripción de la complicación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción de la complicación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_complicacion = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Grupo Dx";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Grupo Dx"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.grupo_dx = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo.";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAP(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        public void CargarRipsDepuradosAU(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {

                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_au_carguedtll> result = new List<rips_depurados_au_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_au_carguedtll obj = new rips_depurados_au_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";
                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {


                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_prestador_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }


                            columna = "Tipo de identificación del usuario";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.tipo_identificacion_usuario = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Número de identificación del usuario en el sistema";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación del usuario en el sistema"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_identificacion_usuario = Convert.ToDecimal(texto); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Fecha de ingreso del usuario a observación";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de ingreso del usuario a observación"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.Fecha_ingreso_usuario_a_observacion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Hora de ingreso del usuario a observación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de ingreso del usuario a observación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.hora_de_ingreso_del_usuario_a_observacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else
                            {
                                msgError = "El valor de la columna no puede estar vacio"; break;
                            }

                            columna = "Número de autorización";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de autorización"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.numero_de_autorizacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Causa externa";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Causa externa"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.causa_externa = Convert.ToDecimal(texto); }


                            columna = "Diagnóstico de la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico de la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_de_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else
                            {
                                msgError = "El valor de la columna no puede estar vacio"; break;
                            }

                            columna = "Diagnóstico relacionado No. 1 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 1 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_1_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico relacionado No. 2 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 2 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_2_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico relacionado No. 3 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 3 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_3_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Destino del usuario a la salida de observación";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Destino del usuario a la salida de observación"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.destino_del_usuario_a_la_salida_de_observacion = Convert.ToDecimal(texto); }

                            columna = "Estado a la salida";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Estado a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.estado_a_la_salida = Convert.ToDecimal(texto);
                            }
                            else
                            {
                                obj.estado_a_la_salida = 0;
                            }

                            columna = "Causa básica de muerte en urgencias";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Causa básica de muerte en urgencias"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.causa_basica_de_muerte_en_urgencias = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Fecha de la salida del usuario en observación";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de la salida del usuario en observación"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_de_la_salida_del_usuario_en_observacion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }

                            columna = "Hora de la salida del usuario en observación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de la salida del usuario en observación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.hora_de_la_salida_del_usuario_en_observacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Descripción causa externa";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción causa externa"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_causa_externa = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción destino del usuario a la salida de observación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción destino del usuario a la salida de observación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_destino_del_usuario_a_la_salida_de_observacion = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción estado a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción estado a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_estado_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico de salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico de salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_de_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 1 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 1 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_1_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 2 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 2 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_2_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 3 a la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 3 a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_3_a_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción causa básica de muerte en urgencias";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción causa básica de muerte en urgencias"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_causa_basica_de_muerte_en_urgencias = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Grupo Dx";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Grupo Dx"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.grupo_dx = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }

                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo.";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAU(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }


            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        public void CargarRipsDepuradosAH(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {
                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_ah_carguedtll> result = new List<rips_depurados_ah_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_ah_carguedtll obj = new rips_depurados_ah_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";
                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {

                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_de_la_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_del_prestador_de_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Tipo de documento de identificación del usuario";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de documento de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.tipo_de_documento_de_identificacion_del_usuario = texto; } else { msgError = "El texto de la columna no puede contener más de 25 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }


                            columna = "Número de identificación del usuario en el sistema";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación del usuario en el sistema"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_de_identificacion_del_usuario_en_el_sistema = Convert.ToDecimal(texto); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Vía de ingreso a la institución";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Vía de ingreso a la institución"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.via_de_ingreso_a_la_institucion = Convert.ToDecimal(texto);
                            }
                            else
                            {
                                obj.via_de_ingreso_a_la_institucion = 0;
                            }

                            columna = "Fecha de ingreso del usuario a la institución";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de ingreso del usuario a la institución"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_de_ingreso_del_usuario_a_la_institucion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Hora de ingreso del usuario a la institución";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de ingreso del usuario a la institución"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.hora_de_ingreso_del_usuario_a_la_institucion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Número de autorización";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de autorización"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_de_autorizacion = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Causa externa";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Causa externa"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.causa_externa = Convert.ToDecimal(texto); }

                            columna = "Diagnóstico principal de ingreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico principal de ingreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_principal_de_ingreso = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Diagnóstico principal de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico principal de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_principal_de_egreso = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Diagnóstico relacionado No. 1 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 1 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_1_de_egreso = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico relacionado No. 2 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 2 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_2_de_egreso = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico relacionado No. 3 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico relacionado No. 3 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_relacionado_no_3_de_egreso = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Diagnóstico de la complicación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico de la complicación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_de_la_complicacion = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Estado a la salida";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Estado a la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                obj.estado_a_la_salida = Convert.ToDecimal(texto);
                            }
                            else
                            {
                                obj.estado_a_la_salida = 0;
                            }

                            columna = "Diagnóstico de la causa básica de muerte";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico de la causa básica de muerte"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_de_la_causa_basica_de_muerte = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Fecha de egreso del usuario a la institución";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de egreso del usuario a la institución"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_de_egreso_del_usuario_a_la_institucion = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }

                            columna = "Hora de egreso del usuario de la institución";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de egreso del usuario de la institución"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.hora_de_egreso_del_usuario_de_la_institucion = texto.Trim(); } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Descripción vía de ingreso a la institución";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción vía de ingreso a la institución"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_via_de_ingreso_a_la_institucion = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción causa externa";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción causa externa"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_causa_externa = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción estado de la salida";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción estado de la salida"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_estado_de_la_salida = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico principal de ingreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico principal de ingreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_principal_de_ingreso = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico principal de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico principal de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_principal_de_egreso = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 1 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 1 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_1_de_egreso = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 2 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 2 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_2_de_egreso = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico relacionado No. 3 de egreso";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico relacionado No. 3 de egreso"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_relacionado_no_3_de_egreso = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico de la complicación";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico de la complicación"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_de_la_complicacion = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico de la causa básica de muerte";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico de la causa básica de muerte"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_de_la_causa_basica_de_muerte = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Grupo Dx";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Grupo Dx"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.grupo_dx = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }

                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }

                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }

                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo.";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAH(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        public void CargarRipsDepuradosAM(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {
                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_am_carguedtll> result = new List<rips_depurados_am_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_am_carguedtll obj = new rips_depurados_am_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";
                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {


                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_de_la_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_prestador_de_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Tipo de identificación del usuario";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de identificación del usuario"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.tipo_identificacion_usuario = texto; } else { msgError = "El texto de la columna no puede contener más de 25 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }


                            columna = "Número de identificación del usuario en el sistema";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación del usuario en el sistema"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_identificacion_del_usuario_en_el_sistema = Convert.ToDecimal(texto); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Número de autorización";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de autorización"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.numero_autorizacion = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Código del medicamento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del medicamento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.codigo_del_medicamento = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Tipo de medicamento";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Tipo de medicamento"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.tipo_de_medicamento = Convert.ToDecimal(texto); }

                            columna = "Nombre genérico del medicamento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Nombre genérico del medicamento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 250) { obj.nombre_genérico_del_medicamento = texto; } else { msgError = "El texto de la columna no puede contener más de 250 caracteres. "; break; }
                            }

                            columna = "Forma farmacéutica";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Forma farmacéutica"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 150) { obj.forma_farmacéutica = texto; } else { msgError = "El texto de la columna no puede contener más de 150 caracteres. "; break; }
                            }

                            columna = "Concentración del medicamento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Concentración del medicamento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 150) { obj.concentracion_del_medicamento = texto; } else { msgError = "El texto de la columna no puede contener más de 150 caracteres. "; break; }
                            }

                            columna = "Unidad de medida del medicamento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Unidad de medida del medicamento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 150) { obj.unidad_de_medida_del_medicamento = texto; } else { msgError = "El texto de la columna no puede contener más de 150 caracteres. "; break; }
                            }

                            columna = "Número de unidades";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de unidades"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_de_unidades = Convert.ToInt32(texto); }

                            columna = "Valor unitario de medicamento";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor unitario de medicamento"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.valor_unitario_de_medicamento = Convert.ToDecimal(texto); }

                            columna = "Valor total de medicamento";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Valor total de medicamento"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.valor_total_de_medicamento = Convert.ToDecimal(texto); }


                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 150) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 150 caracteres. "; break; }
                            }

                            columna = "Descripción tipo de medicamento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción tipo de medicamento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_tipo_de_medicamento = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }

                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo.";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAM(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        public void CargarRipsDepuradosAN(DataTable dt, rips_depurados_carguebase cargueBase, ref MessageResponseOBJ MsgRes)
        {
            String columna = "", msgError = "", tipoColumna = "";
            Int32 fila = 1, idCargueBase = 0; ;

            try
            {
                idCargueBase = BusClass.GuardarCargueBaseRipsDepurados(cargueBase, ref MsgRes);

                if (idCargueBase > 0)
                {
                    List<rips_depurados_an_carguedtll> result = new List<rips_depurados_an_carguedtll>();

                    foreach (DataRow item in dt.Rows)
                    {
                        rips_depurados_an_carguedtll obj = new rips_depurados_an_carguedtll();
                        fila = fila + 1;
                        string texto = "";

                        obj.id_cargue_base = idCargueBase;

                        columna = "Número de la factura";
                        if (!string.IsNullOrEmpty(item["Número de la factura"].ToString()))
                        {

                            columna = "Número de la factura";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Número de la factura"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.numero_de_la_factura = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Código del prestador de servicios de salud";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Código del prestador de servicios de salud"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.codigo_prestador_de_servicios_de_salud = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }

                            columna = "Tipo de identificación de la madre";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Tipo de identificación de la madre"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.tipo_identificacion_de_la_madre = texto; } else { msgError = "El texto de la columna no puede contener más de 25 caracteres. "; break; }
                            }
                            else { msgError = "El valor de la columna no puede estar vacio. "; break; }


                            columna = "Número de identificación de la madre en el sistema";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Número de identificación de la madre en el sistema"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.numero_identificacion_de_la_madre = Convert.ToDecimal(texto); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Fecha de nacimiento del recién nacido";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de nacimiento del recién nacido"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_de_nacimiento_del_recien_nacido = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }
                            else { msgError = "El valor de la columna no puede estar vacio"; break; }


                            columna = "Hora de nacimiento";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de nacimiento"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 25) { obj.hora_de_nacimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 25 caracteres. "; break; }
                            }

                            columna = "Edad gestacional";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Edad gestacional"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.edad_gestacional = Convert.ToInt32(texto); }

                            columna = "Control prenatal";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Control prenatal"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.control_prenatal = Convert.ToDecimal(texto); }


                            columna = "Sexo";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Sexo"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.sexo = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            columna = "Peso";
                            tipoColumna = "Númerico";
                            texto = Convert.ToString(item["Peso"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.peso = Convert.ToDecimal(texto); }

                            columna = "Diagnóstico del recién nacido";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Diagnóstico del recién nacido"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.diagnostico_del_recien_nacido = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Causa básica de muerte";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Causa básica de muerte"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.causa_basica_de_muerte = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Fecha de muerte del recién nacido";
                            tipoColumna = "Fecha";
                            texto = Convert.ToString(item["Fecha de muerte del recién nacido"]);
                            if (!string.IsNullOrEmpty(texto)) { obj.fecha_de_muerte_del_recien_nacido = Convert.ToDateTime(Convert.ToDateTime(texto).ToString("MM/dd/yyyy")); }

                            columna = "Hora de muerte del recién nacido";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Hora de muerte del recién nacido"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.hora_de_nacimiento = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 150) { obj.prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 150 caracteres. "; break; }
                            }

                            columna = "Descripción control prenatal";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción control prenatal"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_control_prenatal = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción diagnóstico del recién nacido";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción diagnóstico del recién nacido"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_diagnostico_del_recien_nacido = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Descripción causa básica de muerte";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Descripción causa básica de muerte"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.descripcion_causa_basica_de_muerte = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Grupo Dx";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Grupo Dx"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 500) { obj.grupo_dx = texto; } else { msgError = "El texto de la columna no puede contener más de 500 caracteres. "; break; }
                            }

                            columna = "Ciudad prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Ciudad prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 100) { obj.ciudad_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 100 caracteres. "; break; }
                            }

                            columna = "Regional prestador";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Regional prestador"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50) { obj.regional_prestador = texto; } else { msgError = "El texto de la columna no puede contener más de 50 caracteres. "; break; }
                            }

                            columna = "Clasificación duplicados";
                            tipoColumna = "Texto";
                            texto = Convert.ToString(item["Clasificación duplicados"]);
                            if (!string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10) { obj.posible_duplicado = texto; } else { msgError = "El texto de la columna no puede contener más de 10 caracteres. "; break; }
                            }

                            result.Add(obj);
                        }
                        else
                        {
                            MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                            MsgRes.DescriptionResponse = "Error en la fila " + fila.ToString() + " columna [" + columna + "]: El valor de la columna no puede se vacio o nulo.";
                            switch (tipoColumna)
                            {
                                case "Númerico":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                    break;
                                case "Texto":
                                    MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                    break;
                                case "Fecha":
                                    MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                    break;
                            }
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }

                    if (string.IsNullOrEmpty(msgError))
                    {
                        BusClass.InsertarCargueMasivoRipsDepuradosAN(result, ref MsgRes);
                        if (MsgRes.ResponseType == ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error)
                        {
                            EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                        }
                    }
                    else
                    {
                        MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                        MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + msgError;
                        switch (tipoColumna)
                        {
                            case "Númerico":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                                break;
                            case "Texto":
                                MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                                break;
                            case "Fecha":
                                MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                                break;
                        }
                        EliminarRipsDepuradosCargueBasePorId(idCargueBase);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error en la fila " + fila.ToString() + " Columna [" + columna + "] " + ex.Message;
                switch (tipoColumna)
                {
                    case "Númerico":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es numérico.]";
                        break;
                    case "Texto":
                        MsgRes.DescriptionResponse += " [Recuerde que el tipo de dato de esta columna es alfanumérico.]";
                        break;
                    case "Fecha":
                        MsgRes.DescriptionResponse += " [Recuerde que en esta columna se deben ingresar fechas.]";
                        break;
                }
                EliminarRipsDepuradosCargueBasePorId(idCargueBase);
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 2 de mayo de 2023
        /// Metodo Para eliminar el cargue base de un registro de cargue rips depurados mediante su ID
        /// </summary>
        /// <param name="idCargueBase"></param>
        public void EliminarRipsDepuradosCargueBasePorId(int idCargueBase)
        {
            BusClass.EliminarRipsDepuradosCargueBasePorId(idCargueBase);
        }

        #endregion

    }
}