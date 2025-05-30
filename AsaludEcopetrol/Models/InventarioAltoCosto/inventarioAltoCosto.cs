using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;
using LinqToExcel;
using Org.BouncyCastle.Asn1.Ocsp;

namespace AsaludEcopetrol.Models.InventarioAltoCosto
{

    public class inventarioAltoCosto
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

        //Variables
        public int id_gestion { get; set; }
        public int id_inventario { get; set; }
        public int id_detalle { get; set; }
        public DateTime fecha_diagnostico { get; set; }
        public DateTime fecha_remision { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string tipo_estudio { get; set; }
        public string motivo_no_diagnostico_usuario { get; set; }
        public DateTime fecha_recoleccion_muestra { get; set; }
        public DateTime fecha_primer_informeHispatologico { get; set; }
        public string codigo_habillitacion_ips { get; set; }
        public DateTime fecha_consulta_medicoTratante { get; set; }
        public string histologia_tumor { get; set; }
        public string grado_diferenciacion_tumor { get; set; }
        public string estadificacion_tnm_figo { get; set; }
        public DateTime fecha_estadificacion { get; set; }
        public string se_realiza_her1 { get; set; }
        public DateTime fecha_realizacion_her1 { get; set; }
        public string resultado_prueba_her1 { get; set; }
        public string estadificacion_dukes { get; set; }
        public DateTime fecha_estadificacion_dukes { get; set; }
        public string estadificacion_hodkin { get; set; }
        public string clasificacion_escala_gleason { get; set; }
        public string clasificacion_riesgo_leucemialinfoma { get; set; }
        public DateTime fecha_clasificacion_riesgo { get; set; }
        public string objetivo_tratamiento_inicial { get; set; }
        public string objetivo_intervencion_periodo_prueba { get; set; }
        public string antecedentes_otro_cancer_primario { get; set; }
        public DateTime fecha_otro_cancer_primario { get; set; }
        public string tipo_cie10_otro_cancer { get; set; }
        public string usuario_recibio_quimioterapia { get; set; }
        public string cuantas_quimioterapias_recibioUsuario { get; set; }
        public string usuario_recibio_citorreduccion { get; set; }
        public string usuario_recibio_induccion { get; set; }
        public string usuario_recibio_intensificacion { get; set; }
        public string usuario_recibio_consolidacion { get; set; }
        public string usuario_recibio_reinduccion { get; set; }
        public string usuario_recibio_mantenimiento { get; set; }
        public string usuario_recibio_mantenimiento_largo { get; set; }
        public string usuario_recibio_otra_fase { get; set; }
        public string numero_ciclos_iniciados { get; set; }
        public string ubicacion_temporal_esquema_quimioterapia { get; set; }
        public DateTime fechaInicio_temporal_esquema_quimioterapia1 { get; set; }
        public string numero_ips { get; set; }
        public string codigo_ips1 { get; set; }
        public string codigo_ips2 { get; set; }
        public string cuantos_medicamentos_neoplasicos { get; set; }
        public string medicamento_antineoplasico_suministrado_1 { get; set; }
        public string medicamento_antineoplasico_suministrado_2 { get; set; }
        public string medicamento_antineoplasico_suministrado_3 { get; set; }
        public string medicamento_antineoplasico_suministrado_4 { get; set; }
        public string medicamento_antineoplasico_suministrado_5 { get; set; }
        public string recibio_ciclosporina { get; set; }
        public string medicamento_antineoplasico_suministrado_7 { get; set; }
        public string medicamento_antineoplasico_suministrado_8 { get; set; }
        public string medicamento_antineoplasico_suministrado_9 { get; set; }
        public string medicamento_antineoplasico_suministrado_adicional_1 { get; set; }
        public string medicamento_antineoplasico_suministrado_adicional_2 { get; set; }
        public string medicamento_antineoplasico_suministrado_adicional_3 { get; set; }
        public string recibio_quimioterapia_intratecal { get; set; }
        public DateTime fecha_finalizacion_periodo_reporte { get; set; }
        public string caracteristicas_actuales_esquema_periodo { get; set; }
        public string motivo_finalizacion_prematura { get; set; }
        public string ubicacion_temporal_ultimoesquema { get; set; }
        public DateTime fecha_inicio_ultimo_esquema { get; set; }
        public string numero_ips_suministra_ultimoesquema { get; set; }
        public string codigo_ips_suministra_ultimoesquema_1 { get; set; }
        public string codigo_ips_suministra_ultimoesquema_2 { get; set; }
        public string cuantos_tratamientos_antineoplasicos_propusieron { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_1 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_2 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_3 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_4 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_5 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_6 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_7 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_8 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_9 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_adicional_1 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_adicional_2 { get; set; }
        public string antineoplasico_administradoUsuario_ultimoesquema_adicional_3 { get; set; }
        public string recibio_terapiaIntratecal_ultimoperiodo { get; set; }
        public DateTime fecha_finalizacionUltimoEsquema_quimioterapia_terapiasistemica { get; set; }
        public string caracteristicas_actuales_ultimoPeriodo { get; set; }
        public string motivo_fializacionPrematura_ultimoEsquema { get; set; }
        public string usuario_sometidoa_cirugias_paliativasCurativas { get; set; }
        public string numeroCirugias_sometidoUsuario_ultimo { get; set; }
        public DateTime fecha_realizacionprimeracirugia_periodoreporte { get; set; }
        public string codigo_ips_realizo_primercirugia { get; set; }
        public string codigo_primeracirugia_periodoactual { get; set; }
        public string ubicaciontemporal_manejo_oncologico_primeraCirugia { get; set; }
        public DateTime fecharealizacion_ultimacirugia_reintervencion { get; set; }
        public string motivo_ultimacirugia { get; set; }
        public string codigo_ips_realizo_ultimacirugia { get; set; }
        public string codigo_ultimacirugia_periodoactual { get; set; }
        public string ubicaciontemporal_manejooncologico_ultimacirugia { get; set; }
        public string estadovital_finalizarultimacirugia { get; set; }
        public string usuariorecibio_radioterapia_periodoreporte_actual { get; set; }
        public string numero_sesionesradioterapia_recibidas_periodo { get; set; }
        public DateTime fechaInicio_primerounicoesquema_tiporadioterapia { get; set; }
        public string ubicaciontemporal_primerunico_esquemaradioterapia_tratamientoOncologico { get; set; }
        public string tipo_radioterapia_aplicada_primeresquema { get; set; }
        public string numeroips_suministranprimeresquema_radioterapia { get; set; }
        public string codigo_ips1_suministraprimeresquema_radioterapia_1 { get; set; }
        public string codigo_ips1_suministraprimeresquema_radioterapia_2 { get; set; }
        public DateTime fecha_finalizacion_primeresquema_radioterapia { get; set; }
        public string caracteristicas_actuales_primeresquema_radioterapia { get; set; }
        public string motivo_fianlizacionprimeresquema_radioterapia { get; set; }
        public DateTime fechaInicio_ultimounicoesquema_tiporadioterapia { get; set; }
        public string ubicaciontemporal_ultimounico_esquemaradioterapia_tratamientoOncologico { get; set; }
        public string tipo_radioterapia_aplicada_ultimoesquema { get; set; }
        public string numeroips_suministranultimoesquema_radioterapia { get; set; }
        public string codigo_ips1_suministraultimoesquema_radioterapia_1 { get; set; }
        public string codigo_ips1_suministraultimoesquema_radioterapia_2 { get; set; }
        public DateTime fecha_finalizacion_ultimoesquema_radioterapia { get; set; }
        public string caracteristicasactuales_ultimoesquema_radioterapia { get; set; }
        public string motivo_fianlizacionultimoesquema_radioterapia { get; set; }
        public string recibiousuario_transplantecelulas_progenitorashematopoyetica_periodoactual { get; set; }
        public string tipo_transplante_recibido { get; set; }
        public string ubicacion_temporal_transplante_relaciononcologico { get; set; }
        public DateTime fecha_transplante { get; set; }
        public string codigo_ips_realizo_transplante { get; set; }
        public string usuario_recibiocirugia_reconstructiva_periodoactual { get; set; }
        public DateTime fecha_cirugia_reconstructiva { get; set; }
        public string codigo_ips_realizocirugia_reconstructiva { get; set; }
        public string usuario_valoradoconsulta_procedimientocuidado_paliativo { get; set; }
        public string usuario_valoradoconsulta_procedimientocuidado_paliativo_medicoespecialista { get; set; }
        public string usuario_valoradoconsulta_procedimientocuidado_paliativo_nomedico { get; set; }
        public string usuario_valoradoconsulta_procedimientocuidado_paliativo_otraespecialidad { get; set; }
        public string usuario_recibioconsulta_procedimiento_cuidadopaliativo_medicogeneral { get; set; }
        public string usuario_recibioconsulta_procedimiento_cuidadopaliativo_trabajosocial { get; set; }
        public string usuario_recibioconsulta_procedimiento_cuidadopaliativo_otroprofesional { get; set; }
        public DateTime fecha_primeraconsulta_procedimientocuidado_paliativo { get; set; }
        public string codigo_ips__recibeatencion_cuidadopaliativo { get; set; }
        public string usuario_valoradoservicio_psiquiatria { get; set; }
        public DateTime fecha_primeraconsulta_serviciopsiquiatria { get; set; }
        public string codigo_ipsdonde_recibeprimeravaloracion_psiquiatria { get; set; }
        public string usuario_valoradoporprofesional_120_nutricion { get; set; }
        public DateTime fecha_consultaInicial_nutricion { get; set; }
        public string codigo_ips_recibevaloracion_nutricion { get; set; }
        public string usuario_recibiosoporte_nutricional { get; set; }
        public string usuario_recibeterapias_complementarias_rehabilitacion { get; set; }
        public string tipo_tratamiento_estarecibiendo_usuario { get; set; }
        public string resultadofinal_resultadooncologico_usuarioesta { get; set; }
        public string estadovital_periodoreporte { get; set; }
        public string novedadadministrativa_usuariorespectoreporteanterior { get; set; }
        public string novedadclinica_usuario_afechacorte { get; set; }
        public DateTime fecha_desafiliacion_EAPB { get; set; }
        public DateTime fecha_muerte { get; set; }
        public string causa_muerte { get; set; }
        public string codigo_unico_identificacion_BDUA_BDEX_PVS { get; set; }
        public DateTime fecha_corte { get; set; }
        public int cerrado { get; set; }
        public string observaciones { get; set; }
        public int auditor_asignado { get; set; }
        public string regional { get; set; }
        public DateTime fecha_gestion { get; set; }
        public string usuario_digita { get; set; }

        //Fin variables

        #endregion

        #region FUNCIONES

        //Funciones

        public int estructuraExcelCargueInventarioAltoCosto(string rutaArchivo, inventario_altoCosto_carguebase dato, ref MessageResponseOBJ MsgRes)
        {
            List<inventario_altoCosto_detalle> Listado = new List<inventario_altoCosto_detalle>();
            var book = new ExcelQueryFactory(rutaArchivo);
            var RtaInsercion = 0;
            int fila = 1;
            string columna = "";

            try
            {
                var lecturaDato = (from c in book.WorksheetRange("A1", "Z999999", "Cargue") select c).ToList();
                var textError = "";

                try
                {
                    for (var i = 0; i < lecturaDato.Count(); i++)
                    {
                        inventario_altoCosto_detalle obj = new inventario_altoCosto_detalle();
                        var item = lecturaDato[i];
                        fila++;
                        if (item[0] != null && item[0] != "")
                        {
                            var texto = "";

                            columna = "Ccagrupador";
                            texto = item["Ccagrupador"];
                            if (texto.Length <= 50)
                            {
                                obj.ccagrupador = Convert.ToString(item["Ccagrupador"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Requerido - obligatorio";
                            texto = item["Requerido - obligatorio"];
                            if (texto.Length <= 2)
                            {
                                obj.requerido_obligatorio = Convert.ToString(item["Requerido - obligatorio"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 2 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Tipo de caso";
                            texto = item["Tipo de caso"];
                            if (texto.Length <= 50)
                            {
                                obj.tipo_caso = Convert.ToString(item["Tipo de caso"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Coordinación";
                            texto = item["Coordinación"];
                            if (texto.Length <= 100)
                            {
                                obj.coordinacion = Convert.ToString(item["Coordinación"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Activo población";
                            texto = item["Activo población"];
                            if (texto.Length <= 50)
                            {
                                obj.activo_poblacion = Convert.ToString(item["Activo población"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Fecha fallecimiento";
                            texto = item["Fecha fallecimiento"];
                            try
                            {
                                if (texto != "" && texto != null)
                                {
                                    obj.fecha_fallecimiento = Convert.ToDateTime(item["Fecha fallecimiento"]);
                                }
                                else
                                {
                                    //textError = columna + ", solo puede contener fechas.";
                                    //throw new Exception(textError);
                                    obj.fecha_fallecimiento = new DateTime(1845, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                obj.fecha_fallecimiento = new DateTime(1845, 01, 01);
                            }


                            columna = "Causa fallecimiento";
                            texto = item["Causa fallecimiento"];
                            if (texto.Length <= 100)
                            {
                                obj.causa_fallecimiento = Convert.ToString(item["Causa fallecimiento"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Fecha desafiliación";
                            texto = item["Fecha desafiliación"];
                            try
                            {
                                if (texto != "" && texto != null)
                                {
                                    obj.fecha_desafiliacion = Convert.ToDateTime(item["Fecha desafiliación"]);
                                }
                                else
                                {
                                    //textError = columna + ", solo puede contener fechas.";
                                    //throw new Exception(textError);
                                    obj.fecha_desafiliacion = new DateTime(1845, 01, 01);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                obj.fecha_desafiliacion = new DateTime(1845, 01, 01);
                            }

                            columna = "Primer nombre del usuario";
                            texto = item["Primer nombre del usuario"];
                            if (texto.Length <= 50)
                            {
                                obj.primer_nombre_usuario = Convert.ToString(item["Primer nombre del usuario"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Segundo nombre del usuario";
                            texto = item["Segundo nombre del usuario"];
                            if (texto.Length <= 50)
                            {
                                obj.segundo_nombre_usuario = Convert.ToString(item["Segundo nombre del usuario"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Primer apellido del usuario";
                            texto = item["Primer apellido del usuario"];
                            if (texto.Length <= 50)
                            {
                                obj.primer_apellido_usuario = Convert.ToString(item["Primer apellido del usuario"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Segundo apellido del usuario";
                            texto = item["Segundo apellido del usuario"];
                            if (texto.Length <= 50)
                            {
                                obj.segundo_apellido_usuario = Convert.ToString(item["Segundo apellido del usuario"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }


                            columna = "Tipo de Identificación del usuario";
                            texto = item["Tipo de Identificación del usuario"];
                            if (texto.Length <= 5)
                            {
                                obj.tipo_identificacion_usuario = Convert.ToString(item["Tipo de Identificación del usuario"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 5 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Número de identificación del usuario";
                            texto = item["Número de identificación del usuario"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 50)
                                {
                                    obj.numero_identificacion_usuario = Convert.ToString(item["Número de identificación del usuario"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 50 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }


                            columna = "Fecha de nacimiento";
                            texto = item["Fecha de nacimiento"];
                            try
                            {
                                if (texto != "" && texto != null)
                                {
                                    obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha de nacimiento"]);
                                }
                                else
                                {
                                    obj.fecha_nacimiento = new DateTime(1845, 01, 01);
                                    //textError = columna + ", solo puede contener fechas.";
                                    //throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                obj.fecha_nacimiento = new DateTime(1845, 01, 01);
                            }

                            columna = "Sexo";
                            texto = item["Sexo"];
                            if (texto.Length <= 5)
                            {
                                obj.sexo = Convert.ToString(item["Sexo"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 1 carácter(M-F).";
                                throw new Exception(textError);
                            }

                            columna = "Ocupación";
                            texto = item["Ocupación"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 4)
                                {
                                    obj.ocupacion = Convert.ToInt32(item["Ocupación"]);
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

                            columna = "Régimen de afiliación AL SGSSS";
                            texto = item["Régimen de afiliación AL SGSSS"];
                            if (texto.Length <= 20)
                            {
                                obj.regimen_afiliacion = Convert.ToString(item["Régimen de afiliación AL SGSSS"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 20 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Código de la EAPB o de la entidad territorial";
                            texto = item["Código de la EAPB o de la entidad territorial"];
                            if (texto.Length <= 50)
                            {
                                obj.codigo_EAPB = Convert.ToString(item["Código de la EAPB o de la entidad territorial"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Código pertenencia étnica";
                            texto = item["Código pertenencia étnica"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 4)
                                {
                                    obj.codigo_pertenencia_etnica = Convert.ToInt32(item["Código pertenencia étnica"]);
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

                            columna = "Grupo poblacional";
                            texto = item["Grupo poblacional"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 4)
                                {
                                    obj.grupo_poblacional = Convert.ToInt32(item["Grupo poblacional"]);
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

                            columna = "Municipio de residencia";
                            texto = item["Municipio de residencia"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 6)
                                {
                                    obj.municipio_residencia = Convert.ToInt32(item["Municipio de residencia"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 6 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Número telefónico del paciente";
                            texto = item["Número telefónico del paciente"];
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 20)
                                {
                                    obj.numero_telefonico_paciente = Convert.ToString(item["Número telefónico del paciente"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 20 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números";
                                throw new Exception(textError);
                            }

                            columna = "Fecha de afiliación a la EAPB que reporta";
                            texto = item["Fecha de afiliación a la EAPB que reporta"];
                            try
                            {
                                if (texto != "" && texto != null)
                                {
                                    obj.fecha_afiliacion_EAPB = Convert.ToDateTime(item["Fecha de afiliación a la EAPB que reporta"]);
                                }
                                else
                                {
                                    obj.fecha_nacimiento = new DateTime(1845, 01, 01);
                                    //textError = columna + ", solo puede contener fechas.";
                                    //throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.Message;
                                obj.fecha_nacimiento = new DateTime(1845, 01, 01);
                            }

                            columna = "Código CIE - 10 de la neoplasia";
                            texto = item["Código CIE - 10 de la neoplasia"];
                            if (texto.Length <= 50)
                            {
                                obj.cod_cie10_neoplasia = Convert.ToString(item["Código CIE - 10 de la neoplasia"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Usuario auditor asignado";
                            texto = item["Usuario auditor asignado"];
                            if (texto.Length <= 50)
                            {
                                obj.usuario_auditor_asignado = Convert.ToString(item["Usuario auditor asignado"]);
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            Listado.Add(obj);
                            obj = new inventario_altoCosto_detalle();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.insercionMasivaAltoCosto(dato, Listado, ref MsgRes);
                    }
                    else
                    {
                        textError = "El cargue no puede quedar vacío.";
                        throw new Exception(textError);
                    }
                }

                catch (Exception ex)
                {
                    var error = ex.Message;
                    MsgRes.DescriptionResponse = "Columna: " + error + " Fila: " + fila;
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                var mensaje = "";

                if (error.Contains("Valid worksheet names"))
                {
                    mensaje = "Error de nombre de hoja. El nombre debe ser: Cargue";
                    MsgRes.DescriptionResponse = "Columna: " + mensaje + " Fila: " + fila;
                }
                else
                {
                    MsgRes.DescriptionResponse = "Columna: " + ex.Message + " Fila: " + fila;
                }
            }

            book.Dispose();
            return RtaInsercion;
        }


        public Int32 CargueMasivoPoblacionComprobada(int? tipo, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            Int32 idContadorFinal = 0;
            int ciclos = 0;

            cargue_cuentas_altoCosto_confirmada obj = new cargue_cuentas_altoCosto_confirmada();
            cargue_cuentas_altoCosto obj2 = new cargue_cuentas_altoCosto();
            List<cargue_cuentas_altoCosto_confirmada> OBJDetalle = new List<cargue_cuentas_altoCosto_confirmada>();
            string columna = "";

            obj2.tipo = tipo;
            obj2.usuario_cargue = SesionVar.UserName;
            obj2.fecha_Cargue = DateTime.Now;

            var textError = "";
            var resultado = 0;
            List<ref_cargue_cuentas_altoCosto> tipoCosto = new List<ref_cargue_cuentas_altoCosto>();
            try
            {
                int conteo = dt2.Rows.Count;

                tipoCosto = BusClass.listadoCargueGsdRastreo();

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                id_cargue = BusClass.cargue_cuentas_altoCosto(obj2, ref MsgRes);

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["Tipo de documento de identificación del usuario"].ToString() != "")
                    {   
                        var texto = "";
                        var fecha = new DateTime();

                        obj.id_cargue = id_cargue;

                        columna = "Tipo de documento de identificación del usuario";
                        texto = Convert.ToString(item["Tipo de documento de identificación del usuario"]);
                        if (texto.Length <= 20)
                        {
                            obj.tipo_documento_usuario = Convert.ToString(item["Tipo de documento de identificación del usuario"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Número de documento de identificación del usuario";
                        try
                        {
                            texto = Convert.ToString(item["Número de documento de identificación del usuario"]).ToUpper();
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 20)
                                {
                                    obj.documento_usuario = Convert.ToString(item["Número de documento de identificación del usuario"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }



                        columna = "Primer apellido";
                        texto = Convert.ToString(item["Primer apellido"]);
                        if (texto.Length <= 100)
                        {
                            obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo apellido";
                        texto = Convert.ToString(item["Segundo apellido"]);

                        if (texto.Length <= 100)
                        {
                            obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer nombre";
                        texto = Convert.ToString(item["Primer nombre"]);

                        if (texto.Length <= 100)
                        {
                            obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo nombre";
                        texto = Convert.ToString(item["Segundo nombre"]);

                        if (texto.Length <= 100)
                        {
                            obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Fecha de nacimiento";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha de nacimiento"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha de nacimiento"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;

                            if (error.Contains("No puede ir"))
                            {
                                textError = ex.Message;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Sexo";
                        texto = Convert.ToString(item["Sexo"]);
                        if (texto.Length <= 5)
                        {
                            obj.sexo = Convert.ToString(item["Sexo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Código CIE - 10 población cancer";
                        texto = Convert.ToString(item["Código CIE - 10 población cancer"]);
                        if (texto.Length <= 50)
                        {
                            obj.codigo_cie10 = Convert.ToString(item["Código CIE - 10 población cancer"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Descripcion población cáncer";
                        texto = Convert.ToString(item["Descripcion población cáncer"]);
                        if (texto.Length <= 200)
                        {
                            obj.descripcion_codigo_cie10 = Convert.ToString(item["Descripcion población cáncer"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Fecha de diagnóstico del cáncer reportado";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha de diagnóstico del cáncer reportado"]);
                            if (fecha != null)
                            {
                                obj.fecha_diagnostico_cancer = Convert.ToDateTime(item["Fecha de diagnóstico del cáncer reportado"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("No puede ir"))
                            {
                                textError = ex.Message;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }
                            throw new Exception(textError);
                        }


                        columna = "Documento+Dx cáncer población";
                        texto = Convert.ToString(item["Documento+Dx cáncer población"]);
                        if (texto.Length <= 200)
                        {
                            obj.documento_cancer_poblacion = Convert.ToString(item["Documento+Dx cáncer población"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Paciente unico Si/No población CAC";
                        texto = Convert.ToString(item["Paciente unico Si/No población CAC"]);
                        if (texto.Length <= 5)
                        {
                            obj.paciente_unico = Convert.ToString(item["Paciente unico Si/No población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Fecha nacimiento población CAC";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha nacimiento población CAC"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento_poblacion = Convert.ToDateTime(item["Fecha nacimiento población CAC"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;

                            if (error.Contains("No puede ir"))
                            {
                                textError = ex.Message;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Género población CAC";
                        texto = Convert.ToString(item["Género población CAC"]);
                        if (texto.Length <= 20)
                        {
                            obj.genero_poblacion = Convert.ToString(item["Género población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Estado población CAC";
                        texto = Convert.ToString(item["Estado población CAC"]);
                        if (texto.Length <= 20)
                        {
                            obj.estado_poblacion = Convert.ToString(item["Estado población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Regional población CAC";
                        texto = Convert.ToString(item["Regional población CAC"]);
                        if (texto.Length <= 30)
                        {
                            obj.regional_poblacion = Convert.ToString(item["Regional población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 30 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis población CAC";
                        texto = Convert.ToString(item["Unis población CAC"]);
                        if (texto.Length <= 30)
                        {
                            obj.unis_poblacion = Convert.ToString(item["Unis población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 30 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Fallecido Si/No población CAC";
                        texto = Convert.ToString(item["Fallecido Si/No población CAC"]);
                        if (texto.Length <= 5)
                        {
                            obj.fallecido_poblacion = Convert.ToString(item["Fallecido Si/No población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Fecha fallecimiento población CAC";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha fallecimiento población CAC"]);
                            if (fecha != null)
                            {
                                obj.fecha_fallecimiento = Convert.ToDateTime(item["Fecha fallecimiento población CAC"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("No puede ir vacio."))
                            {
                                textError = ex.Message;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Diagnóstico muerte población CAC";
                        texto = Convert.ToString(item["Diagnóstico muerte población CAC"]);
                        if (texto.Length <= 100)
                        {
                            obj.diagnostico_muerte_poblacion = Convert.ToString(item["Diagnóstico muerte población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Nombre diagnóstico muerte población CAC";
                        texto = Convert.ToString(item["Nombre diagnóstico muerte población CAC"]);
                        if (texto.Length <= 200)
                        {
                            obj.nombre_diagnostico_poblacion = Convert.ToString(item["Nombre diagnóstico muerte población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Edad población CAC";
                        try
                        {
                            texto = Convert.ToString(item["Edad población CAC"]).ToUpper();
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.edad_poblacion = Convert.ToInt32(item["Edad población CAC"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;

                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }



                        columna = "Tipo de paciente homologado población CAC";
                        texto = Convert.ToString(item["Tipo de paciente homologado población CAC"]);
                        if (texto.Length <= 20)
                        {
                            obj.tipo_paciente_homologado_poblacion = Convert.ToString(item["Tipo de paciente homologado población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Grupo de edad población CAC";
                        texto = Convert.ToString(item["Grupo de edad población CAC"]);
                        if (texto.Length <= 20)
                        {
                            obj.grupo_edad_poblacion = Convert.ToString(item["Grupo de edad población CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Población CAC";
                        texto = Convert.ToString(item["Población CAC"]);
                        if (texto.Length <= 50)
                        {
                            ref_cargue_cuentas_altoCosto dato = new ref_cargue_cuentas_altoCosto();
                            dato = tipoCosto.Where(x => x.descripcion.ToUpper().Contains(texto.ToUpper())).FirstOrDefault();
                            if (dato != null)
                            {
                                obj.poblacion_cac = Convert.ToString(item["Población CAC"]);
                            }
                            else
                            {
                                textError = columna + ", el tipo de poblcación: " + texto + " no existe.";
                                throw new Exception(textError);
                            }
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cargue_cuentas_altoCosto_confirmada();
                        IntContador = IntContador + 1;
                        idContadorFinal++;
                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarCuentasAltoCostoConfirmnada(OBJDetalle, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<cargue_cuentas_altoCosto_confirmada>();
                            ciclos++;
                        }
                    }
                }

                if (idContadorFinal == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    resultado = BusClass.InsertarCuentasAltoCostoConfirmnada(OBJDetalle, ref MsgRes);
                }

                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                    var error = ex.Message;
                }

                return id_cargue;
            }
            catch (Exception ex)
            {
                BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                if (textError != "" && textError != null)
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + textError;
                }

                else
                {
                    MsgRes.DescriptionResponse = "Error en la fila: " + IntContador.ToString() + " columna: " + columna;
                }

                if (ex.Message.Contains("El archivo no puede cargarse vacío"))
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.CodeError = ex.Message;
                return id_cargue;
            }
        }


        public Int32 CargueMasivoCancer(int? tipo, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            Int32 idContadorFinal = 0;
            int ciclos = 0;

            cargue_cuentas_altoCosto_cancer obj = new cargue_cuentas_altoCosto_cancer();
            cargue_cuentas_altoCosto obj2 = new cargue_cuentas_altoCosto();
            List<cargue_cuentas_altoCosto_cancer> OBJDetalle = new List<cargue_cuentas_altoCosto_cancer>();
            string columna = "";

            obj2.tipo = tipo;
            obj2.usuario_cargue = SesionVar.UserName;
            obj2.fecha_Cargue = DateTime.Now;

            var textError = "";
            var resultado = 0;

            try
            {
                id_cargue = BusClass.cargue_cuentas_altoCosto(obj2, ref MsgRes);

                int conteo = dt2.Rows.Count;

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["Rastreo mes"].ToString() != "")
                    {
                        var texto = "";
                        var fecha = new DateTime();

                        obj.id_cargue = id_cargue;

                        columna = "Rastreo mes";
                        texto = Convert.ToString(item["Rastreo mes"]);

                        if (texto.Length <= 20)
                        {
                            obj.rastreo_mes = Convert.ToString(item["Rastreo mes"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Año rastreo";
                        try
                        {
                            texto = Convert.ToString(item["Año rastreo"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.año_rastreo = Convert.ToString(item["Año rastreo"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "No. Documento";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento = Convert.ToString(item["No. Documento"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }
                            throw new Exception(textError);
                        }

                        columna = "Regional";
                        texto = Convert.ToString(item["Regional"]);

                        if (texto.Length <= 50)
                        {
                            obj.regional = Convert.ToString(item["Regional"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis";
                        texto = Convert.ToString(item["Unis"]);

                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item["Unis"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Tipo documento";
                        texto = Convert.ToString(item["Tipo documento"]);

                        if (texto.Length <= 50)
                        {
                            obj.tipo_documento = Convert.ToString(item["Tipo documento"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "No. Documento paciente";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento paciente"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento_paciente = Convert.ToString(item["No. Documento paciente"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }



                        columna = "Primer apellido";
                        texto = Convert.ToString(item["Primer apellido"]);

                        if (texto.Length <= 100)
                        {
                            obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo apellido";
                        texto = Convert.ToString(item["Segundo apellido"]);

                        if (texto.Length <= 100)
                        {
                            obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer nombre";
                        texto = Convert.ToString(item["Primer nombre"]);

                        if (texto.Length <= 100)
                        {
                            obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo nombre";
                        texto = Convert.ToString(item["Segundo nombre"]);

                        if (texto.Length <= 100)
                        {
                            obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }



                        columna = "Fecha de nacimiento";

                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha de nacimiento"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha de nacimiento"]);
                            }
                            else
                            {
                                textError = columna + ",No puede ir vacio.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("No puede ir"))
                            {
                                textError = ex.Message;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto.";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Edad";
                        try
                        {
                            texto = Convert.ToString(item["Edad"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.edad = Convert.ToInt32(item["Edad"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }

                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }
                            throw new Exception(textError);
                        }

                        columna = "Sexo";
                        texto = Convert.ToString(item["Sexo"]);

                        if (texto.Length <= 5)
                        {
                            obj.sexo = Convert.ToString(item["Sexo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Diagnóstico CIE10";
                        texto = Convert.ToString(item["Diagnóstico CIE10"]);

                        if (texto.Length <= 20)
                        {
                            obj.diagnostico_cie10 = Convert.ToString(item["Diagnóstico CIE10"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Descripción Dx";
                        texto = Convert.ToString(item["Descripción Dx"]);

                        if (texto.Length <= 100)
                        {
                            obj.descripcion_dx = Convert.ToString(item["Descripción Dx"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Agrupador";
                        texto = Convert.ToString(item["Agrupador"]);

                        if (texto.Length <= 100)
                        {
                            obj.agrupador = Convert.ToString(item["Agrupador"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Deteccion temprana";
                        texto = Convert.ToString(item["Deteccion temprana"]);

                        if (texto.Length <= 5)
                        {
                            obj.deteccion_temprana = Convert.ToString(item["Deteccion temprana"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Cuenta alto costo";
                        texto = Convert.ToString(item["Cuenta alto costo"]);

                        if (texto.Length <= 5)
                        {
                            obj.cuenta_altocosto = Convert.ToString(item["Cuenta alto costo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Quimioterapia/Radioterapia";
                        texto = Convert.ToString(item["Quimioterapia/Radioterapia"]);

                        if (texto.Length <= 30)
                        {
                            obj.quimioterapia_radioterapia = Convert.ToString(item["Quimioterapia/Radioterapia"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 30 caracteres.";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cargue_cuentas_altoCosto_cancer();
                        IntContador = IntContador + 1;
                        idContadorFinal++;
                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarCuentasAltoCostoCancer(OBJDetalle, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<cargue_cuentas_altoCosto_cancer>();
                            ciclos++;
                        }
                    }
                }

                if (idContadorFinal == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    resultado = BusClass.InsertarCuentasAltoCostoCancer(OBJDetalle, ref MsgRes);
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                    var error = ex.Message;
                }

                return id_cargue;
            }
            catch (Exception ex)
            {
                BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

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
                return 0;
            }
        }


        public Int32 CargueMasivoHemofilia(int? tipo, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            int ciclos = 0;

            cargue_cuentas_altoCosto_hemofilia obj = new cargue_cuentas_altoCosto_hemofilia();
            cargue_cuentas_altoCosto obj2 = new cargue_cuentas_altoCosto();
            List<cargue_cuentas_altoCosto_hemofilia> OBJDetalle = new List<cargue_cuentas_altoCosto_hemofilia>();
            string columna = "";

            obj2.tipo = tipo;
            obj2.usuario_cargue = SesionVar.UserName;
            obj2.fecha_Cargue = DateTime.Now;

            var textError = "";
            var resultado = 0;

            try
            {
                id_cargue = BusClass.cargue_cuentas_altoCosto(obj2, ref MsgRes);

                int conteo = dt2.Rows.Count;

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["No. Documento"].ToString() != "")
                    {
                        var texto = "";
                        var fecha = new DateTime();

                        obj.id_cargue = id_cargue;

                        columna = "No. Documento";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento = Convert.ToString(item["No. Documento"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Mes";
                        texto = Convert.ToString(item["Mes"]);

                        if (texto.Length <= 20)
                        {
                            obj.mes = Convert.ToString(item["Mes"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "En rastreos";
                        texto = Convert.ToString(item["En rastreos"]);

                        if (texto.Length <= 5)
                        {
                            obj.rastreos = Convert.ToString(item["En rastreos"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Reportado en CAC";
                        texto = Convert.ToString(item["Reportado en CAC"]);

                        if (texto.Length <= 5)
                        {
                            obj.reportado_cac = Convert.ToString(item["Reportado en CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "En Medicarte";
                        texto = Convert.ToString(item["En Medicarte"]);

                        if (texto.Length <= 5)
                        {
                            obj.en_medicarte = Convert.ToString(item["En Medicarte"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Observaciones";
                        texto = Convert.ToString(item["Observaciones"]);

                        if (texto.Length <= 1000)
                        {
                            obj.observaciones = Convert.ToString(item["Observaciones"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 1000 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Regional";
                        texto = Convert.ToString(item["Regional"]);

                        if (texto.Length <= 50)
                        {
                            obj.regional = Convert.ToString(item["Regional"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis";
                        texto = Convert.ToString(item["Unis"]);

                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item["Unis"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer nombre";
                        texto = Convert.ToString(item["Primer nombre"]);

                        if (texto.Length <= 100)
                        {
                            obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo nombre";
                        texto = Convert.ToString(item["Segundo nombre"]);

                        if (texto.Length <= 50)
                        {
                            obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer apellido";
                        texto = Convert.ToString(item["Primer apellido"]);

                        if (texto.Length <= 100)
                        {
                            obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo apellido";
                        texto = Convert.ToString(item["Segundo apellido"]);

                        if (texto.Length <= 100)
                        {
                            obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Tipo identificacion del paciente";
                        texto = Convert.ToString(item["Tipo identificacion del paciente"]);

                        if (texto.Length <= 50)
                        {
                            obj.tipo_identificacion_paciente = Convert.ToString(item["Tipo identificacion del paciente"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Identificacion del paciente";
                        try
                        {
                            texto = Convert.ToString(item["Identificacion del paciente"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 21)
                                {
                                    obj.identificacion_paciente = Convert.ToString(item["Identificacion del paciente"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 20 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 20 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Fecha de nacimiento";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha de nacimiento"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha de nacimiento"]);
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

                        columna = "Sexo";
                        texto = Convert.ToString(item["Sexo"]);

                        if (texto.Length <= 10)
                        {
                            obj.seco = Convert.ToString(item["Sexo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 10 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Diagnóstico CIE10";
                        texto = Convert.ToString(item["Diagnóstico CIE10"]);

                        if (texto.Length <= 50)
                        {
                            obj.diagnostico_cie10 = Convert.ToString(item["Diagnóstico CIE10"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 10 caracteres.";
                            throw new Exception(textError);
                        }


                        columna = "Descripción Dx";
                        texto = Convert.ToString(item["Descripción Dx"]);

                        if (texto.Length <= 200)
                        {
                            obj.descripcion_dx = Convert.ToString(item["Descripción Dx"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 200 caracteres.";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cargue_cuentas_altoCosto_hemofilia();
                        IntContador = IntContador + 1;

                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarCuentasAltoCostoHemofilia(OBJDetalle, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<cargue_cuentas_altoCosto_hemofilia>();
                            ciclos++;
                        }
                    }
                }

                if (IntContador == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    resultado = BusClass.InsertarCuentasAltoCostoHemofilia(OBJDetalle, ref MsgRes);
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                    var error = ex.Message;
                }

                return id_cargue;
            }
            catch (Exception ex)
            {
                BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

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
                return 0;
            }
        }

        public Int32 CargueMasivoArtritis(int? tipo, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            int ciclos = 0;

            cargue_cuentas_altoCosto_artritis obj = new cargue_cuentas_altoCosto_artritis();
            cargue_cuentas_altoCosto obj2 = new cargue_cuentas_altoCosto();
            List<cargue_cuentas_altoCosto_artritis> OBJDetalle = new List<cargue_cuentas_altoCosto_artritis>();
            string columna = "";

            obj2.tipo = tipo;
            obj2.usuario_cargue = SesionVar.UserName;
            obj2.fecha_Cargue = DateTime.Now;

            var textError = "";
            var resultado = 0;

            try
            {
                id_cargue = BusClass.cargue_cuentas_altoCosto(obj2, ref MsgRes);

                int conteo = dt2.Rows.Count;

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["No. Documento"].ToString() != "")
                    {
                        var texto = "";
                        var fecha = new DateTime();

                        obj.id_cargue = id_cargue;

                        columna = "No. Documento";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento = Convert.ToString(item["No. Documento"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }

                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Mes rastreo";
                        texto = Convert.ToString(item["Mes rastreo"]);

                        if (texto.Length <= 20)
                        {
                            obj.mes_rastreo = Convert.ToString(item["Mes rastreo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "En rastreos";
                        texto = Convert.ToString(item["En rastreos"]);

                        if (texto.Length <= 5)
                        {
                            obj.en_rastreo = Convert.ToString(item["En rastreos"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Reportado en CAC";
                        texto = Convert.ToString(item["Reportado en CAC"]);

                        if (texto.Length <= 5)
                        {
                            obj.reportado_cac = Convert.ToString(item["Reportado en CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "En Medicarte";
                        texto = Convert.ToString(item["En Medicarte"]);

                        if (texto.Length <= 5)
                        {
                            obj.en_medicarte = Convert.ToString(item["En Medicarte"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Observaciones";
                        texto = Convert.ToString(item["Observaciones"]);

                        if (texto.Length <= 1000)
                        {
                            obj.observaciones = Convert.ToString(item["Observaciones"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 1000 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "No. Doc 2";
                        try
                        {
                            texto = Convert.ToString(item["No. Doc 2"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento_2 = Convert.ToString(item["No. Doc 2"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }

                        }
                        catch (Exception ex)
                        {

                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Coordinación OK";
                        texto = Convert.ToString(item["Coordinación OK"]);

                        if (texto.Length <= 50)
                        {
                            obj.coordinacion_ok = Convert.ToString(item["Coordinación OK"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis";
                        texto = Convert.ToString(item["Unis"]);

                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item["Unis"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Tipo documento";
                        texto = Convert.ToString(item["Tipo documento"]);

                        if (texto.Length <= 50)
                        {
                            obj.tipo_documento = Convert.ToString(item["Tipo documento"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "No. Documento paciente";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento paciente"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento_paciente = Convert.ToString(item["No. Documento paciente"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }

                        }
                        catch (Exception ex)
                        {

                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Sexo";
                        texto = Convert.ToString(item["Sexo"]);

                        if (texto.Length <= 20)
                        {
                            obj.sexo = Convert.ToString(item["Sexo"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Fecha nacimiento";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha nacimiento"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha nacimiento"]);
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

                        columna = "Edad";
                        try
                        {
                            texto = Convert.ToString(item["Edad"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.edad = Convert.ToInt32(item["Edad"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {

                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }


                        columna = "Primer nombre";
                        texto = Convert.ToString(item["Primer nombre"]);

                        if (texto.Length <= 50)
                        {
                            obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo nombre";
                        texto = Convert.ToString(item["Segundo nombre"]);

                        if (texto.Length <= 50)
                        {
                            obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer apellido";
                        texto = Convert.ToString(item["Primer apellido"]);

                        if (texto.Length <= 50)
                        {
                            obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo apellido";
                        texto = Convert.ToString(item["Segundo apellido"]);

                        if (texto.Length <= 50)
                        {
                            obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Diagnóstico CIE10";
                        texto = Convert.ToString(item["Diagnóstico CIE10"]);

                        if (texto.Length <= 50)
                        {
                            obj.diagnostico_cie10 = Convert.ToString(item["Diagnóstico CIE10"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Descripción Dx";
                        texto = Convert.ToString(item["Descripción Dx"]);

                        if (texto.Length <= 100)
                        {
                            obj.descripcion_dx = Convert.ToString(item["Descripción Dx"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cargue_cuentas_altoCosto_artritis();
                        IntContador = IntContador + 1;

                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarCuentasAltoCostoArtritis(OBJDetalle, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<cargue_cuentas_altoCosto_artritis>();
                            ciclos++;
                        }
                    }
                }

                if (IntContador == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    resultado = BusClass.InsertarCuentasAltoCostoArtritis(OBJDetalle, ref MsgRes);
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                    var error = ex.Message;
                }

                return id_cargue;
            }
            catch (Exception ex)
            {
                BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

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
                return 0;
            }
        }

        public Int32 CargueMasivoVih(int? tipo, DataTable dt2, string nombreArchivo, ref MessageResponseOBJ MsgRes)
        {
            Int32 id_cargue = 0;
            Int32 IntContador = 1;
            Int32 IntContadorFilas = 0;
            int ciclos = 0;

            cargue_cuentas_altoCosto_vih obj = new cargue_cuentas_altoCosto_vih();
            cargue_cuentas_altoCosto obj2 = new cargue_cuentas_altoCosto();
            List<cargue_cuentas_altoCosto_vih> OBJDetalle = new List<cargue_cuentas_altoCosto_vih>();
            string columna = "";

            obj2.tipo = tipo;
            obj2.usuario_cargue = SesionVar.UserName;
            obj2.fecha_Cargue = DateTime.Now;

            var textError = "";
            var resultado = 0;

            try
            {
                id_cargue = BusClass.cargue_cuentas_altoCosto(obj2, ref MsgRes);

                int conteo = dt2.Rows.Count;

                if (conteo == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                foreach (DataRow item in dt2.Rows)
                {
                    IntContadorFilas = IntContadorFilas + 1;

                    if (item["No. Documento"].ToString() != "")
                    {
                        var texto = "";
                        var fecha = new DateTime();

                        obj.id_cargue = id_cargue;

                        columna = "Mes";
                        texto = Convert.ToString(item["Mes"]);

                        if (texto.Length <= 20)
                        {
                            obj.mes = Convert.ToString(item["Mes"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 20 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "No. Documento";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento = Convert.ToString(item["No. Documento"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }
                            throw new Exception(textError);
                        }


                        columna = "En rastreos";
                        texto = Convert.ToString(item["En rastreos"]);

                        if (texto.Length <= 5)
                        {
                            obj.en_rastreo = Convert.ToString(item["En rastreos"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Reportado en CAC";
                        texto = Convert.ToString(item["Reportado en CAC"]);

                        if (texto.Length <= 5)
                        {
                            obj.reportado_cac = Convert.ToString(item["Reportado en CAC"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "En Medicarte";
                        texto = Convert.ToString(item["En Medicarte"]);

                        if (texto.Length <= 5)
                        {
                            obj.en_medicarte = Convert.ToString(item["En Medicarte"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 5 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Observaciones";
                        texto = Convert.ToString(item["Observaciones"]);

                        if (texto.Length <= 1000)
                        {
                            obj.observaciones = Convert.ToString(item["Observaciones"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 1000 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Coordinación";
                        texto = Convert.ToString(item["Coordinación"]);

                        if (texto.Length <= 50)
                        {
                            obj.coordinacion = Convert.ToString(item["Coordinación"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Unis";
                        texto = Convert.ToString(item["Unis"]);

                        if (texto.Length <= 50)
                        {
                            obj.unis = Convert.ToString(item["Unis"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Tipo documento";
                        texto = Convert.ToString(item["Tipo documento"]);

                        if (texto.Length <= 50)
                        {
                            obj.tipo_documento = Convert.ToString(item["Tipo documento"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "No. Documento paciente";
                        try
                        {
                            texto = Convert.ToString(item["No. Documento paciente"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.documento_paciente = Convert.ToString(item["No. Documento paciente"]);
                                }
                                else
                                {

                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Primer apellido";
                        texto = Convert.ToString(item["Primer apellido"]);

                        if (texto.Length <= 50)
                        {
                            obj.primer_apellido = Convert.ToString(item["Primer apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo apellido";
                        texto = Convert.ToString(item["Segundo apellido"]);

                        if (texto.Length <= 50)
                        {
                            obj.segundo_apellido = Convert.ToString(item["Segundo apellido"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Primer nombre";
                        texto = Convert.ToString(item["Primer nombre"]);

                        if (texto.Length <= 50)
                        {
                            obj.primer_nombre = Convert.ToString(item["Primer nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Segundo nombre";
                        texto = Convert.ToString(item["Segundo nombre"]);

                        if (texto.Length <= 50)
                        {
                            obj.segundo_nombre = Convert.ToString(item["Segundo nombre"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Fecha nacimiento";
                        try
                        {
                            fecha = Convert.ToDateTime(item["Fecha nacimiento"]);
                            if (fecha != null)
                            {
                                obj.fecha_nacimiento = Convert.ToDateTime(item["Fecha nacimiento"]);
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

                        columna = "Edad";
                        try
                        {
                            texto = Convert.ToString(item["Edad"]);
                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length < 11)
                                {
                                    obj.edad = Convert.ToInt32(item["Edad"]);
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 10 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo se aceptan números.";
                                throw new Exception(textError);
                            }
                        }
                        catch (Exception ex)
                        {
                            var error = ex.Message;
                            if (error.Contains("solo se aceptan números") || error.Contains("solo puede contener 10 caracteres."))
                            {
                                textError = error;
                            }
                            else
                            {
                                textError = columna + ", formato incorrecto";
                            }

                            throw new Exception(textError);
                        }

                        columna = "Genero";
                        texto = Convert.ToString(item["Genero"]);

                        if (texto.Length <= 50)
                        {
                            obj.genero = Convert.ToString(item["Genero"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Diagnóstico CIE10";
                        texto = Convert.ToString(item["Diagnóstico CIE10"]);

                        if (texto.Length <= 50)
                        {
                            obj.diagnostico_cie10 = Convert.ToString(item["Diagnóstico CIE10"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 50 caracteres.";
                            throw new Exception(textError);
                        }

                        columna = "Descripción Dx";
                        texto = Convert.ToString(item["Descripción Dx"]);

                        if (texto.Length <= 100)
                        {
                            obj.descripcion_dx = Convert.ToString(item["Descripción Dx"]);
                        }
                        else
                        {
                            textError = columna + ", solo puede contener 100 caracteres.";
                            throw new Exception(textError);
                        }

                        OBJDetalle.Add(obj);
                        obj = new cargue_cuentas_altoCosto_vih();
                        IntContador = IntContador + 1;

                        if (IntContadorFilas >= 30000)
                        {
                            resultado = BusClass.InsertarCuentasAltoCostoVIH(OBJDetalle, ref MsgRes);
                            IntContadorFilas = 0;
                            OBJDetalle = new List<cargue_cuentas_altoCosto_vih>();
                            ciclos++;
                        }
                    }
                }

                if (IntContador == 0)
                {
                    throw new Exception("El archivo no puede cargarse vacío");
                }

                try
                {
                    resultado = BusClass.InsertarCuentasAltoCostoVIH(OBJDetalle, ref MsgRes);
                }
                catch (Exception ex)
                {
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                    MsgRes.CodeError = ex.Message;
                    MsgRes.DescriptionResponse = "Error  en el cargue masivo.";
                    BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

                    var error = ex.Message;
                }

                return id_cargue;
            }
            catch (Exception ex)
            {
                BusClass.eliminarDatosCuentasAltoCosto(id_cargue, tipo);

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
                    MsgRes.DescriptionResponse += "No pertenece a la tabla";
                }

                if (ex.Message.Contains("El archivo no puede cargarse vacío"))
                {
                    MsgRes.DescriptionResponse = ex.Message;
                }

                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.CodeError = ex.Message;
                return 0;
            }
        }

        #endregion
    }
}