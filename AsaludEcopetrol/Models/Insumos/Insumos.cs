using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.Data;
using System.Configuration;
using System.IO;

namespace AsaludEcopetrol.Models.Insumos
{
    public class Insumos
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

        #region Metodos

        public bool ValidarExistenciaQuejasValidas(int mes, int año)
        {
            return BusClass.ValidarExistenciaQuejasValidas(mes, año);
        }

        public void InsertarDetallesQuejasValidas(List<calidad_quejas_validas_dtll> result, calidad_quejas_validas objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarQuejasValidasDtll(result, objbase, ref MsgRes);
        }

        public List<vw_calidad_quejas_validas> GetListCalidadQuejasValidas()
        {
            return BusClass.GetListCalidadQuejasValidas();
        }

        public List<calidad_quejas_validas_base_zip> GetListBasesCargadasQuejasValidas()
        {
            return BusClass.GetListBasesCargadasQuejasValidas();
        }

        public calidad_quejas_validas_base_zip GetArchivoById(int id)
        {
            return BusClass.GetArchivoById(id);
        }

        public void EliminarArchivoZipQuejasValidas(calidad_quejas_validas_base_zip obj)
        {
            BusClass.EliminarArchivoZipQuejasValidas(obj);
        }

        public void InsertarArchivoQuejasValidas(calidad_quejas_validas_base_zip obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarArchivoQuejasValidas(obj, ref MsgRes);
        }


        public bool ValidarExistenciaOportunidadRIPS(int mes, int año)
        {
            return BusClass.ValidarExistenciaOportunidadRIPS(mes, año);
        }

