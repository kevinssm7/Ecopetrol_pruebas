using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Reportes
{
    public class Reportes
    {

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

        public List<ManagmentReportDevolucionFacResult> ConsultaReporteDevolucionFac(Int32 id_devolucion_factura)
        {
            return BusClass.ConsultaReporteDevolucionFac(id_devolucion_factura);
        }
        
        public List<ManagmentReportHallazgosRipResult> ConsultaReporteHallazgosRips(Int32 id_hallazgo_RIPS)
        {
            return BusClass.ConsultaReporteHallazgosRips(id_hallazgo_RIPS);
        }

        public List<ManagmentReporIndicadorMDResult> ReporteIndicadores(Int32 id_indicadores_medicamentos)
        {
            return BusClass.ReporteIndicadores(id_indicadores_medicamentos);
        }

        public List<ManagmentReportNotifiAuditoriaResult> ReportNotificacionAudi(Int32 id)
        {
            return BusClass.ReportNotificacionAudi(id);

        }

        public List<vw_odont_ortodoncia_report> ConsultaIdReporteOrtodoncia(Int32 id_tratamiento_ortodoncia, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteOrtodoncia(id_tratamiento_ortodoncia, ref MsgRes);
        }
        public List<vw_odont_removible_report> ConsultaIdReporteRemovible(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteRemovible(id_rehabilitacion_oral_protesis_removibles, ref MsgRes);
        }
        public List<vw_odont_endodoncia_report> ConsultaIdReporteEndodoncia(Int32 id_tratamiento_endodoncia, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteEndodoncia(id_tratamiento_endodoncia, ref MsgRes);
        }

        public List<vw_odont_fija_report> ConsultaIdReporteProtesisFija(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteProtesisFija(id_tratamiento_Protesis_Fija, ref MsgRes);
        }

        public List<odont_rehabilitacion_oral_protesis_fija_dtll> ConsultaIdReporteProtesisFijaDtll(Int32 id_tratamiento_Protesis_Fija, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteProtesisFijaDtll(id_tratamiento_Protesis_Fija, ref MsgRes);
        }

        public List<vw_odont_porcentaje_d1_fija> Getporcentaje_d1_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Getporcentaje_d1_fija(id_protesis_fija, ref MsgRes);
        }

        public List<vw_odont_porcentaje_d2_fija> Getporcentaje_d2_fija(Int32 id_protesis_fija, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Getporcentaje_d2_fija(id_protesis_fija, ref MsgRes);
        }

        public List<vw_odont_reporte_removible_dtll> ConsultaIdReporteProtesisRemovibleDtll(Int32 id_rehabilitacion_oral_protesis_removibles, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ConsultaIdReporteProtesisRemovibleDtll(id_rehabilitacion_oral_protesis_removibles, ref MsgRes);
        }

        /*Alexis*/
        public List<vw_odont_tableros_ortodoncia> ConsultaListadoTTOsOrodoncia(int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ortodoncia> result = BusClass.ConsultaListadoTTOsOrodoncia(ref MsgRes);
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();

            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal&& l.año == añofinal).ToList();

            if(regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();

            return result;
        }

        public List<vw_odont_tableros_ortodoncia_prof> ConsultaListadoTTOsOrodonciaProf(int? regional, int? unis, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ortodoncia_prof> result = BusClass.ConsultaListadoTTOsOrodonciaProf(ref MsgRes);

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                result = result.Where(l => l.id_unis == unis).ToList();
            if(mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial ).ToList();
            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();

            return result;
        }
        
        public List<vw_odont_tableros_ProtesisFija> ConsultaListadoTTOsPPF(int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisFija> result = BusClass.ConsultaListadoTTOsPPF(ref MsgRes);
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();

            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            return result;
        }

        public List<vw_odont_tableros_ProtesisFija_prof> ConsultaListadoTTOsProf(int? regional, int? unis, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisFija_prof> result = BusClass.ConsultaListadoTTOsProf(ref MsgRes);

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                result = result.Where(l => l.id_unis == unis).ToList();
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();
            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();
            return result;
        }

        public List<vw_odont_tableros_ProtesisRemov> ConsultaListadoTTOsRemovible(int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisRemov> result = BusClass.ConsultaListadoTTOsRemovible(ref MsgRes);
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();

            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();

            return result;
        }

        public List<vw_odont_tableros_ProtesisRemov_prof> ConsultaListadoTTOsRemoviblesProf(int? regional, int? unis, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_ProtesisRemov_prof> result = BusClass.ConsultaListadoTTOsRemoviblesProf(ref MsgRes);

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                result = result.Where(l => l.id_unis == unis).ToList();
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();
            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();
            return result;
        }

        public List<vw_odont_tableros_endodoncia> ConsultaListadoTTOsEndodoncia(int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, int? regional, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_endodoncia> result = BusClass.ConsultaListadoTTOsEndodoncia(ref MsgRes);
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();

            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            return result;
        }

        public List<vw_odont_tableros_endodoncia_prof> ConsultaListadoEndodnciaProf(int? regional, int? unis, int? mesinicial, int? añoinicial, int? mesfinal, int? añofinal, ref MessageResponseOBJ MsgRes)
        {
            List<vw_odont_tableros_endodoncia_prof> result = BusClass.ConsultaListadoEndodonciaProf(ref MsgRes);

            if (regional != null)
                result = result.Where(l => l.id_regional == regional).ToList();
            if (unis != null)
                result = result.Where(l => l.id_unis == unis).ToList();
            if (mesinicial != null && añoinicial != null)
                result = result.Where(l => l.mes_ingresado >= mesinicial && l.año == añoinicial).ToList();
            if (mesfinal != null && añofinal != null)
                result = result.Where(l => l.mes_ingresado <= mesfinal && l.año == añofinal).ToList();
            return result;
        }

        public List<sis_usuario> ConsultaDocUusuario(String documento, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.BuscaAuditorDoc(documento, ref MsgRes);
        }
        
        public List<vw_pruebas_img_usuarios> BuscaAuditorImg(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.BuscaAuditorImg(strUsuario, ref MsgRes);
        }

        public List<vw_glosa_medicamentos> ConsultaGlosa(String formula)
        {
            return BusClass.ConsultaGlosa(formula);
        }

        public List<ManagmentFacMedicamentosResult> CuentaFacMedicamentos(String factura, String formula, Int32 OPC, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CuentaFacMedicamentos(factura, formula, OPC, ref MsgRes);
        }

        public List<managmentprestadoresFacturasResult> GetFacturasByEstadoAceptadas(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoAceptadas(idestado, ref MsgRes);
        }
        public List<managmentprestadoresFacturasReporteResult> GetFacturasByEstadoReporte(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoReporte(idestado, ref MsgRes);
        }


        public List<managmentprestadoresFacturasReporte_fisResult> GetFacturasByEstadoReporte_fis(int idestado, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByEstadoReporte_fis(idestado, ref MsgRes);
        }

        public List<managmentRechazoFacturasReporteResult> GetFacturasByRechazoReporte(int id_dtll, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetFacturasByRechazoReporte(id_dtll, ref MsgRes);
        }

        public List<managmentRechazoLoteFacturasReporteResult> GetLoteFacturasByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetLoteFacturasByRechazoReporte(id_lote, ref MsgRes);
        }

        public List<managmentRechazoLoteDtllFacturasReporteResult> GetLoteFacturasdtllByRechazoReporte(int id_lote, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetLoteFacturasdtllByRechazoReporte(id_lote, ref MsgRes);
        }

        

        public ecop_firma_digital_sami GetFirmas(Int32? idusuario)
        {
            return BusClass.GetFirmas(idusuario);
        }

        public management_ecop_firma_digital_samiResult GetFirmasId(int? idUsuario)
        {
            return BusClass.GetFirmasId(idUsuario);
        }

        public string AgregarValor(string Valor)
        {
            var Conteo = "";
            int length1 = Valor.Length;
            switch (length1)
            {
                case 1:
                    Conteo = "00000000";
                    break;
                case 2:
                    Conteo = "0000000";
                    break;
                case 3:
                    Conteo = "000000";
                    break;
                case 4:
                    Conteo = "00000";
                    break;
                case 5:
                    Conteo = "0000";
                    break;
                default:
                    Conteo = "000";
                    break;
            }
            if (length1 <= 1)
            {
               
            }
            return Conteo;
        }

        public string AgregarValor2(string Valor)
        {
            var Conteo = "";
            int length1 = Valor.Length;
            switch (length1)
            {
                case 1:
                    Conteo = "000";
                    break;
                case 2:
                    Conteo = "00";
                    break;
                case 3:
                    Conteo = "0";
                    break;
               
                default:
                    Conteo = "0";
                    break;
            }
            if (length1 <= 1)
            {

            }
            return Conteo;

        }

        public Int32 InsertarFacturaAprobadas(ecop_gestion_facturas_aprobadas obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFacturaAprobadas(obj, ref MsgRes);
        }

        public List<management_reportelotecontabilizadoResult> ConsultaReporteLote(Int32 ID)
        {
            return BusClass.ConsultaReporteLote(ID);
        }


    }
}