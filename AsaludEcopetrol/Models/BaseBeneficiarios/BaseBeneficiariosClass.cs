using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using AsaludEcopetrol.Helpers;
using ECOPETROL_COMMON.ENUM;
using AsaludEcopetrol.BussinesManager;
using Facede;

namespace AsaludEcopetrol.Models.BaseBeneficiarios
{
    public class BaseBeneficiariosClass
    {
        #region  PROPIEDADES

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
        Facade BusClass = new Facade();


        #endregion
        public int id_base_beneficiarios { get; set; }
        public string cod_personal { get; set; }
        public int dependencia { get; set; }
        public string descripcion_depen { get; set; }
        public int distrito { get; set; }
        public string descripcion_distrito { get; set; }
        public string regsitro { get; set; }
        public int historia_clinica { get; set; }
        public string Apellidos { get; set; }
        public string Nombre { get; set; }
        public string Clase_de_identificacion { get; set; }
        public string Numero_identificacion { get; set; }
        public string genero { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string edad { get; set; }
        public string grupo_etareo { get; set; }
        public string deparrtamento { get; set; }
        public string cod_ciu { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string ciudad_medico { get; set; }
        public string descripcion_ciudad { get; set; }
        public string regional { get; set; }
        public string unis { get; set; }
        public string estado_civil { get; set; }
        public string nivel_estudio { get; set; }
        public int nomina { get; set; }
        public string descripcion_nomina { get; set; }
        public string contrato { get; set; }
        public string descripcion_contrato { get; set; }
        public string centro_costo { get; set; }
        public string codifo_funcion_miembro { get; set; }
        public string descrip_funcion_denomi { get; set; }
        public DateTime fecha_vin_o_incrip { get; set; }
        public DateTime inicio_medico { get; set; }
        public DateTime terminacion_medico { get; set; }
        public DateTime ingreso_ECP { get; set; }
        public string estado { get; set; }
        public string costo { get; set; }
        public string direccion { get; set; }
        public string reg_personal { get; set; }
        public string sub_personal { get; set; }
        public string tipo_salud { get; set; }
        public string tipo1 { get; set; }
        public string tipo2 { get; set; }
        public int contador { get; set; }
        public int mes { get; set; }
        public int año { get; set; }
        public DateTime fecha_ingreso_cargue { get; set; }

        public int ExcelAPrefacturasReaprobacion(DataTable dt, int mes, int año, ref MessageResponseOBJ MsgRes)
        {
            var insercionReaprobacion = 0;
            List<base_beneficiarios> listado = new List<base_beneficiarios>();
            List<base_beneficiarios_vip> vip = new List<base_beneficiarios_vip>();

            try
            {
                vip = BusClass.listadoBBVIP();

                string columna = "";
                int fila = 1;
                var textError = "";
                DateTime Periodo = new DateTime(año, mes, 01);

                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        base_beneficiarios obj = new base_beneficiarios();
                        fila++;

                        if (!string.IsNullOrEmpty(item["Código personal"].ToString()))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            obj.periodo = Periodo;
                            obj.mes = mes;
                            obj.año = año;
                            obj.usuario_digita = SesionVar.UserName;

                            columna = "Código personal";
                            texto = Convert.ToString(item["Código personal"]);
                            if (texto.Length <= 50)
                            {
                                obj.cod_personal = Convert.ToString(item["Código personal"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Dependencia";
                            texto = Convert.ToString(item["Dependencia"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 9)
                                {
                                    obj.dependencia = Convert.ToInt32(item["Dependencia"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción dependencia";
                            texto = Convert.ToString(item["Descripción dependencia"]);
                            if (texto.Length <= 150)
                            {
                                obj.descripcion_depen = Convert.ToString(item["Descripción dependencia"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 150 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Distrito";
                            texto = Convert.ToString(item["Distrito"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 9)
                                {
                                    obj.distrito = Convert.ToInt32(item["Distrito"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción distrito";
                            texto = Convert.ToString(item["Descripción distrito"]);
                            if (texto.Length <= 200)
                            {
                                obj.descripcion_distrito = Convert.ToString(item["Descripción distrito"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Registro";
                            texto = Convert.ToString(item["Registro"]);
                            if (texto.Length <= 250)
                            {
                                obj.regsitro = Convert.ToString(item["Registro"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 250 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Historia clinica";
                            texto = Convert.ToString(item["Historia clinica"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 9)
                                {
                                    obj.historia_clinica = Convert.ToInt32(item["Historia clinica"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Apellidos";
                            texto = Convert.ToString(item["Apellidos"]);
                            if (texto.Length <= 100)
                            {
                                obj.Apellidos = Convert.ToString(item["Apellidos"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre";
                            texto = Convert.ToString(item["Nombre"]);
                            if (texto.Length <= 100)
                            {
                                obj.Nombre = Convert.ToString(item["Nombre"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Clase identificación";
                            texto = Convert.ToString(item["Clase identificación"]);
                            if (texto.Length <= 50)
                            {
                                obj.Clase_de_identificacion = Convert.ToString(item["Clase identificación"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Número identificación";
                            texto = Convert.ToString(item["Número identificación"]);
                            if (texto.Length <= 100)
                            {
                                obj.Numero_identificacion = Convert.ToString(item["Número identificación"]).ToUpper();

                                base_beneficiarios_vip vipIndi = vip.FirstOrDefault(x => x.documento == obj.Numero_identificacion);
                                if (vipIndi != null)
                                {
                                    obj.vip = 1;
                                }
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Genero";
                            texto = Convert.ToString(item["Genero"]);
                            if (texto.Length <= 50)
                            {
                                obj.genero = Convert.ToString(item["Genero"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha nacimiento";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha nacimiento"]);
                                if (fechas != null)
                                {
                                    obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha nacimiento"]);
                                }
                                else
                                {
                                    obj.fecha_nacimiento = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Edad";
                            texto = Convert.ToString(item["Edad"]);
                            if (texto.Length <= 3)
                            {
                                obj.edad = Convert.ToString(item["Edad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 3 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Grupo etareo";
                            texto = Convert.ToString(item["Grupo etareo"]);
                            if (texto.Length <= 50)
                            {
                                obj.grupo_etareo = Convert.ToString(item["Grupo etareo"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Departamento";
                            texto = Convert.ToString(item["Departamento"]);
                            if (texto.Length <= 100)
                            {
                                obj.deparrtamento = Convert.ToString(item["Departamento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Código ciudad";
                            texto = Convert.ToString(item["Código ciudad"]);
                            if (texto.Length <= 50)
                            {
                                obj.cod_ciu = Convert.ToString(item["Código ciudad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Ciudad";
                            texto = Convert.ToString(item["Ciudad"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad = Convert.ToString(item["Ciudad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Telefono";
                            texto = Convert.ToString(item["Telefono"]);
                            if (texto.Length <= 50)
                            {
                                obj.telefono = Convert.ToString(item["Telefono"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Celular";
                            texto = Convert.ToString(item["Celular"]);
                            if (texto.Length <= 50)
                            {
                                obj.celular = Convert.ToString(item["Celular"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Email";
                            texto = Convert.ToString(item["Email"]);
                            if (texto.Length <= 200)
                            {
                                obj.email = Convert.ToString(item["Email"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Ciudad medico";
                            texto = Convert.ToString(item["Ciudad medico"]);
                            if (texto.Length <= 50)
                            {
                                obj.ciudad_medico = Convert.ToString(item["Ciudad medico"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción ciudad";
                            texto = Convert.ToString(item["Descripción ciudad"]);
                            if (texto.Length <= 100)
                            {
                                obj.descripcion_ciudad = Convert.ToString(item["Descripción ciudad"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Regional";
                            texto = Convert.ToString(item["Regional"]);
                            if (texto.Length <= 50)
                            {
                                obj.regional = Convert.ToString(item["Regional"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
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
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Estado civil";
                            texto = Convert.ToString(item["Estado civil"]);
                            if (texto.Length <= 50)
                            {
                                obj.estado_civil = Convert.ToString(item["Estado civil"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nivel estudio";
                            texto = Convert.ToString(item["Nivel estudio"]);
                            if (texto.Length <= 10)
                            {
                                obj.nivel_estudio = Convert.ToString(item["Nivel estudio"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 10 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nomina";
                            texto = Convert.ToString(item["Nomina"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 9)
                                {
                                    obj.nomina = Convert.ToInt32(item["Nomina"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción nomina";
                            texto = Convert.ToString(item["Descripción nomina"]);
                            if (texto.Length <= 100)
                            {
                                obj.descripcion_nomina = Convert.ToString(item["Descripción nomina"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Contrato";
                            texto = Convert.ToString(item["Contrato"]);
                            if (texto.Length <= 50)
                            {
                                obj.contrato = Convert.ToString(item["Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción contrato";
                            texto = Convert.ToString(item["Descripción contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.descripcion_contrato = Convert.ToString(item["Descripción contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Centro costo";
                            texto = Convert.ToString(item["Centro costo"]);
                            if (texto.Length <= 50)
                            {
                                obj.centro_costo = Convert.ToString(item["Centro costo"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Código función miembro";
                            texto = Convert.ToString(item["Código función miembro"]);
                            if (texto.Length <= 50)
                            {
                                obj.codifo_funcion_miembro = Convert.ToString(item["Código función miembro"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Descripción función nomina";
                            texto = Convert.ToString(item["Descripción función nomina"]);
                            if (texto.Length <= 250)
                            {
                                obj.descrip_funcion_denomi = Convert.ToString(item["Descripción función nomina"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 250 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha vinculación o inscripción";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha vinculación o inscripción"]);
                                if (fechas != null)
                                {
                                    obj.fecha_vin_o_incrip = Convert.ToDateTime(item["Fecha vinculación o inscripción"]);
                                }
                                else
                                {
                                    obj.fecha_vin_o_incrip = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Inicio medico";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Inicio medico"]);
                                if (fechas != null)
                                {
                                    obj.inicio_medico = Convert.ToDateTime(item["Inicio medico"]);
                                }
                                else
                                {
                                    obj.inicio_medico = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Terminación medico";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Terminación medico"]);
                                if (fechas != null)
                                {
                                    obj.terminacion_medico = Convert.ToDateTime(item["Terminación medico"]);
                                }
                                else
                                {
                                    obj.terminacion_medico = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Ingreso ECP";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Ingreso ECP"]);
                                if (fechas != null)
                                {
                                    obj.ingreso_ECP = Convert.ToDateTime(item["Ingreso ECP"]);
                                }
                                else
                                {
                                    obj.ingreso_ECP = new DateTime(1900, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", formato incorrecto.";
                                throw new Exception(textError);
                            }

                            columna = "Estado";
                            texto = Convert.ToString(item["Estado"]);
                            if (texto.Length <= 50)
                            {
                                obj.estado = Convert.ToString(item["Estado"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Costo";
                            texto = Convert.ToString(item["Costo"]);
                            if (texto.Length <= 50)
                            {
                                obj.costo = Convert.ToString(item["Costo"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Dirección";
                            texto = Convert.ToString(item["Dirección"]);
                            if (texto.Length <= 250)
                            {
                                obj.direccion = Convert.ToString(item["Dirección"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 250 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Reg personal";
                            texto = Convert.ToString(item["Reg personal"]);
                            if (texto.Length <= 50)
                            {
                                obj.reg_personal = Convert.ToString(item["Reg personal"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Sub personal";
                            texto = Convert.ToString(item["Sub personal"]);
                            if (texto.Length <= 50)
                            {
                                obj.sub_personal = Convert.ToString(item["Sub personal"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo salud";
                            texto = Convert.ToString(item["Tipo salud"]);
                            if (texto.Length <= 50)
                            {
                                obj.tipo_salud = Convert.ToString(item["Tipo salud"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo 1";
                            texto = Convert.ToString(item["Tipo 1"]);
                            if (texto.Length <= 100)
                            {
                                obj.tipo1 = Convert.ToString(item["Tipo 1"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Tipo 2";
                            texto = Convert.ToString(item["Tipo 2"]);
                            if (texto.Length <= 100)
                            {
                                obj.tipo2 = Convert.ToString(item["Tipo 2"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Contador";
                            texto = Convert.ToString(item["Contador"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 9)
                                {
                                    obj.contador = Convert.ToInt32(item["Contador"]);
                                }
                                else
                                {
                                    textError = columna + ", sólo debe contener máximo 9 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Mega nombre";
                            texto = Convert.ToString(item["Mega nombre"]);
                            if (texto.Length <= 200)
                            {
                                obj.mega_nombre = Convert.ToString(item["Mega nombre"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Mega documento";
                            texto = Convert.ToString(item["Mega documento"]);
                            if (texto.Length <= 200)
                            {
                                obj.mega_documento = Convert.ToString(item["Mega documento"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", sólo debe contener máximo 200 caracteres.";
                                throw new Exception(textError);
                            }

                            obj.fecha_ingreso_cargue = DateTime.Now;

                            listado.Add(obj);
                            obj = new base_beneficiarios();
                        }
                    }

                    insercionReaprobacion = BusClass.InsertarBaseBeneficiariosMasivo(listado, ref MsgRes);
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


        public Int32 ExcelCorreosPPE(DataTable dt2, ref MessageResponseOBJ MsgRes)
        {

            Int32 IntContador = 2;
            var columna = 0;
            var mensaje = "";
            var variable = "";

            List<ecop_directorioPPE_correos> OBJDetalle = new List<ecop_directorioPPE_correos>();
            try
            {
                foreach (DataRow item in dt2.Rows)
                {

                    if (IntContador == 173)
                    {
                        var prueba = "";
                    }
                    var valida = Convert.ToString(item["Regional"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();


                    if (valida != null && valida.Length > 0)
                    {
                        ecop_directorioPPE_correos ppe = new ecop_directorioPPE_correos();

                        columna = 1;
                        variable = Convert.ToString(item["Regional"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 10)
                        {
                            ppe.regional = Convert.ToString(item["Regional"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 10 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 2;
                        variable = Convert.ToString(item["Unis"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 50)
                        {
                            ppe.unis = Convert.ToString(item["Unis"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 50 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 3;
                        variable = Convert.ToString(item["Localidad"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 50)
                        {
                            ppe.localidad = Convert.ToString(item["Localidad"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 50 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 4;
                        variable = Convert.ToString(item["IPS integradora /Mega /Mepa / Oga"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 50)
                        {
                            ppe.ips_integrada = Convert.ToString(item["IPS integradora /Mega /Mepa / Oga"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 50 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 5;
                        variable = Convert.ToString(item["Clasifique el tipo de PPE  (MEGA, MEPA, OGA, OESPAS) Para el caso de las IPS Integradoras"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 100)
                        {
                            ppe.clasifique_ppe = Convert.ToString(item["Clasifique el tipo de PPE  (MEGA, MEPA, OGA, OESPAS) Para el caso de las IPS Integradoras"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 100 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 6;
                        variable = Convert.ToString(item["Nombre IPS integradora (En caso de que aplique)"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 100)
                        {
                            ppe.nombre_ipsIntegrada = Convert.ToString(item["Nombre IPS integradora (En caso de que aplique)"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 100 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 7;
                        variable = Convert.ToString(item["Nit del profesional (mega / mepa / oga)."]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        if (variable.Length > 1 && variable != "" && variable != null)
                        {
                            if (variable.Length <= 50)
                            {
                                ppe.documento_profesional = Convert.ToString(item["Nit del profesional (mega / mepa / oga)."]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                            }
                            else
                            {
                                mensaje = columna + " debe contener maximo 50 caracteres";
                                throw new Exception(mensaje);
                            }
                        }
                        else
                        {
                            mensaje = columna + " es un campo obligatorio.";
                            throw new Exception(mensaje);
                        }

                        columna = 8;
                        variable = Convert.ToString(item["Nombre del profesional (mega / mepa / oga)."]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        if (variable.Length > 1 && variable != "" && variable != null)
                        {
                            if (variable.Length <= 100)
                            {
                                ppe.nombre_profesional = Convert.ToString(item["Nombre del profesional (mega / mepa / oga)."]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                            }
                            else
                            {
                                mensaje = columna + " debe contener maximo 100 caracteres";
                                throw new Exception(mensaje);
                            }
                        }
                        else
                        {
                            mensaje = columna + " es un campo obligatorio.";
                            throw new Exception(mensaje);
                        }

                        columna = 9;
                        variable = Convert.ToString(item["Correo electrónico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        if (variable.Length > 1 && variable != "" && variable != null)
                        {
                            if (variable.Length <= 500)
                            {
                                ppe.correo = Convert.ToString(item["Correo electrónico"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                            }
                            else
                            {
                                mensaje = columna + " debe contener maximo 500 caracteres";
                                throw new Exception(mensaje);
                            }
                        }
                        else
                        {
                            mensaje = columna + " es un campo obligatorio.";
                            throw new Exception(mensaje);
                        }


                        columna = 10;
                        variable = Convert.ToString(item["Número de contacto"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 200)
                        {
                            ppe.numero_contacto = Convert.ToString(item["Número de contacto"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 200 caracteres";
                            throw new Exception(mensaje);
                        }


                        columna = 11;
                        variable = Convert.ToString(item["¿El PPE realiza notificación al SIVIGILA?"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 5)
                        {
                            ppe.PPE_realizaVigilancia = Convert.ToString(item["¿El PPE realiza notificación al SIVIGILA?"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 5 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 12;
                        variable = Convert.ToString(item["Tiene ficha de caracterización como Unidad Primaria Generadora de Datos - UPGD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();

                        if (variable.Length <= 5)
                        {
                            ppe.tieneFicha_caracterizacion_unidadPrimaria = Convert.ToString(item["Tiene ficha de caracterización como Unidad Primaria Generadora de Datos - UPGD"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 5 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 13;
                        variable = Convert.ToString(item["Observaciones"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 1000)
                        {
                            ppe.observaciones = Convert.ToString(item["Observaciones"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 1000 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 14;
                        variable = Convert.ToString(item["Mega nombre"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length <= 200)
                        {
                            ppe.mega_nombre = Convert.ToString(item["Mega nombre"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        }
                        else
                        {
                            mensaje = columna + " debe contener maximo 200 caracteres";
                            throw new Exception(mensaje);
                        }

                        columna = 15;
                        variable = Convert.ToString(item["Mega correo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                        if (variable.Length > 1 && variable != "" && variable != null)
                        {
                            if (variable.Length <= 200)
                            {
                                ppe.mega_correo = Convert.ToString(item["Mega correo"]).Replace(StringHelper.STR_LETRAÑ, StringHelper.STR_LETRA1).Replace(StringHelper.STR_TILDE, String.Empty).ToUpper();
                            }
                            else
                            {
                                mensaje = columna + " debe contener maximo 200 caracteres";
                                throw new Exception(mensaje);
                            }
                        }
                        else
                        {
                            mensaje = columna + " es un campo obligatorio.";
                            throw new Exception(mensaje);
                        }

                        ppe.fecha_digita = DateTime.Now;
                        ppe.usuario_digita = SesionVar.UserName;

                        OBJDetalle.Add(ppe);
                        ppe = new ecop_directorioPPE_correos();
                        IntContador = IntContador + 1;
                    }
                }
                var retorno = BusClass.CargueCorreosPPE(OBJDetalle, ref MsgRes);

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                return retorno;
            }
            catch (Exception ex)
            {
                if (mensaje != "" && mensaje != null)
                {
                    MsgRes.DescriptionResponse = "Error en la columna: " + mensaje + " Fila: " + IntContador;
                }
                else
                {
                    MsgRes.DescriptionResponse = "Error en la  columna: " + columna + " Fila: " + IntContador;
                }
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.CodeError = ex.Message;

                throw;
            }
        }

        public int eliminarCorreosPPE(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.eliminarCorreosPPE(ref MsgRes);
        }

    }
}