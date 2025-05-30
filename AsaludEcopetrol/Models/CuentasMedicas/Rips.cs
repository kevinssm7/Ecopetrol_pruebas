using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using System.Configuration;
using System.Data.SqlClient;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class Rips
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

        public Int32 InsertRips(RIPS ObjRips)
        {
            return BusClass.InsertarRips(ObjRips, ref MsgRes);
        }

        public List<RIPS> ConsultaRips(Int32 IdRips)
        {
            return BusClass.ConsultaRips(IdRips, ref MsgRes);
        }

        public List<Ref_tipo_rips> ConsultaTipoRips()
        {
            return BusClass.ConsultaTipoRips(ref MsgRes);
        }

        public bool ActualizarRips(RIPS ObjRips)
        {
            return BusClass.ActualizaRips(ObjRips, ref MsgRes);
        }

        public Int32 InsertRipsAC(List<RIPS_AC> ObjRipsAC, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAC(ObjRipsAC, ref MsgRes);
        }
        public Int32 InsertRipsAD(List<RIPS_AD> ObjRipsAD, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAD(ObjRipsAD, ref MsgRes);
        }
        public Int32 InsertRipsAF(List<RIPS_AF> ObjRipsAF, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAF(ObjRipsAF, ref MsgRes);
        }
        public Int32 InsertRipsAH(List<RIPS_AH> ObjRipsAH, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAH(ObjRipsAH, ref MsgRes);
        }
        public Int32 InsertRipsAM(List<RIPS_AM> ObjRipsAM, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAM(ObjRipsAM, ref MsgRes);
        }
        public Int32 InsertRipsAN(List<RIPS_AN> ObjRipsAN, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAN(ObjRipsAN, ref MsgRes);
        }
        public Int32 InsertRipsAP(List<RIPS_AP> ObjRipsAP, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAP(ObjRipsAP, ref MsgRes);
        }
        public Int32 InsertRipsAT(List<RIPS_AT> ObjRipsAT, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAT(ObjRipsAT, ref MsgRes);
        }
        public Int32 InsertRipsAU(List<RIPS_AU> ObjRipsAU, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsAU(ObjRipsAU, ref MsgRes);
        }
        public Int32 InsertRipsCT(List<RIPS_CT> ObjRipsCT, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsCT(ObjRipsCT, ref MsgRes);
        }
        public Int32 InsertRipsUS(List<RIPS_US> ObjRipsUS, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarRipsUS(ObjRipsUS, ref MsgRes);
        }

        public List<ECOPETROL_COMMON.ENUM.reporterips> GetEvaluacionRips(Int32 IdRips)
        {
            string conexion = "";
            conexion = ConfigurationManager.ConnectionStrings["ecopetrolConnectionString"].ConnectionString;
            return BusClass.ConsultaRipsEvaluacion(IdRips, conexion, ref MsgRes);
        }

        public List<managmentReportePrestadoresNoExistentesResult> getprestadoresnoexistentes(Int32 IdRips, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.getprestadoresnoexistentes(IdRips, ref MsgRes);
        }

        public List<Logerroresevaluacionrips> GetLogEvaluacionRips(Int32 IdRips)
        {
            List<Logerroresevaluacionrips> consulta = BusClass.ConsultaLogRipsEvaluacion(IdRips, ref MsgRes);
            //List<Logerroresevaluacionrips> listado = new List<Logerroresevaluacionrips>();
            //foreach (ManagmentErroresRipsEvaluacionResult item in consulta)
            //{
            //    Logerroresevaluacionrips lista = new Logerroresevaluacionrips();
            //    lista.codigo_prestador = item.codigo_prestador;
            //    lista.prestador = item.nombre_prestador;
            //    lista.AC_Num_factura_no_existe_en_AF = item.ac_factura_con_error;
            //    lista.AP_Num_factura_no_existe_en_AF = item.ap_factura_con_error;
            //    lista.AH_Num_factura_no_existe_en_AF = item.ah_factura_con_error;
            //    lista.AU_Num_factura_no_existe_en_AF = item.au_factura_con_error;
            //    lista.AC_Dx_no_corresponde_con_finalidad = item.diagnostico_no_corresponde_con_finalidad;
            //    lista.AC_Usuario_debe_estar_en_US = item.ac_usuario_con_error;
            //    lista.AP_Usuario_debe_estar_en_US = item.ap_usuario_con_error;
            //    lista.AU_Usuario_debe_estar_en_US = item.au_usuario_con_error;
            //    lista.AH_Usuario_debe_estar_en_US = item.ah_usuario_con_error;
            //    lista.AC_sin_DX = item.sin_dx;
            //    lista.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado = item.Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado;
            //    lista.AP_Sin_ambito_en_el_CUPS = item.Sin_ambito_en_el_CUPS;
            //    lista.AP_Sin_CUPS = item.Sin_CUPS;
            //    lista.AU_Sin_causa_basica_de_muerte = item.Sin_causa_basica_de_muerte;
            //    lista.AU_Error_en_fecha_de_egreso = item.Error_en_fecha_de_egreso;
            //    lista.AU_Error_en_causa_externa = item.au_causa_ext_con_error;
            //    lista.AH_Error_en_causa_externa = item.ah_causa_ext_con_error;
            //    lista.AU_Error_de_DX_no_aplica_R_Z = item.au_diag_egreso_con_error;
            //    lista.AH_Error_de_DX_no_aplica_R_Z = item.ah_diag_egreso_con_error;
            //    lista.AN_Sin_fecha_de_muerte = item.an_fecha_muerte_con_error;
            //    lista.AN_Sin_hora_de_muerte = item.an_hora_muerte_con_error;
            //    lista.Total_Errores = item.ap_total_errores + item.ac_total_errores + item.au_total_errores + item.ah_total_errores + item.an_total_errores;
            //    listado.Add(lista);
            //}

            return consulta;
        }

        public List<Logerroresevaluacionrips> GetLogEvaluacionRipsHistorico(Int32 IdRips, string regional, string mes, string año)
        {
            List<ManagmentErroresRipsEvaluacion_historicoResult> consulta = BusClass.ConsultaLogRipsEvaluacionHistorico(IdRips, ref MsgRes);
            List<Logerroresevaluacionrips> listado = new List<Logerroresevaluacionrips>();
            foreach (ManagmentErroresRipsEvaluacion_historicoResult item in consulta)
            {
                Logerroresevaluacionrips lista = new Logerroresevaluacionrips();
                lista.codigo_prestador = item.codigo_prestador;
                lista.prestador = item.nombre_prestador;
                lista.AC_Num_factura_no_existe_en_AF = item.ac_factura_con_error;
                lista.AP_Num_factura_no_existe_en_AF = item.ap_factura_con_error;
                lista.AH_Num_factura_no_existe_en_AF = item.ah_factura_con_error;
                lista.AU_Num_factura_no_existe_en_AF = item.au_factura_con_error;
                lista.AC_Dx_no_corresponde_con_finalidad = item.diagnostico_no_corresponde_con_finalidad;
                lista.AC_Usuario_debe_estar_en_US = item.ac_usuario_con_error;
                lista.AP_Usuario_debe_estar_en_US = item.ap_usuario_con_error;
                lista.AU_Usuario_debe_estar_en_US = item.au_usuario_con_error;
                lista.AH_Usuario_debe_estar_en_US = item.ah_usuario_con_error;
                lista.AC_sin_DX = item.sin_dx;
                lista.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado = item.Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado;
                lista.AP_Sin_ambito_en_el_CUPS = item.Sin_ambito_en_el_CUPS;
                lista.AP_Sin_CUPS = item.Sin_CUPS;
                lista.AU_Sin_causa_basica_de_muerte = item.Sin_causa_basica_de_muerte;
                lista.AU_Error_en_fecha_de_egreso = item.Error_en_fecha_de_egreso;
                lista.AU_Error_en_causa_externa = item.au_causa_ext_con_error;
                lista.AH_Error_en_causa_externa = item.ah_causa_ext_con_error;
                lista.AU_Error_de_DX_no_aplica_R_Z = item.au_diag_egreso_con_error;
                lista.AH_Error_de_DX_no_aplica_R_Z = item.ah_diag_egreso_con_error;
                lista.AN_Sin_fecha_de_muerte = item.an_fecha_muerte_con_error;
                lista.AN_Sin_hora_de_muerte = item.an_hora_muerte_con_error;
                lista.Total_Errores = item.ap_total_errores + item.ac_total_errores + item.au_total_errores + item.ah_total_errores + item.an_total_errores;
                lista.Mes = mes;
                lista.Año = año;
                lista.Regional = regional;
                listado.Add(lista);
            }

            return listado;
        }

        public List<RIPS>GetListaRipsPorMesYAño(int mes, int año, int? regional)
        {
            return BusClass.GetListaRipsPorMesYAño(mes, año, regional);
        }

        public vw_totales_rips_evaluacion GetTotalesEvaluacionRips(Int32 IdRips)
        {
            return BusClass.ConsultaTotalesRipsEvaluacion(IdRips, ref MsgRes);
        }

        public RIPS ValidacionCargueRips(int idregional, int mes, int año)
        {
            return BusClass.ValidacionCargueRips(idregional, mes, año);
        }

        public RIPS_AC GetRipsAcById(int idripsac)
        {
            return BusClass.GetRipsAcById(idripsac);
        }

        public RIPS_AP GetRipsApById(int idripsap)
        {
            return BusClass.GetRipsApById(idripsap);
        }

        public RIPS_AU GetRipsAuById(int id)
        {
            return BusClass.GetRipsAuById(id);
        }

        public RIPS_AH GetRipsAhById(int id)
        {
            return BusClass.GetRipsAhById(id);
        }

        public RIPS_AN GetRipsAnById(int id)
        {
            return BusClass.GetRipsAnById(id);
        }

        
        public LogErroresevaluacionRipsAC ConstruirDetalleLogAC(List<Logerroresevaluacionrips> report)
        {
            LogErroresevaluacionRipsAC log = new LogErroresevaluacionRipsAC();

            int count = 0;
            string detallelog = "";
            foreach (Logerroresevaluacionrips item in report)
            {
                if (item.AC_Error_en_DX_Genero > 0)
                {
                    count += item.AC_Error_en_DX_Genero;
                    detallelog += " AC: Error en DX Genero: " + item.AC_Error_en_DX_Genero + "\r\n";
                }

                if (item.AC_Num_factura_no_existe_en_AF > 0)
                {
                    count += item.AC_Num_factura_no_existe_en_AF;
                    detallelog += "AC: Numero de factura no existe en  archivoAF: " + item.AC_Num_factura_no_existe_en_AF + "\r\n";
                    
                }

                if (item.AC_Dx_no_corresponde_con_finalidad > 0)
                {
                    count += item.AC_Dx_no_corresponde_con_finalidad;
                    detallelog += "AC: Finalidad 5 fuera del rango de edad: " + item.AC_Dx_no_corresponde_con_finalidad + "\r\n";
                    
                }

                if (item.AC_Usuario_debe_estar_en_US > 0)
                {
                    count += item.AC_Usuario_debe_estar_en_US;
                    detallelog += "AC: Identificación no existe en archivo US: " + item.AC_Usuario_debe_estar_en_US + "\r\n";
                    
                }

                if (item.AC_sin_DX > 0)
                {
                    count += item.AC_sin_DX;
                    detallelog += "AC: Sin DX: " + item.AC_sin_DX + " ,";
                }
            }

            log.numerrores = count;
            log.detallelog = detallelog;
            return log;
        }

        public LogErroresevaluacionRipsAP ConstruirDetalleLogAP(List<Logerroresevaluacionrips> report)
        {
            LogErroresevaluacionRipsAP log = new LogErroresevaluacionRipsAP();

            int count = 0;
            string detallelog = "";
            foreach (Logerroresevaluacionrips item in report)
            {

                if (item.AP_Error_en_DX_Genero > 0)
                {
                    detallelog += "AP: Error en DX Genero: " + item.AP_Error_en_DX_Genero + "\r\n";
                    count += item.AP_Error_en_DX_Genero;
                }

                if (item.AP_Num_factura_no_existe_en_AF > 0)
                {
                    detallelog +="AP: Numero de factura no existe en  archivoAF: " + item.AP_Num_factura_no_existe_en_AF + "\r\n";
                    count += item.AP_Num_factura_no_existe_en_AF;
                }

                if (item.AP_Usuario_debe_estar_en_US > 0)
                {
                    detallelog += "AP: Identificación no existe en archivo US: " + item.AP_Usuario_debe_estar_en_US + "\r\n";
                    count += item.AP_Usuario_debe_estar_en_US;
                }

                if (item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado > 0)
                {
                    detallelog +="AP: Procedimiento Quirúrgico sin DX o con DX errado: " + item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado + "\r\n";
                    count += item.AP_Procedimiento_Quirúrgico_sin_Dx_o_con_Dx_errado;
                }
                    
                if (item.AP_Sin_ambito_en_el_CUPS > 0)
                {
                    detallelog +="AP: Sin ambito en el Cups: " + item.AP_Sin_ambito_en_el_CUPS + "\r\n";
                    count += item.AP_Sin_ambito_en_el_CUPS;
                }

                if (item.AP_Sin_CUPS > 0)
                {
                    detallelog += "AP: Sin Cups: " + item.AP_Sin_CUPS + "\r\n";
                    count += item.AP_Sin_CUPS;
                }
            }

            log.numerrores = count;
            log.detallelog = detallelog;
            return log;
        }

        public LogErroresevaluacionRipsAN ConstruirDetalleLogAN(List<Logerroresevaluacionrips> report)
        {
            LogErroresevaluacionRipsAN log = new LogErroresevaluacionRipsAN();

            int count = 0;
            string detallelog = "";
            foreach (Logerroresevaluacionrips item in report)
            {
                if (item.AN_Sin_fecha_de_muerte > 0)
                {
                    detallelog += "AN: Sin fecha de Muerte: " + item.AN_Sin_fecha_de_muerte + "\r\n";
                    count += item.AN_Sin_fecha_de_muerte;
                }

                if (item.AN_Sin_hora_de_muerte > 0)
                {
                    detallelog += "AN: Sin hora de Muerte: " + item.AN_Sin_hora_de_muerte + "\r\n";
                    count += item.AN_Sin_fecha_de_muerte;
                }
            }

            log.numerrores = count;
            log.detallelog = detallelog;
            return log;
        }

        public LogErroresevaluacionRipsAU ConstruirDetalleLogAU(List<Logerroresevaluacionrips> report)
        {
            LogErroresevaluacionRipsAU log = new LogErroresevaluacionRipsAU();

            int count = 0;
            string detallelog = "";
            foreach (Logerroresevaluacionrips item in report)
            {
                if (item.AU_Num_factura_no_existe_en_AF > 0)
                {
                    detallelog += "AU: Numero de factura no existe en  archivoAF: " + item.AU_Num_factura_no_existe_en_AF + "\r\n";
                    count += item.AU_Num_factura_no_existe_en_AF;
                }

                if (item.AU_Usuario_debe_estar_en_US > 0)
                {
                    detallelog += "AU: Identificación no existe en archivo US: " + item.AU_Usuario_debe_estar_en_US + "\r\n";
                    count += item.AU_Usuario_debe_estar_en_US;
                }

                if (item.AU_Sin_causa_basica_de_muerte > 0)
                {
                    detallelog += "AU: Sin Causa Basica de Muerte: " + item.AU_Sin_causa_basica_de_muerte + "\r\n";
                    count += item.AU_Sin_causa_basica_de_muerte;
                }

                if (item.AU_Error_en_fecha_de_egreso > 0)
                {
                    detallelog += "AU: Error en fecha de egreso: " + item.AU_Error_en_fecha_de_egreso + "\r\n";
                    count += item.AU_Error_en_fecha_de_egreso;
                }


                if (item.AU_Error_en_causa_externa > 0)
                {
                    detallelog += "AU: Error en causa externa RC: " + item.AU_Error_en_causa_externa + "\r\n";
                    count += item.AU_Error_en_causa_externa;
                }

                if (item.AU_Error_de_DX_no_aplica_R_Z > 0)
                {
                    detallelog += "AU: Error en causa externa DX: " + item.AU_Error_de_DX_no_aplica_R_Z + "\r\n";
                    count += item.AU_Error_de_DX_no_aplica_R_Z;
                }

            }

            log.numerrores = count;
            log.detallelog = detallelog;
            return log;
        }
        
        public LogErroresevaluacionRipsAH ConstruirDetalleLogAH(List<Logerroresevaluacionrips> report)
        {
            LogErroresevaluacionRipsAH log = new LogErroresevaluacionRipsAH();

            int count = 0;
            string detallelog = "";
            foreach (Logerroresevaluacionrips item in report)
            {
                if (item.AH_Error_en_DX_Genero > 0)
                {
                    detallelog += "AH: Error en DX Genero: " + item.AH_Error_en_DX_Genero + "\r\n";
                    count += item.AH_Error_en_DX_Genero;
                }

                if (item.AH_Num_factura_no_existe_en_AF > 0)
                {
                    detallelog += "AH: Numero de factura no existe en  archivoAF: " + item.AH_Num_factura_no_existe_en_AF + "\r\n";
                    count += item.AH_Num_factura_no_existe_en_AF;
                }

                if (item.AH_Usuario_debe_estar_en_US > 0)
                {
                    detallelog += "AH: Identificación no existe en archivo US: " + item.AH_Usuario_debe_estar_en_US + "\r\n";
                    count += item.AH_Usuario_debe_estar_en_US;
                }

                if (item.AH_Error_en_causa_externa > 0)
                {
                    detallelog += "AH: Error en causa externa: " + item.AH_Error_en_causa_externa + "\r\n";
                    count += item.AH_Error_en_causa_externa;
                }

                if (item.AH_Error_de_DX_no_aplica_R_Z > 0)
                {
                    detallelog += "AH: Error de diagnostico, No aplica R-Z: " + item.AH_Error_de_DX_no_aplica_R_Z + "\r\n";
                    count += item.AH_Error_de_DX_no_aplica_R_Z;
                }
                
            }

            log.numerrores = count;
            log.detallelog = detallelog;
            return log;
        }

        public List<ManagmentRipsHomologacionFacResult> ConsultaHomologacionFac(String num_factura, String tipo_id_prestador, String num_id_prestador)
        {
            return BusClass.ConsultaHomologacionFac(num_factura, tipo_id_prestador, num_id_prestador);
        }
        public List<Ref_tipo_documental> GetTipoIdentificacion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTipoIdentificacion(ref MsgRes);
        }

        //public void insercionDatos()
        //{
        //    using (SqlConnection connection = new SqlConnection(""))
        //    {
        //        connection.Open();

        //        SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
        //        bulkCopy.DestinationTableName = "miTabla";

        //        bulkCopy.ColumnMappings.Add("columna1", "columna1");
        //        bulkCopy.ColumnMappings.Add("columna2", "columna2");
        //    }
        //}
        #endregion
    }
}