        public void InsertarDetallesOportunidadRIPS(List<calidad_oportunidad_rips_dtll> result, calidad_oportunidad_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarOportunidadRips(result, objbase, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_rips_indicador> GetListCalidadOportunidadRips()
        {
            return BusClass.GetListCalidadOportunidadRips();
        }

        public void InsertarDetallesCalidadRIPS(List<calidad_de_rips_dtll> result, calidad_de_rips objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCalidadRips(result, objbase, ref MsgRes);
        }

        public List<vw_calidad_de_rips_indicador> GetListCalidadCalidadRips()
        {
            return BusClass.GetListCalidadCalidadRips();
        }

        public void InsertarOportunidadCitasMedicas(List<calidad_oportunidad_citas_medicina_gnral_dtll> result, calidad_oportunidad_citas_medicina_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarOportunidadCitasMedicas(result, objbase, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_citas_medicina_gnral_indicador> GetListCalidadOporCitasMedicas()
        {
            return BusClass.GetListCalidadOporCitasMedicas();
        }

        public void InsertarOportunidadOdontologia(List<calidad_oportunidad_odontologia_gnral_dtll> result, calidad_oportunidad_odontologia_gnral objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarOportunidadOdontologia(result, objbase, ref MsgRes);
        }

        public List<vw_calidad_oportunidad_odontologia_gnral_indicador> GetListCalidadOporOdontologia()
        {
            return BusClass.GetListCalidadOporOdontologia();
        }

        public List<vw_calidad_citas_cumplidas_indicador> GetListCalidadCitasCumplidas()
        {
            return BusClass.GetListCalidadCitasCumplidas();
        }


        public void InsertarCalidadCitasCumplidas(List<calidad_citas_cumplidas_dtll> result, calidad_citas_cumplidas objbase, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCalidadCitasCumplidas(result, objbase, ref MsgRes);
        }

        public void InsertarEventosAdversos(DataTable dt, ref MessageResponseOBJ MsgRes)
        {
            Int32 fila = 1;
            string columna = "";
            List<calidad_evento_adverso> result = new List<calidad_evento_adverso>();

            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    calidad_evento_adverso obj = new calidad_evento_adverso();
                    fila = fila + 1;

                    if (!string.IsNullOrEmpty(item["AÑO"].ToString()))
                    {
                        columna = "Año";
                        obj.año = Convert.ToInt32(item["AÑO"]);

                        columna = "ID MES";
                        obj.id_mes = Convert.ToInt32(item["ID MES"]);

                        columna = "PERIODO";
                        obj.periodo = Convert.ToDateTime(item["PERIODO"]);

                        columna = "MES";
                        obj.mes = Convert.ToString(item["MES"]);

                        columna = "CONSECUTIVO";
                        obj.consecutivo = Convert.ToInt32(item["CONSECUTIVO"]);

                        columna = "FECHA DE REPORTE";
                        obj.fecha_reporte = Convert.ToDateTime(item["FECHA DE REPORTE"]);

                        columna = "FECHA DE OCURRENCIA DEL EVENTO";
                        obj.fecha_ocurrencia_evento = Convert.ToDateTime(item["FECHA DE OCURRENCIA DEL EVENTO"]);

                        columna = "LOCALIDAD DE SERVICIOS DE SALUD";
                        obj.localidad_servicios_salud = Convert.ToString(item["LOCALIDAD DE SERVICIOS DE SALUD"]);

                        columna = "DEPENDENCIA DE SALUD";
                        obj.dependencia_de_salud = Convert.ToString(item["DEPENDENCIA DE SALUD"]);

                        columna = "FUENTE DEL REPORTE";
                        obj.fuente_del_reporte = Convert.ToString(item["FUENTE DEL REPORTE"]);

                        columna = "REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE";
                        obj.nom_reportante = Convert.ToString(item["REPORTANTE (NOMBRE DE QUIEN REALIZA EL REPORTE"]);

                        columna = "REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE";
                        obj.id_reportante = Convert.ToString(item["REPORTANTE (IDENTIFICACIÓN DE  QUIEN REALIZA EL REPORTE"]);

                        columna = "LUGAR DONDE OCURRIO EL INCIDENTE O EVENTO ";
                        obj.lugar_ocurrencia_evento = Convert.ToString(item["LUGAR DONDE OCURRIO EL INCIDENTE O EVENTO "]);

                        columna = "AMBITO DE OCURRENCIA DEL EVENTO";
                        obj.ambito_ocurrencia_evento = Convert.ToString(item["AMBITO DE OCURRENCIA DEL EVENTO"]);

                        columna = "NIT DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO";
                        obj.nit_prestador_ocurrencia_evento = Convert.ToString(item["NIT DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO"]);

                        columna = "NOMBRE DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO";
                        obj.nom_prestador_ocurrencia_evento = Convert.ToString(item["NOMBRE DEL PRESTADOR EN DONDE OCURRIO EL EVENTO ADVERSO"]);

                        columna = "NUMERO DE IDENTIFICACION DEL PRESTADOR (CODIGO SAP)";
                        obj.num_id_prestador_codigo_sap = Convert.ToString(item["NUMERO DE IDENTIFICACION DEL PRESTADOR (CODIGO SAP)"]);

                        columna = "TIPO DE IDENTIFICACION DEL BENEFICIARIO";
                        obj.tipo_ident_beneficiario_ocurrencia_evento = Convert.ToString(item["TIPO DE IDENTIFICACION DEL BENEFICIARIO"]);

                        columna = "NUMERO DE IDENTIFICACION DEL BENEFICIARIO";
                        obj.num_id_beneficiario = Convert.ToString(item["NUMERO DE IDENTIFICACION DEL BENEFICIARIO"]);

                        columna = "NOMBRE DEL BENEFICIARIO";
                        obj.nom_beneficiario = Convert.ToString(item["NOMBRE DEL BENEFICIARIO"]);

                        columna = "EDAD DEL BENEFICIARIO ";
                        obj.edad_beneficiario = Convert.ToInt32(item["EDAD DEL BENEFICIARIO "]);

                        columna = "DESCRIPCION DEL EVENTO";
                        obj.descripcion_evento = Convert.ToString(item["DESCRIPCION DEL EVENTO"]);

                        columna = "CATEGORIA DEL EVENTO";
                        obj.categoria_evento = Convert.ToString(item["CATEGORIA DEL EVENTO"]);

                        columna = "SUBCATEGORIA DEL EVENTO";
                        obj.subcategoria_evento = Convert.ToString(item["SUBCATEGORIA DEL EVENTO"]);

                        columna = "CLASIFICACION DEL EVENTO DE LA ATENCIÓN EN SALUD";
                        obj.clasificacion_evento_atencion_en_salud = Convert.ToString(item["CLASIFICACION DEL EVENTO DE LA ATENCIÓN EN SALUD"]);

                        columna = "CONFIRMACION EVENTO (PREVENIBLE NO PREVENIBLE)";
                        obj.confirmacion_evento = Convert.ToString(item["CONFIRMACION EVENTO (PREVENIBLE NO PREVENIBLE)"]);

                        columna = "CONCEPTO AUDITORIA";
                        obj.concepto_auditoria = Convert.ToString(item["CONCEPTO AUDITORIA"]);

                        columna = "SEVERIDAD DEL DESENLACE";
                        obj.severidad_del_desenlace = Convert.ToString(item["SEVERIDAD DEL DESENLACE"]);

                        columna = "PROBABILIDAD DE REPETICION";
                        obj.probabilidad_de_repeticion = Convert.ToString(item["PROBABILIDAD DE REPETICION"]);

                        columna = "GESTION DE LA GESTORÍA INTEGRAL DE LA CALIDAD";
                        obj.gestion_de_la_gestoria_integral_calidad = Convert.ToString(item["GESTION DE LA GESTORÍA INTEGRAL DE LA CALIDAD"]);

                        columna = "PLAN DE MEJORA AL PRESTADOR (SI O NO)";
                        obj.plan_de_mejora_al_prestador = Convert.ToString(item["PLAN DE MEJORA AL PRESTADOR (SI O NO)"]);

                        columna = "COSTO DE NO CALIDAD";
                        obj.costo_de_no_calidad = Convert.ToDecimal(item["COSTO DE NO CALIDAD"]);

                        columna = "SEGUIMIENTO";
                        obj.seguimiento = Convert.ToString(item["SEGUIMIENTO"]);

                        columna = "REGIONAL DEL EVENTO";
                        obj.regional_evento = Convert.ToString(item["REGIONAL DEL EVENTO"]);

                        columna = "FECHA RADICACION DEL PLAN DE MEJORA";
                        obj.fecha_radicacion_plan_mejora = Convert.ToString(item["FECHA RADICACION DEL PLAN DE MEJORA"]);

                        columna = "FECHA PROGRAMADA PARA REVISION DE PLAN DE MEJORA";
                        obj.fecha_programada_revision_plan_mejora = Convert.ToString(item["FECHA PROGRAMADA PARA REVISION DE PLAN DE MEJORA"]);

                        result.Add(obj);
                        obj = new calidad_evento_adverso();
                    }

                }

                BusClass.InsertarEventosAdversos(result, ref MsgRes);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (ex.Message.Contains("No se puede convertir un objeto DBNull en otros tipos"))
                {
                    mensaje = "Revise el registro ya que el dato tiene un formato incorrecto";
                }
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Error en la fila: " + fila.ToString() + " columna: " + columna + " " + mensaje;
                MsgRes.CodeError = ex.Message;
            }
        }

        public List<calidad_evento_adverso> GetListCalidadEventoAdverso()
        {
            return BusClass.GetListCalidadEventoAdverso();
        }

        public string ObtenerPath(string filename)
        {

            string carpeta = "";
            if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "1")
            {
                carpeta = "DocumentosInsumos";
            }
            else if (ConfigurationManager.AppSettings["BDActiva"].ToString() == "2")
            {
                carpeta = "DocumentosInsumosPruebas";
            }

            string Rutadocumentos = ConfigurationManager.AppSettings["rutaDocumentosInsumos"];
            if (!System.IO.Directory.Exists(Rutadocumentos))
            {
                System.IO.Directory.CreateDirectory(Rutadocumentos);
            }

            Rutadocumentos += "//" + carpeta + "//";
            if (!System.IO.Directory.Exists(Rutadocumentos))
            {
                System.IO.Directory.CreateDirectory(Rutadocumentos);
            }
            return Path.Combine(Rutadocumentos, filename);
        }

        public void InsertarDocumentoInsumo(calidad_gestor_documental_insumos obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarDocumentoInsumo(obj, ref MsgRes);
        }

        public List<calidad_gestor_documental_insumos> GetListGestorDocumentalInsumos()
        {
            return BusClass.GetListGestorDocumentalInsumos();
        }

        public calidad_gestor_documental_insumos GetDocumentoById(int id)
        {
            return BusClass.GetDocumentoById(id);
        }

        public vw_calidad_gestor_documental_insumos VwGetDocumentoById(int id)
        {
            return BusClass.VwGetDocumentoById(id);
        }

        public vw_calidad_gestor_documental_insumos TarerArchivoInsumosId(int id)
        {
            return BusClass.TarerArchivoInsumosId(id);
        }

        public void EliminarDocumentoRutaFisica(string ruta, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                if (System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }

                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = ECOPETROL_COMMON.ENUM.BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }

        }

        public void EliminarDocumento(calidad_gestor_documental_insumos obj)
        {
            BusClass.EliminarDocumento(obj);
        }

        public List<ref_calidad_insumos_tipo_documental> GetListInsumoTipoDocumental()
        {
            return BusClass.GetListInsumoTipoDocumental();
        }

        #endregion
    }
}