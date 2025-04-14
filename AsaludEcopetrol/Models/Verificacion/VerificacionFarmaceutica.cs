using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Verificacion
{
    public class VerificacionFarmaceutica
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

        public int id_evaluacion { get; set; }

        public List<ref_ver_tipoCriterio> Get_refTipoCriterio()
        {
            return BusClass.Get_refTipoCriterio();
        }

        public List<management_verificacionListaResult> getTipoCriterio(int idTipo)
        {
            return BusClass.getTipoCriterioId(idTipo);
        }

        public ref_verificacion_farmaceutico Get_refVerificacionFarmaceutitaById(int idTipoVer)
        {
            return BusClass.Get_refVerificacionFarmaceutitaById(idTipoVer);
        }
        public List<ref_verificacion_farmaceutico> Get_refVerificacionFarmaceutita()
        {
            return BusClass.Get_refVerificacionFarmaceutita();
        }

        public void InsertarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarVerificacion(obj, ref MsgRes);
        }
        public void ActualizarVerificacion(ref_verificacion_farmaceutico obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarVerificacion(obj, ref MsgRes);
        }

        public void InsertarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarTipoCriteriover(obj, ref MsgRes);
        }
        public void ActualizarTipoCriteriover(ref_ver_tipoCriterio obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarTipoCriteriover(obj, ref MsgRes);
        }
        public List<ver_tipocriterio> get_ref_tipoCriterio(int idVerificacion)
        {
            return BusClass.get_ref_tipoCriterio(idVerificacion);
        }

        public List<ref_ver_grupo_tpocriterio> get_ver_grupoTipoCritero()
        {
            return BusClass.get_ver_grupoTipoCritero();
        }

        public void InsertarAdminCriteriosver(int tipoVerificacion, List<int> seleccionados, List<int> seleccionados2, List<string> seleccionados3, string usuario, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarAdminCriteriosver(tipoVerificacion, seleccionados, seleccionados2, seleccionados3, usuario, ref MsgRes);
        }

        public void EliminarTipoCriteriover(int idtipocriterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarTipoCriteriover(idtipocriterio, ref MsgRes);
        }
        public List<ver_criterio> getcriteriosbytipoverificacion(int tipoverificacion)
        {
            return BusClass.getcriteriosbytipoverificacion(tipoverificacion);
        }

        public ver_criterio ConsultarCriterioById2(int idcriterio)
        {
            return BusClass.ConsultarCriterioById2(idcriterio);
        }

        public void InsertarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCriteriover(criterio, ref MsgRes);
        }

        public void ActualizarCriteriover(ver_criterio criterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCriteriover(criterio, ref MsgRes);
        }

        public List<ref_verificacionFarmaceutica_tipos> getTiposVerificacion()
        {
            return BusClass.getTiposVerificacion();
        }

        public void EliminarCriterioVerificacion(int idcriterio, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarCriterioVerificacion(idcriterio, ref MsgRes);
        }

        public void InsertarCarguePuntoDispensacion(List<ver_puntoDispensacion> List, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarCarguePuntoDispensacion(List, ref MsgRes);
        }

        public List<ver_puntoDispensacion> getPuntoDispensacionList()
        {
            return BusClass.getPuntoDispensacionList();
        }

        public List<management_verificacionListaResult> getTotalDatosDispen()
        {
            return BusClass.getTotalDatosDispen();
        }

        public int ActualizarPuntoDispensacion(ver_puntoDispensacion obj)
        {
            return BusClass.ActualizarPuntoDispensacion(obj);
        }

        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacion()
        {
            return BusClass.getDispensacionEvaluacion();
        }

        public List<management_dispensacion_evaluacionRelacion_totalResult> getDispensacionEvaluacionTotal()
        {
            return BusClass.getDispensacionEvaluacionTotal();
        }
        public List<management_dispensacion_evaluacionRelacionResult> getDispensacionEvaluacionId(int Id)
        {
            return BusClass.getDispensacionEvaluacionId(Id);
        }

        public List<management_dispensacion_evaluacionRelacion_totalIdResult> getDispensacionEvaluacionTotalId(int id)
        {
            return BusClass.getDispensacionEvaluacionTotalId(id);
        }

        public List<management_dispensacion_evaluacionRelacion_hallazgoResult> getEvolucionHallazgos()
        {
            return BusClass.getEvolucionHallazgos();
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalHallazgo()
        {
            return BusClass.getDispensacionEvaluacionTotalHallazgo();
        }

        public List<management_dispensacion_evaluacionRelacion_total_hallazgoResult> getDispensacionEvaluacionTotalIdHallazgoId(int id)
        {
            return BusClass.getDispensacionEvaluacionTotalIdHallazgoId(id);
        }

        public List<ref_evaluacion_estadoTotal> getEstadosEvaluacionHallazgos()
        {
            return BusClass.getEstadosEvaluacionHallazgos();
        }


        public List<ref_evaluacion_tipoHallazgo> getTipoHallazgoEvaluacion()
        {
            return BusClass.getTipoHallazgoEvaluacion();
        }
        public List<ver_evaluacion_hallazgo> ExisteHallazgoSubsanado(int idTotal, int id_tipoCriterio)
        {
            return BusClass.ExisteHallazgoSubsanado(idTotal, id_tipoCriterio);
        }

        public List<ref_evaluacion_cumplimiento> getCumplimientoEvaluacion()
        {
            return BusClass.getCumplimientoEvaluacion();
        }
        public List<ref_evaluacion_tipoSoporte> getTipoSoporteEvaluacion()
        {
            return BusClass.getTipoSoporteEvaluacion();
        }

        public int insertarHallazgoItemEvaluacion(ver_evaluacion_hallazgo obj)
        {
            return BusClass.insertarHallazgoItemEvaluacion(obj);
        }

        public int insertarHallazgoItemEvaluacionArchivos(ver_evaluacion_hallazgo_archivos obj)
        {
            return BusClass.insertarHallazgoItemEvaluacionArchivos(obj);
        }
        public int ActualizarHallazgoVisitas(ver_evaluacion_hallazgo obj)
        {
            return BusClass.ActualizarHallazgoVisitas(obj);
        }
    }
}