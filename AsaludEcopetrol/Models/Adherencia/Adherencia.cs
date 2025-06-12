using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Adherencia
{
    public class Adherencia
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

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio()
        {
            return BusClass.get_ref_TipoCriterio();
        }

        public List<ref_adh_grupo_tipocriterio> get_ref_grupoTipoCriterio()
        {
            return BusClass.get_ref_grupoTipoCriterio();
        }

        public List<adh_tipocriterio> get_adh_tipocriterio(int idadherencia)
        {
            return BusClass.get_adh_tipocriterio(idadherencia);
        }

        public List<ref_adh_grupo_tipocriterio> get_ref_adh_grupotipocriterio()
        {
            return BusClass.get_ref_adh_grupotipocriterio();
        }

        public List<ref_adh_tipo_criterio> get_ref_TipoCriterio_cohorte(int idtipocohorte)
        {
            return BusClass.get_ref_TipoCriterio_cohorte(idtipocohorte);
        }

        public List<adh_criterio> getcriteriosbytipocohorte(int tipocohorte)
        {
            return BusClass.getcriteriosbytipocohorte(tipocohorte);
        }

        public void InsertarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarTipoCriterio(obj, ref MsgRes);
        }

        public void InsertarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCriterio(criterio, ref MsgRes);
        }
            
        public void ActualizarTipoCriterio(ref_adh_tipo_criterio obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarTipoCriterio(obj, ref MsgRes);
        }

        public void ActualizarCriterio(adh_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCriterio(criterio, ref MsgRes);
        }

        public void EliminarCriterioCohorte(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarCriterioCohorte(idcriterio, ref MsgRes);
        }

        public void EliminarTipoCriterio(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarTipoCriterio(idtipocriterio, ref MsgRes);
        }

        public adh_criterio ConsultarCriterioById(int idcriterio)
        {
            return BusClass.ConsultarCriterioById(idcriterio);
        }

        public int InsertarResultados(adh_resultados resultados, List<string> resultadoshc1, ref MessageResponseOBJ Msg)
        {
            return BusClass.InsertarResultados(resultados, resultadoshc1, ref Msg);
        }

        public List<adh_resultados> GetResultadosPrestador(int idprestador, int profesional, int mes, int año)
        {
            return BusClass.GetResultadosPrestador(idprestador, profesional, mes, año);
        }

        public List<vw_rptResultadosAdherencia> GetResultadosPrestador(Int32? idresultados)
        {
            return BusClass.GetResultadosPrestador(idresultados);
        }


        public List<managmentReporteResultadosAdherenciaResult> GetResultadosAdherencia(Int32 idresultados)
        {
            return BusClass.GetResultadosAdherencia(idresultados);
        }

        public List<managmentReporteResultadosAdherencia2Result> GetResultadosAdherencia2(Int32 idresultados)
        {
            return BusClass.GetResultadosAdherencia2(idresultados);
        }

        public List<Management_adh_cantidad_resultados_grupoResult> GetResultadosGrupoAdherencia(Int32 idresultados)
        {
            return BusClass.GetResultadosGrupoAdherencia(idresultados);
        }


        public List<Ref_ips_cuentas> getprestadores(string param)
        {
            List<Ref_ips_cuentas> prestadores = BusClass.getprestadores();
            if (!String.IsNullOrEmpty(param))
            {
                prestadores = prestadores.Where(l => !String.IsNullOrEmpty(l.Nit) && !String.IsNullOrEmpty(l.Nombre)).ToList();
                prestadores = prestadores.Where(l => l.Nit.Contains(param) || l.Nombre.ToLower().Contains(param.ToLower())).ToList();
            }

            return prestadores;
        }

        public void InsertarTipoCohorte(ref_cohortes obj)
        {
            BusClass.InsertarTipoCohorte(obj);
        }

        public void ActualizarTipoCohorte(ref_cohortes obj)
        {
            BusClass.ActualizarTipoCohorte(obj);
        }

        public ref_cohortes getTipoCohorteById(int idtipocohorte)
        {
            return BusClass.getTipoCohorteById(idtipocohorte);
        }

        public void InsertarAdminCriterios(int tipoadherencia, List<int> seleccionados, List<int> seleccionados2, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarAdminCriterios(tipoadherencia, seleccionados, seleccionados2, ref MsgRes);
        }

        public List<ref_adherencia_unis> GetUnisByRegional(int idregional)
        {
            return BusClass.GetUnisByRegional(idregional);
        }

        public List<ref_adherencia_ciudad> GetciudadByunis(int idunis)
        {
            return BusClass.GetciudadByunis(idunis);
        }

        public List<ref_adherencia_prestador_ciudad> GetPrestadoresByciudad(int idciudad)
        {
            return BusClass.GetPrestadoresByciudad(idciudad);
        }

        public List<ref_adherencia_profesional_prestador> GetProfesionalesByprestador(int idprestador)
        {
            return BusClass.GetProfesionalesByprestador(idprestador);
        }

        public ref_adherencia_prestador GetprestadorByid(int idprestador)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ref_adherencia_prestador.Where(l => l.id_adherencia_prestador == idprestador).FirstOrDefault();
        }

        public List<ManagmentReporteResultadosAdherenciaGnrResult> ObtenerResultadosGeneralesAdherencia(int id, int regional, DateTime? fecha1,
            DateTime? fecha2, int? mesini, int? mesfin, int? añoini, int? añofin, DateTime? fechahcinicio, DateTime? fechahcfinal, int? unis, int? ciudad, int? prestador, int? profesional)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ManagmentReporteResultadosAdherenciaGnr(id, regional, unis, ciudad, prestador, profesional, fecha1, fecha2, mesini, mesfin, añoini, añofin, fechahcinicio, fechahcfinal).ToList();
        }
        public List<ref_adherencia_ciudad> getCiudadesUnis(int idUnis)
        {
            return BusClass.getCiudadesUnis(idUnis);
        }

        public AdherenciaConteo GetDatosConteoAdh(int idadhresultados)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            List<Management_adh_conteo_seleccion_grupoResult> Result = db.Management_adh_conteo_seleccion_grupo(idadhresultados).ToList();

            AdherenciaConteo obj = new AdherenciaConteo();

            if (Result.Where(l => l.seleccion_historia_clinica == "SI" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault() != null)
                obj.calidad_cumple = Result.Where(l => l.seleccion_historia_clinica == "SI" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "NO" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault() != null)
                obj.calidad_nocumple = Result.Where(l => l.seleccion_historia_clinica == "NO" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "NA" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault() != null)
                obj.calidad_noaplica = Result.Where(l => l.seleccion_historia_clinica == "NA" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "PA" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault() != null)
                obj.calidad_parcial = Result.Where(l => l.seleccion_historia_clinica == "PA" && l.id_ref_adh_grupo_tipocriterio == 1).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "SI" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault() != null)
                obj.cumplimiento_cumple = Result.Where(l => l.seleccion_historia_clinica == "SI" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "NO" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault() != null)
                obj.cumplimiento_nocumple = Result.Where(l => l.seleccion_historia_clinica == "NO" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "NA" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault() != null)
                obj.cumplimiento_noaplica = Result.Where(l => l.seleccion_historia_clinica == "NA" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault().cantidad;
            if (Result.Where(l => l.seleccion_historia_clinica == "PA" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault() != null)
                obj.cumplimiento_parcial = Result.Where(l => l.seleccion_historia_clinica == "PA" && l.id_ref_adh_grupo_tipocriterio == 2).FirstOrDefault().cantidad;

            if (obj.calidad_cumple == null)
                obj.calidad_cumple = 0;
            if (obj.calidad_nocumple == null)
                obj.calidad_nocumple = 0;
            if (obj.calidad_noaplica == null)
                obj.calidad_noaplica = 0;
            if (obj.calidad_parcial == null)
                obj.calidad_parcial = 0;
            if (obj.cumplimiento_cumple == null)
                obj.cumplimiento_cumple = 0;
            if (obj.cumplimiento_nocumple == null)
                obj.cumplimiento_nocumple = 0;
            if (obj.cumplimiento_parcial == null)
                obj.cumplimiento_parcial = 0;
            if (obj.cumplimiento_noaplica == null)
                obj.cumplimiento_noaplica = 0;

            return obj;
        }

        public List<vw_seleccion_adh_por_componente> GetListResultadosComponente(int id_tipo_cohorte)
        {
            List<vw_seleccion_adh_por_componente> result = new List<vw_seleccion_adh_por_componente>();
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            db.CommandTimeout = 10000;
            return result = db.vw_seleccion_adh_por_componente.Where(l => l.tipo_cohorte == id_tipo_cohorte).ToList();

        }

        #endregion
    }

    public class AdherenciaConteo
    {
        public int? calidad_cumple { get; set; }
        public int? calidad_nocumple { get; set; }
        public int? calidad_noaplica { get; set; }
        public int? calidad_parcial { get; set; }
        public int? cumplimiento_cumple { get; set; }
        public int? cumplimiento_nocumple { get; set; }
        public int? cumplimiento_noaplica { get; set; }
        public int? cumplimiento_parcial { get; set; }
    }
}