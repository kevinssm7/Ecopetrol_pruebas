using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using AsaludEcopetrol.BussinesManager;
using AsaludEcopetrol.Models.CuentasMedicas;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Consulta
{
    public class Consulta
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


        private List<Ref_tipo_documental> _ListTipoDoc;
        public List<Ref_tipo_documental> ListTipoDoc
        {
            get
            {
                if (_ListTipoDoc == null)
                {
                    return _ListTipoDoc = BusClass.GetTipoDocumnetal();
                }
                else
                {
                    return _ListTipoDoc;
                }

            }

            set
            {
                _ListTipoDoc = value;
            }
        }

        private List<vw_consulta_censo> _ListaCensoFechas;
        public List<vw_consulta_censo> ListaCensoFechas
        {
            get
            {
                if (_ListaCensoFechas == null)
                {
                    _ListaCensoFechas = BusClass.ConsultaCenso(ref MsgRes);
                    _ListaCensoFechas = _ListaCensoFechas.Where(x => x.fecha_recepcion_censo >= fecha_inicio && x.fecha_recepcion_censo <= fecha_fin).ToList();

                    return _ListaCensoFechas;
                }
                else
                {
                    return _ListaCensoFechas;
                }

            }

            set
            {
                _ListaCensoFechas = value;
            }
        }

        private List<vw_consulta_ecop> _ListaCensoConcuFechas;
        public List<vw_consulta_ecop> ListaCensoConcuFechas
        {
            get
            {
                if (_ListaCensoConcuFechas == null)
                {
                    _ListaCensoConcuFechas = BusClass.ConsultaCensoConcurrencia(ref MsgRes);
                    _ListaCensoConcuFechas = _ListaCensoConcuFechas.Where(x => Convert.ToDateTime(x.fecha_recepcion_censo) >= fecha_inicio && Convert.ToDateTime(x.fecha_recepcion_censo) <= fecha_fin).ToList();

                    return _ListaCensoConcuFechas;
                }
                else
                {
                    return _ListaCensoConcuFechas;
                }

            }

            set
            {
                _ListaCensoConcuFechas = value;
            }
        }


        private List<Management_Consulta_EcopResult> _ListaNuevasConsultas;
        public List<Management_Consulta_EcopResult> ListaNuevasConsultas
        {
            get
            {
                if (_ListaNuevasConsultas == null)
                {

                    return _ListaNuevasConsultas = new List<Management_Consulta_EcopResult>();
                }
                else
                {
                    return _ListaNuevasConsultas;
                }

            }

            set
            {
                _ListaNuevasConsultas = value;
            }
        }


        private List<Management_Consulta_Ecop2Result> _ListaNuevasConsultas2;
        public List<Management_Consulta_Ecop2Result> ListaNuevasConsultas2
        {
            get
            {
                if (_ListaNuevasConsultas2 == null)
                {

                    return _ListaNuevasConsultas2 = new List<Management_Consulta_Ecop2Result>();
                }
                else
                {
                    return _ListaNuevasConsultas2;
                }

            }

            set
            {
                _ListaNuevasConsultas2 = value;
            }
        }


        private List<vw_consulta_pacientesActivos> _ListaPacientesActivos;
        public List<vw_consulta_pacientesActivos> ListaPacientesActivos
        {
            get
            {
                if (_ListaPacientesActivos == null)
                {

                    return _ListaPacientesActivos = BusClass.ConsultaPacientesActivos();
                }
                else
                {
                    return _ListaPacientesActivos;
                }
            }
            set
            {
                _ListaPacientesActivos = value;
            }
        }

        private List<vw_consulta_censo> _ListaCensoDocumento;
        public List<vw_consulta_censo> ListaCensoDocumento
        {
            get
            {
                if (_ListaCensoDocumento == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaCensoDocumento = db.vw_consulta_censo.Where(x => x.num_identifi_afil == num_identifi_afil).ToList();
                    }

                    return _ListaCensoDocumento;
                }
                else
                {
                    return _ListaCensoDocumento;
                }

            }

            set
            {
                _ListaCensoDocumento = value;
            }
        }

        private List<vw_consulta_censo> _ListaCensoRegional;
        public List<vw_consulta_censo> ListaCensoRegional
        {
            get
            {
                if (_ListaCensoRegional == null)
                {

                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaCensoRegional = db.vw_consulta_censo.Where(x => x.regional == regional && (x.fecha_recepcion_censo.Value.Date >= fecha_inicio.Value.Date && x.fecha_recepcion_censo.Value.Date <= fecha_fin.Value.Date)).ToList();
                    }

                    return _ListaCensoRegional;
                }
                else
                {
                    return _ListaCensoRegional;
                }

            }

            set
            {
                _ListaCensoRegional = value;
            }
        }

        private List<vw_consulta_concurrencia> _ListaConcurrenciaFechas;
        public List<vw_consulta_concurrencia> ListaConcurrenciaFechas
        {
            get
            {
                if (_ListaConcurrenciaFechas == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 10000;
                        _ListaConcurrenciaFechas = db.vw_consulta_concurrencia.Where(l => l.fecha_evolucion.Value.Date >= fecha_inicio && l.fecha_evolucion <= fecha_fin).ToList();
                    }

                    return _ListaConcurrenciaFechas;
                }
                else
                {
                    return _ListaConcurrenciaFechas;
                }

            }

            set
            {
                _ListaConcurrenciaFechas = value;
            }
        }


        private List<vw_consulta_concurrencia_reporte> _ListaConcurrenciaFechasReporte;
        public List<vw_consulta_concurrencia_reporte> ListaConcurrenciaFechasReporte
        {
            get
            {
                if (_ListaConcurrenciaFechasReporte == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 10000;
                        _ListaConcurrenciaFechasReporte = db.vw_consulta_concurrencia_reporte.Where(l => Convert.ToDateTime(l.fecha_evolucion) >= fecha_inicio && Convert.ToDateTime(l.fecha_evolucion) <= fecha_fin).ToList();
                    }

                    return _ListaConcurrenciaFechasReporte;
                }
                else
                {
                    return _ListaConcurrenciaFechasReporte;
                }
            }

            set
            {
                _ListaConcurrenciaFechasReporte = value;
            }
        }

        private List<vw_consulta_concurrencia> _ListaConcurrenciaFechas2;
        public List<vw_consulta_concurrencia> ListaConcurrenciaFechas2
        {
            get
            {
                if (_ListaConcurrenciaFechas2 == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaConcurrenciaFechas2 = db.vw_consulta_concurrencia.Where(x => x.fecha_ingreso.Value.Date >= fecha_inicio.Value.Date && x.fecha_ingreso.Value.Date <= fecha_fin.Value.Date).ToList();
                    }

                    return _ListaConcurrenciaFechas2;
                }
                else
                {
                    return _ListaConcurrenciaFechas2;
                }

            }

            set
            {
                _ListaConcurrenciaFechas2 = value;
            }
        }

        private List<vw_consulta_concurrencia> _ListaConcurrenciaDocumento;
        public List<vw_consulta_concurrencia> ListaConcurrenciaDocumento
        {
            get
            {
                if (_ListaConcurrenciaDocumento == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaConcurrenciaDocumento = db.vw_consulta_concurrencia.Where(x => x.num_identifi_afil == num_identifi_afil).ToList();
                    }

                    return _ListaConcurrenciaDocumento;
                }
                else
                {
                    return _ListaConcurrenciaDocumento;
                }

            }

            set
            {
                _ListaConcurrenciaDocumento = value;
            }
        }

        private List<vw_consulta_concurrencia_reporte> _ListaConcurrenciaDocumentoReporte;
        public List<vw_consulta_concurrencia_reporte> ListaConcurrenciaDocumentoReporte
        {
            get
            {
                if (_ListaConcurrenciaDocumentoReporte == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaConcurrenciaDocumentoReporte = db.vw_consulta_concurrencia_reporte.Where(x => x.num_identifi_afil == num_identifi_afil).ToList();
                    }

                    return _ListaConcurrenciaDocumentoReporte;
                }
                else
                {
                    return _ListaConcurrenciaDocumentoReporte;
                }

            }

            set
            {
                _ListaConcurrenciaDocumentoReporte = value;
            }
        }

        private List<vw_consulta_concurrencia> _ListaConcurrenciaRegional;
        public List<vw_consulta_concurrencia> ListaConcurrenciaRegional
        {
            get
            {
                if (_ListaConcurrenciaRegional == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        try
                        {
                            db.CommandTimeout = 5000;
                            _ListaConcurrenciaRegional = db.vw_consulta_concurrencia.Where(x => x.regional == regional && (x.fecha_evolucion.Value.Date >= fecha_inicio.Value.Date && x.fecha_evolucion.Value.Date <= fecha_fin.Value.Date)).ToList();
                        }
                        catch (Exception ex)
                        {
                            _ListaConcurrenciaRegional = new List<vw_consulta_concurrencia>();
                        }

                    }

                    return _ListaConcurrenciaRegional;
                }
                else
                {
                    return _ListaConcurrenciaRegional;
                }

            }

            set
            {
                _ListaConcurrenciaRegional = value;
            }
        }


        private List<vw_consulta_concurrencia_reporte> _ListaConcurrenciaRegionalReporte;
        public List<vw_consulta_concurrencia_reporte> ListaConcurrenciaRegionalReporte
        {
            get
            {
                if (_ListaConcurrenciaRegionalReporte == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        try
                        {
                            db.CommandTimeout = 5000;
                            _ListaConcurrenciaRegionalReporte = db.vw_consulta_concurrencia_reporte.Where(x => x.regional == regional && (Convert.ToDateTime(x.fecha_evolucion) >= fecha_inicio.Value.Date && Convert.ToDateTime(x.fecha_evolucion) <= fecha_fin.Value.Date)).ToList();
                        }
                        catch (Exception ex)
                        {
                            _ListaConcurrenciaRegionalReporte = new List<vw_consulta_concurrencia_reporte>();
                        }

                    }

                    return _ListaConcurrenciaRegionalReporte;
                }
                else
                {
                    return _ListaConcurrenciaRegionalReporte;
                }

            }

            set
            {
                _ListaConcurrenciaRegionalReporte = value;
            }
        }



        private List<vw_consulta_egreso> _ListaegresoFechas;
        public List<vw_consulta_egreso> ListaegresoFechas
        {
            get
            {
                if (_ListaegresoFechas == null)
                {
                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaegresoFechas = db.vw_consulta_egreso.Where(x => x.fecha_egreso.Value.Date >= fecha_inicio.Value.Date && x.fecha_egreso.Value.Date <= fecha_fin.Value.Date).ToList();
                    }

                    return _ListaegresoFechas;
                }
                else
                {
                    return _ListaegresoFechas;
                }

            }

            set
            {
                _ListaegresoFechas = value;
            }
        }

        private List<vw_consulta_egreso> _ListaegresoDocumentos;
        public List<vw_consulta_egreso> ListaegresoDocumentos
        {
            get
            {
                if (_ListaegresoDocumentos == null)
                {

                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 5000;
                        _ListaegresoDocumentos = db.vw_consulta_egreso.Where(x => x.documento == num_identifi_afil).ToList();
                    }

                    return _ListaegresoDocumentos;
                }
                else
                {
                    return _ListaegresoDocumentos;
                }

            }

            set
            {
                _ListaegresoDocumentos = value;
            }
        }

        private List<vw_consulta_egreso> _ListaegresoRegional;
        public List<vw_consulta_egreso> ListaegresoRegional
        {
            get
            {
                if (_ListaegresoRegional == null)
                {

                    using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                    {
                        db.CommandTimeout = 3000;
                        _ListaegresoRegional = db.vw_consulta_egreso.Where(x => x.id_ref_regional == regional).ToList();
                    }

                    _ListaegresoRegional = _ListaegresoRegional.Where(x => x.fecha_egreso.Value.Date >= fecha_inicio.Value.Date && x.fecha_egreso.Value.Date <= fecha_fin.Value.Date).ToList();


                    return _ListaegresoRegional;
                }
                else
                {
                    return _ListaegresoRegional;
                }

            }

            set
            {
                _ListaegresoRegional = value;
            }
        }


        private List<managment_vw_consulta_egresoResult> _ListaegresoFechas2;
        public List<managment_vw_consulta_egresoResult> ListaegresoFechas2
        {
            get
            {
                if (_ListaegresoFechas2 == null)
                {
                    _ListaegresoFechas2 = BusClass.ConsultaEgreso2(ref MsgRes);
                    _ListaegresoFechas2 = _ListaegresoFechas2.Where(x => x.fecha_egreso >= fecha_inicio && x.fecha_egreso <= fecha_fin).ToList();

                    return _ListaegresoFechas2;
                }
                else
                {
                    return _ListaegresoFechas2;
                }

            }

            set
            {
                _ListaegresoFechas2 = value;
            }
        }

        private List<managment_vw_consulta_egresoResult> _ListaegresoDocumentos2;
        public List<managment_vw_consulta_egresoResult> ListaegresoDocumentos2
        {
            get
            {
                if (_ListaegresoDocumentos2 == null)
                {
                    _ListaegresoDocumentos2 = BusClass.ConsultaEgreso2(ref MsgRes);
                    _ListaegresoDocumentos2 = _ListaegresoDocumentos2.Where(x => x.documento == num_identifi_afil).ToList();

                    return _ListaegresoDocumentos2;
                }
                else
                {
                    return _ListaegresoDocumentos2;
                }

            }

            set
            {
                _ListaegresoDocumentos2 = value;
            }
        }

        private List<managment_vw_consulta_egresoResult> _ListaegresoRegional2;
        public List<managment_vw_consulta_egresoResult> ListaegresoRegional2
        {
            get
            {
                if (_ListaegresoRegional2 == null)
                {
                    _ListaegresoRegional2 = BusClass.ConsultaEgreso2(ref MsgRes);
                    _ListaegresoRegional2 = _ListaegresoRegional2.Where(x => x.id_ref_regional == regional).ToList();
                    _ListaegresoRegional2 = _ListaegresoRegional2.Where(x => x.fecha_egreso >= fecha_inicio && x.fecha_egreso <= fecha_fin).ToList();


                    return _ListaegresoRegional2;
                }
                else
                {
                    return _ListaegresoRegional2;
                }

            }

            set
            {
                _ListaegresoRegional2 = value;
            }
        }



        private List<vw_consulta_eventos_adversos> _ListaEventosFechas;
        public List<vw_consulta_eventos_adversos> ListaEventosFechas
        {
            get
            {
                if (_ListaEventosFechas == null)
                {
                    _ListaEventosFechas = BusClass.ConsultaEventosAd(ref MsgRes);
                    _ListaEventosFechas = _ListaEventosFechas.Where(x => x.fecha_evento_adv >= fecha_inicio && x.fecha_evento_adv <= fecha_fin).ToList();

                    return _ListaEventosFechas;
                }
                else
                {
                    return _ListaEventosFechas;
                }

            }

            set
            {
                _ListaEventosFechas = value;
            }
        }

        private List<vw_consulta_eventos_adversos> _ListaEventosDocumento;
        public List<vw_consulta_eventos_adversos> ListaEventosDocumento
        {
            get
            {
                if (_ListaEventosDocumento == null)
                {
                    _ListaEventosDocumento = BusClass.ConsultaEventosAd(ref MsgRes);
                    _ListaEventosDocumento = _ListaEventosDocumento.Where(x => x.num_identifi_afil == num_identifi_afil).ToList();

                    return _ListaEventosDocumento;
                }
                else
                {
                    return _ListaEventosDocumento;
                }

            }

            set
            {
                _ListaEventosDocumento = value;
            }
        }


        private List<vw_consulta_situacion_calidad> _ListaSituacionCalidadFechas;
        public List<vw_consulta_situacion_calidad> ListaSituacionCalidadFechas
        {
            get
            {
                if (_ListaSituacionCalidadFechas == null)
                {
                    _ListaSituacionCalidadFechas = BusClass.ConsultaSituacionCal(ref MsgRes);
                    _ListaSituacionCalidadFechas = _ListaSituacionCalidadFechas.Where(x => x.fecha_situcion >= fecha_inicio && x.fecha_situcion <= fecha_fin).ToList();

                    return _ListaSituacionCalidadFechas;
                }
                else
                {
                    return _ListaSituacionCalidadFechas;
                }

            }

            set
            {
                _ListaSituacionCalidadFechas = value;
            }
        }


        private List<vw_consulta_situacion_calidad> _ListaSituacionCalidadDocumento;
        public List<vw_consulta_situacion_calidad> ListaSituacionCalidadDocumento
        {
            get
            {
                if (_ListaSituacionCalidadDocumento == null)
                {
                    _ListaSituacionCalidadDocumento = BusClass.ConsultaSituacionCal(ref MsgRes);
                    _ListaSituacionCalidadDocumento = _ListaSituacionCalidadDocumento.Where(x => x.num_identifi_afil == num_identifi_afil).ToList();

                    return _ListaSituacionCalidadDocumento;
                }
                else
                {
                    return _ListaSituacionCalidadDocumento;
                }

            }

            set
            {
                _ListaSituacionCalidadDocumento = value;
            }
        }

        private List<vw_gestantes> _ListaGestantesFechas;
        public List<vw_gestantes> ListaGestantesFechas
        {
            get
            {
                if (_ListaGestantesFechas == null)
                {

                    _ListaGestantesFechas = BusClass.ConsultaGestantes(ref MsgRes);
                    _ListaGestantesFechas = _ListaGestantesFechas.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _ListaGestantesFechas;
                }
                else
                {
                    return _ListaGestantesFechas;
                }
            }

            set
            {
                _ListaGestantesFechas = value;
            }
        }

        private List<management_controlNatalidadResult> _ListaGestantesFechasNuevo;
        public List<management_controlNatalidadResult> ListaGestantesFechasNuevo
        {
            get
            {
                if (_ListaGestantesFechasNuevo == null)
                {
                    DateTime fin = (DateTime)fecha_fin;
                    fin = fin.AddDays(1);

                    _ListaGestantesFechasNuevo = BusClass.ConsultaGestantesNuevo(ref MsgRes);
                    _ListaGestantesFechasNuevo = _ListaGestantesFechasNuevo.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fin).ToList();

                    return _ListaGestantesFechasNuevo;
                }
                else
                {
                    return _ListaGestantesFechasNuevo;
                }

            }

            set
            {
                _ListaGestantesFechasNuevo = value;
            }
        }

        private List<vw_gestantes> _ListaGestantesRegional;
        public List<vw_gestantes> ListaGestantesRegional
        {
            get
            {
                if (_ListaGestantesRegional == null)
                {

                    _ListaGestantesRegional = BusClass.ConsultaGestantes(ref MsgRes);
                    _ListaGestantesRegional = _ListaGestantesRegional.Where(x => x.regional_id == regional).ToList();
                    _ListaGestantesRegional = _ListaGestantesRegional.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _ListaGestantesRegional;
                }
                else
                {
                    return _ListaGestantesRegional;
                }

            }

            set
            {
                _ListaGestantesRegional = value;
            }
        }
        private List<management_controlNatalidadResult> _ListaGestantesRegionalNueva;
        public List<management_controlNatalidadResult> ListaGestantesRegionalNueva
        {
            get
            {
                if (_ListaGestantesRegionalNueva == null)
                {

                    DateTime fin = (DateTime)fecha_fin;
                    fin = fin.AddDays(1);
                    _ListaGestantesRegionalNueva = BusClass.ConsultaGestantesNuevo(ref MsgRes);
                    _ListaGestantesRegionalNueva = _ListaGestantesRegionalNueva.Where(x => x.regional_id == regional).ToList();
                    _ListaGestantesRegionalNueva = _ListaGestantesRegionalNueva.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fin).ToList();

                    return _ListaGestantesRegionalNueva;
                }
                else
                {
                    return _ListaGestantesRegionalNueva;
                }
            }

            set
            {
                _ListaGestantesRegionalNueva = value;
            }
        }


        private List<vw_gestantes> _ListaGestantesNuevo;
        public List<vw_gestantes> ListaGestantesNuevo
        {
            get
            {
                if (_ListaGestantesNuevo == null)
                {

                    return _ListaGestantesNuevo = new List<vw_gestantes>();
                }
                else
                {
                    return _ListaGestantesNuevo;
                }

            }

            set
            {
                _ListaGestantesRegional = value;
            }
        }


        private List<vw_gestantes_sin> _ListaGestantesSinFechas;
        public List<vw_gestantes_sin> ListaGestantesSinFechas
        {
            get
            {
                if (_ListaGestantesSinFechas == null)
                {
                    _ListaGestantesSinFechas = BusClass.ConsultaGestantesSin(ref MsgRes);
                    _ListaGestantesSinFechas = _ListaGestantesSinFechas.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _ListaGestantesSinFechas;
                }
                else
                {
                    return _ListaGestantesSinFechas;
                }

            }

            set
            {
                _ListaGestantesSinFechas = value;
            }
        }


        private List<vw_Mortalidad> _ListaMortalidadFechas;
        public List<vw_Mortalidad> ListaMortalidadFechas
        {
            get
            {
                if (_ListaMortalidadFechas == null)
                {
                    _ListaMortalidadFechas = BusClass.ConsultaMortalidad(ref MsgRes);
                    _ListaMortalidadFechas = _ListaMortalidadFechas.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _ListaMortalidadFechas;
                }
                else
                {
                    return _ListaMortalidadFechas;
                }

            }

            set
            {
                _ListaMortalidadFechas = value;
            }
        }

        private List<vw_Mortalidad> _ListaMortalidadRegional;
        public List<vw_Mortalidad> ListaMortalidadRegional
        {
            get
            {
                if (_ListaMortalidadRegional == null)
                {
                    _ListaMortalidadRegional = BusClass.ConsultaMortalidad(ref MsgRes);
                    _ListaMortalidadRegional = _ListaMortalidadRegional.Where(x => x.regional_id == regional).ToList();
                    _ListaMortalidadRegional = _ListaMortalidadRegional.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();


                    return _ListaMortalidadRegional;
                }
                else
                {
                    return _ListaMortalidadRegional;
                }

            }

            set
            {
                _ListaMortalidadRegional = value;
            }
        }



        private List<vw_Mortalidad> _ListaMortalidadNuevo;
        public List<vw_Mortalidad> ListaMortalidadNuevo
        {
            get
            {
                if (_ListaMortalidadNuevo == null)
                {


                    return _ListaMortalidadNuevo = new List<vw_Mortalidad>();

                }
                else
                {
                    return _ListaMortalidadNuevo;
                }

            }

            set
            {
                _ListaMortalidadNuevo = value;
            }
        }

        private List<vw_Mortalidad_sin> _ListaMortalidadSinFechas;
        public List<vw_Mortalidad_sin> ListaMortalidadSinFechas
        {
            get
            {
                if (_ListaMortalidadSinFechas == null)
                {
                    _ListaMortalidadSinFechas = BusClass.ConsultaMortalidadSin(ref MsgRes);
                    _ListaMortalidadSinFechas = _ListaMortalidadSinFechas.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _ListaMortalidadSinFechas;
                }
                else
                {
                    return _ListaMortalidadSinFechas;
                }

            }

            set
            {
                _ListaMortalidadSinFechas = value;
            }
        }


        private List<vw_consulta_alertas> _GetConsultaAlertas;
        public List<vw_consulta_alertas> GetConsultaAlertas
        {
            get
            {
                if (_GetConsultaAlertas == null)
                {

                    _GetConsultaAlertas = BusClass.GetConsultaAlertas();
                    _GetConsultaAlertas = _GetConsultaAlertas.Where(X => X.estado == "A").ToList();
                    return _GetConsultaAlertas;
                }
                else
                {
                    return _GetConsultaAlertas;
                }

            }

            set
            {
                _GetConsultaAlertas = value;
            }
        }

        private List<ManagmentAlertasCalidadResult> _ListaAlertas;
        public List<ManagmentAlertasCalidadResult> ListaAlertas
        {
            get
            {
                if (_ListaAlertas == null)
                {
                    return _ListaAlertas = new List<ManagmentAlertasCalidadResult>();
                }
                else
                {
                    return _ListaAlertas;
                }

            }

            set
            {
                _ListaAlertas = value;
            }
        }

        private List<vw_sis_auditor_ciudad> _ListCiudadAuditor;
        public List<vw_sis_auditor_ciudad> ListCiudadAuditor
        {
            get
            {
                if (_ListCiudadAuditor == null)
                {
                    _ListCiudadAuditor = BusClass.GetCiudadesAuditor();
                    _ListCiudadAuditor = _ListCiudadAuditor.Where(x => x.id_auditor == SesionVar.IDUser).ToList();


                    return _ListCiudadAuditor;
                }
                else
                {
                    return _ListCiudadAuditor;
                }

            }

            set
            {
                _ListCiudadAuditor = value;
            }
        }

        private List<sis_auditor_regional> _ListRegional;
        public List<sis_auditor_regional> ListRegional
        {
            get
            {
                if (_ListRegional == null)
                {
                    _ListRegional = BusClass.GetRegionalAuditor();
                    _ListRegional = _ListRegional.Where(x => x.id_auditor == SesionVar.IDUser).ToList();


                    return _ListRegional;
                }
                else
                {
                    return _ListRegional;
                }

            }

            set
            {
                _ListRegional = value;
            }
        }

        private List<vw_Devoluciones_sin_gestionar> _ListDevSinGestionar;
        public List<vw_Devoluciones_sin_gestionar> ListDevSinGestionar
        {
            get
            {
                if (_ListDevSinGestionar == null)
                {
                    if (SesionVar.ROL == "1")
                    {
                        _ListDevSinGestionar = BusClass.DevolucionesSinGestion();
                        return _ListDevSinGestionar;
                    }
                    else
                    {

                        List<vw_Devoluciones_sin_gestionar> lista = new List<vw_Devoluciones_sin_gestionar>();
                        vw_Devoluciones_sin_gestionar obj = new vw_Devoluciones_sin_gestionar();

                        foreach (var item in ListRegional)
                        {
                            _ListDevSinGestionar = BusClass.DevolucionesSinGestion();
                            _ListDevSinGestionar = _ListDevSinGestionar.Where(x => x.id_ref_regional == item.id_regional).ToList();

                            foreach (var item2 in _ListDevSinGestionar)
                            {
                                obj.id_devolucion_factura = Convert.ToInt32(item2.id_devolucion_factura);
                                obj.NumeroFactura = Convert.ToString(item2.NumeroFactura);
                                obj.Nit = Convert.ToString(item2.Nit);
                                obj.Prestador = Convert.ToString(item2.Prestador);
                                obj.fecha_devolucion = Convert.ToDateTime(item2.fecha_devolucion);
                                obj.id_ciudad = Convert.ToInt32(item2.id_ciudad);
                                obj.ciudad = Convert.ToString(item2.ciudad);
                                obj.Auditor = Convert.ToString(item2.Auditor);

                                lista.Add(obj);
                                obj = new vw_Devoluciones_sin_gestionar();
                            }


                        }
                        _ListDevSinGestionar = lista;
                    }
                    return _ListDevSinGestionar;
                }
                else
                {
                    return _ListDevSinGestionar;
                }

            }

            set
            {
                _ListDevSinGestionar = value;
            }
        }

        private List<vw_facturas_sin_auditar> _ListFacturasSin;
        public List<vw_facturas_sin_auditar> ListFacturasSin
        {
            get
            {
                if (_ListFacturasSin == null)
                {
                    if (SesionVar.ROL == "1")
                    {
                        _ListFacturasSin = BusClass.FacturasporAuditar();

                        return _ListFacturasSin;
                    }
                    else
                    {
                        List<vw_facturas_sin_auditar> lista = new List<vw_facturas_sin_auditar>();
                        vw_facturas_sin_auditar obj = new vw_facturas_sin_auditar();

                        foreach (var item in ListRegional)
                        {
                            _ListFacturasSin = BusClass.FacturasporAuditar();
                            _ListFacturasSin = _ListFacturasSin.Where(x => x.id_ref_regional == item.id_regional).ToList();

                            foreach (var item2 in _ListFacturasSin)
                            {
                                obj.id_factura_sin_censo = Convert.ToInt32(item2.id_factura_sin_censo);
                                obj.numero_factura = Convert.ToString(item2.numero_factura);
                                obj.fecha_factura = Convert.ToDateTime(item2.fecha_factura);
                                obj.fecha_recepcion = Convert.ToDateTime(item2.fecha_recepcion);
                                obj.Nit = Convert.ToString(item2.Nit);
                                obj.ips = Convert.ToString(item2.ips);
                                obj.tipo_factura = Convert.ToString(item2.tipo_factura);
                                obj.valor_factura = Convert.ToString(item2.valor_factura);
                                obj.tipo = Convert.ToString(item2.tipo);
                                obj.usuario_digita = Convert.ToString(item2.usuario_digita);
                                obj.digita_fecha = Convert.ToDateTime(item2.digita_fecha);

                                lista.Add(obj);
                                obj = new vw_facturas_sin_auditar();
                            }


                        }
                        _ListFacturasSin = lista;

                    }
                    return _ListFacturasSin;
                }
                else
                {
                    return _ListFacturasSin;
                }

            }

            set
            {
                _ListFacturasSin = value;
            }
        }

        private List<vw_hallazgos_RIPS> _ListHallazgoRIPS;
        public List<vw_hallazgos_RIPS> ListHallazgoRIPS
        {
            get
            {
                if (_ListHallazgoRIPS == null)
                {
                    if (SesionVar.ROL == "1")
                    {
                        _ListHallazgoRIPS = BusClass.HallazgosRipsSinGestion();

                        return _ListHallazgoRIPS;
                    }
                    else
                    {
                        List<vw_hallazgos_RIPS> lista = new List<vw_hallazgos_RIPS>();
                        vw_hallazgos_RIPS obj = new vw_hallazgos_RIPS();

                        foreach (var item in ListRegional)
                        {
                            _ListHallazgoRIPS = BusClass.HallazgosRipsSinGestion();
                            _ListHallazgoRIPS = _ListHallazgoRIPS.Where(x => x.id_ref_regional == item.id_regional).ToList();

                            foreach (var item2 in _ListHallazgoRIPS)
                            {
                                obj.id_hallazgo_RIPS = Convert.ToInt32(item2.id_hallazgo_RIPS);
                                obj.numero_factura = Convert.ToString(item2.numero_factura);
                                obj.fecha_reporte_hallazgo = Convert.ToDateTime(item2.fecha_reporte_hallazgo);
                                obj.fecha_recepcion = Convert.ToDateTime(item2.fecha_recepcion);
                                obj.Nit = Convert.ToString(item2.Nit);
                                obj.Prestador = Convert.ToString(item2.Prestador);
                                obj.fecha_digita = Convert.ToDateTime(item2.fecha_digita);
                                obj.usuario_digita = Convert.ToString(item2.usuario_digita);

                                lista.Add(obj);
                                obj = new vw_hallazgos_RIPS();
                            }
                            _ListHallazgoRIPS = lista;
                        }
                        return _ListHallazgoRIPS;
                    }
                }
                else
                {
                    return _ListHallazgoRIPS;
                }

            }

            set
            {
                _ListHallazgoRIPS = value;
            }
        }



        private List<Ref_regional> _refRegional;
        public List<Ref_regional> RefRegional
        {
            get
            {
                if (_refRegional == null)
                {
                    _refRegional = BusClass.GetRefRegion();

                    return _refRegional;
                }
                else
                {
                    return _refRegional;
                }

            }

            set
            {
                _refRegional = value;
            }
        }

        private List<vw_ECOPETROL_DEVOLUCION_FAC> _LstDevoluciones;
        public List<vw_ECOPETROL_DEVOLUCION_FAC> LstDevoluciones
        {
            get
            {
                if (_LstDevoluciones == null)
                {
                    _LstDevoluciones = BusClass.VwDevoluciones();
                    _LstDevoluciones = _LstDevoluciones.Where(x => x.Fecha_Devolucion >= fecha_inicio && x.Fecha_Devolucion <= fecha_fin).ToList();

                    return _LstDevoluciones;
                }
                else
                {
                    return _LstDevoluciones;
                }

            }

            set
            {
                _LstDevoluciones = value;
            }
        }

        private List<vw_ECOPETROL_HALLAZGOS_RIPS> _LstHallazgos;
        public List<vw_ECOPETROL_HALLAZGOS_RIPS> LstHallazgos
        {
            get
            {
                if (_LstDevoluciones == null)
                {
                    _LstHallazgos = BusClass.VwHallazgosRIPS();
                    _LstHallazgos = _LstHallazgos.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_fin).ToList();

                    return _LstHallazgos;
                }
                else
                {
                    return _LstHallazgos;
                }

            }

            set
            {
                _LstHallazgos = value;
            }
        }

        private List<ECOPETROL_RECEPCION_FACTURAS> _ListRecepcion;
        public List<ECOPETROL_RECEPCION_FACTURAS> ListRecepcion
        {
            get
            {
                if (_ListRecepcion == null)
                {
                    _ListRecepcion = BusClass.VwRecepcionFacturas();
                    _ListRecepcion = _ListRecepcion.Where(x => x.digita_fecha >= fecha_inicio && x.digita_fecha <= fecha_fin).ToList();

                    return _ListRecepcion;
                }
                else
                {
                    return _ListRecepcion;
                }

            }

            set
            {
                _ListRecepcion = value;
            }
        }

        private List<ECOPETROL_RECEPCION_FACTURAS> _ListRecepcionSin;
        public List<ECOPETROL_RECEPCION_FACTURAS> ListRecepcionSin
        {
            get
            {
                if (_ListRecepcionSin == null)
                {
                    _ListRecepcionSin = BusClass.VwRecepcionFacturas();
                    _ListRecepcionSin = _ListRecepcionSin.Where(x => x.auditada == "SI").ToList();
                    _ListRecepcionSin = _ListRecepcionSin.Where(x => x.digita_fecha >= fecha_inicio && x.digita_fecha <= fecha_fin).ToList();

                    return _ListRecepcionSin;
                }
                else
                {
                    return _ListRecepcionSin;
                }

            }

            set
            {
                _ListRecepcionSin = value;
            }
        }

        [Required(ErrorMessage = "***")]
        [Display(Name = "FECHA INICIO")]
        public Nullable<DateTime> fecha_inicio { get; set; }
        public Nullable<DateTime> fecha_iniciook { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        [Display(Name = "FECHA INICIO")]
        public Nullable<DateTime> fecha_inicial { get; set; }

        public int valor { get; set; }




        [Required(ErrorMessage = "***")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        [Display(Name = "FECHA FIN")]
        public Nullable<DateTime> fecha_fin { get; set; }
        public Nullable<DateTime> fecha_finok { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        [Display(Name = "FECHA FIN")]
        public Nullable<DateTime> fecha_final { get; set; }

        [Required(ErrorMessage = "***")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        [Display(Name = "FECHA EVOLUCION")]
        public Nullable<DateTime> fecha_evolucion { get; set; }

        [Required(ErrorMessage = "SELECCIONE UNA OPCION")]
        public String id_tipo_busqueda { get; set; }

        [Required(ErrorMessage = "SELECCIONE UNA OPCION")]
        [Display(Name = "SELECCIONE ALERTA:")]
        public String id_tipo_busqueda2 { get; set; }

        [Required(ErrorMessage = "SELECCIONE UNA OPCION")]
        public String id_filtro_busqueda { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "TIPO DOCUMENTO:")]
        public String tipo_identifi_afiliado { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "NUMERO DOCUMENTO:")]
        public String num_identifi_afil { get; set; }

        [Required(ErrorMessage = "***")]
        [Display(Name = "REGIONAL")]
        public Int32? regional { get; set; }

        public String NomRegional { get; set; }


        [Required(ErrorMessage = "***")]
        [Display(Name = "Tipo Archivo")]
        public string tipo_archivo { get; set; }

        public ICollection<vw_Devoluciones_sin_gestionar> Detalle { get; set; }



        #endregion

        #region METODOS

        public void CuentaFechaCargue(Int32 Opc)
        {
            ListaAlertas = BusClass.CuentaFechaCargue(Opc, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "0", "0", ref MsgRes);
        }

        public void CuentaFechaDevolucion(Int32 Opc)
        {
            ListaAlertas = BusClass.CuentaFechaCargue(Opc, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "0", "0", ref MsgRes);
        }

        public List<managmentRips_AC_FechaconsultaResult> GetListRipsFechaCosnsulta(DateTime FechaInicio, DateTime FechaFinal)
        {
            return BusClass.ConsultaRipsFechaConsulta(FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<managmentRips_AP_FechaProcedimientoResult> GetListRipsFechaProcedimiento(int? regional, DateTime FechaInicio, DateTime FechaFinal)
        {

            return BusClass.ConsultaRipsFechaProcedimeinto(regional, FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<vw_consulta_rips_an_fechanacimiento> GetListRipsFechaNacimiento(DateTime FechaInicio, DateTime FechaFinal)
        {

            return BusClass.GetListRipsFechaNacimiento(FechaInicio, FechaFinal, ref MsgRes);
        }

        public List<vw_consulta_rips_ah_mortalidad> GetListRipsMortalidadAH(DateTime? FechaInicial, DateTime? FechaFinal)
        {

            return BusClass.GetListRipsMortalidadAH(FechaInicial, FechaFinal, ref MsgRes);
        }

        public List<vw_consulta_rips_au_mortalidad> GetListRipsMortalidadAU(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            return BusClass.GetListRipsMortalidadAU(FechaInicial, FechaFinal, ref MsgRes);
        }

        public List<Management_Consulta_EcopResult> CargarListas(Int32 valor)
        {
            List<Management_Consulta_EcopResult> List = new List<Management_Consulta_EcopResult>();
            List<Management_Consulta_EcopResult> resultUno = new List<Management_Consulta_EcopResult>();
            List<Management_Consulta_EcopResult> resultDos = new List<Management_Consulta_EcopResult>();
            
            try
            {
                List = BusClass.ConsultaCensoConcurrenciaII(valor, regional, num_identifi_afil, fecha_inicio, fecha_fin, ref MsgRes);

                if (SesionVar.ROL == "25")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        resultUno = List.Where(x => x.id_regional_asa == item.id_regional).ToList();
                        resultDos = resultDos.Concat(resultUno).ToList();
                    }

                    ListaNuevasConsultas = resultDos;
                }
                else
                {
                    ListaNuevasConsultas = List;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return ListaNuevasConsultas;    
        }

        public List<Management_Consulta_EcopResult> CargarListas2(Int32 valor)
        {
            List<Management_Consulta_Ecop2Result> List = new List<Management_Consulta_Ecop2Result>();
            List<Management_Consulta_Ecop2Result> resultUno = new List<Management_Consulta_Ecop2Result>();
            List<Management_Consulta_Ecop2Result> resultDos = new List<Management_Consulta_Ecop2Result>();

            try
            {
                List = BusClass.ConsultaCensoConcurrenciaII2(valor, regional, num_identifi_afil, fecha_inicio, fecha_fin, ref MsgRes);

                if (SesionVar.ROL == "25")
                {
                    List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                    regional obj = new regional();
                    list = BusClass.GetRegionalAuditor();
                    list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                    foreach (var item in list)
                    {
                        resultUno = List.Where(x => x.id_regional_asa == item.id_regional).ToList();
                        resultDos = resultDos.Concat(resultUno).ToList();
                    }

                    ListaNuevasConsultas2 = resultDos;
                }
                else
                {
                    ListaNuevasConsultas2 = List;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return ListaNuevasConsultas;
        }


        public List<vw_gestantes> CargarListasGestantes(Int32 valor)
        {

            List<vw_gestantes> lista = new List<vw_gestantes>();
            List<vw_gestantes> result = new List<vw_gestantes>();
            List<vw_gestantes> lista2 = new List<vw_gestantes>();
            lista = BusClass.ConsultaGestantes(ref MsgRes);

            //_ListaGestantesNuevo = BusClass.ConsultaGestantes(ref MsgRes);

            if (SesionVar.ROL == "25")
            {
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                regional obj = new regional();
                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    result = lista.Where(x => x.regional_id == item.id_regional).ToList();
                    lista2 = lista2.Concat(result).ToList();
                }

                _ListaGestantesNuevo = lista2;
            }
            else
            {
                _ListaGestantesNuevo = lista;
            }

            if (valor == 1)
            {
                _ListaGestantesNuevo = _ListaGestantesNuevo.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_final).ToList();
            }
            else
            {

                _ListaGestantesNuevo = _ListaGestantesNuevo.Where(x => x.regional_id == regional).ToList();
                _ListaGestantesNuevo = _ListaGestantesNuevo.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_final).ToList();
            }

            return _ListaGestantesNuevo;


        }

        public List<vw_Mortalidad> CargarListasMortalidad(Int32 valor)
        {

            List<vw_Mortalidad> lista = new List<vw_Mortalidad>();
            List<vw_Mortalidad> result = new List<vw_Mortalidad>();
            List<vw_Mortalidad> lista2 = new List<vw_Mortalidad>();
            lista = BusClass.ConsultaMortalidad(ref MsgRes);

            //_ListaMortalidadNuevo = BusClass.ConsultaMortalidad(ref MsgRes);

            if (SesionVar.ROL == "25")
            {
                List<sis_auditor_regional> list = new List<sis_auditor_regional>();
                regional obj = new regional();
                list = BusClass.GetRegionalAuditor();
                list = list.Where(x => x.id_auditor == SesionVar.IDUser).ToList();

                foreach (var item in list)
                {
                    result = lista.Where(x => x.regional_id == item.id_regional).ToList();
                    lista2 = lista2.Concat(result).ToList();
                }

                _ListaMortalidadNuevo = lista2;
            }
            else
            {
                _ListaMortalidadNuevo = lista;
            }

            if (valor == 1)
            {

                _ListaMortalidadNuevo = _ListaMortalidadNuevo.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_final).ToList();

            }
            else
            {
                _ListaMortalidadNuevo = _ListaMortalidadNuevo.Where(x => x.regional_id == regional).ToList();
                _ListaMortalidadNuevo = _ListaMortalidadNuevo.Where(x => x.fecha_digita >= fecha_inicio && x.fecha_digita <= fecha_final).ToList();

            }


            return _ListaMortalidadNuevo;
        }


        public void InsertarMega(List<megas_cargue_base> ListMega)
        {
            BusClass.InsertarMega(ListMega, ref MsgRes);
        }

        public List<ref_consulta_ecopetrol> Ref_ConsultasEcopetrol()
        {
            return BusClass.Ref_ConsultasEcopetrol();
        }

        public List<ManagmentClinicosCensoConConcurrenciaResult> CensoConcurrenciaEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CensoConcurrenciaEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 21 de diciembre de 2022
        /// Metodo que reemplazara la consulta CensoConcurrenciaEcopetrol se añade consulta por SQL COMMAND
        /// </summary>
        /// <param name="fecha_ini"></param>
        /// <param name="fecha_final"></param>
        /// <param name="MsgRes"></param>
        /// <returns></returns>
        public DataTable CensoConcurrenciaEcopetrolII(DateTime fecha_ini, DateTime fecha_final, String Conexion, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CensoConcurrenciaEcopetrolII(fecha_ini, fecha_final, Conexion, ref MsgRes);
        }

        public List<ManagmentClinicosCensoResult> CensoEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.CensoEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }

        public List<ManagmentClinicosConsultasAlertasResult> AlertasEcopetrol(DateTime fecha_ini, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.AlertasEcopetrol(fecha_ini, fecha_final, ref MsgRes);
        }



        #endregion

        #region  FFMM


        public List<Management_FFMM_Consultas_cuentasResult> GetConsultaFFMMCuentas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentas(ref MsgRes);
        }

        public List<Management_FFMM_Consultas_ConcurreniaResult> GetConsultaFFMMConcurrencia(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMConcurrencia(ref MsgRes);
        }

        public List<Management_FFMM_Consultas_PADResult> GetConsultaFFMMPad(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMPad(ref MsgRes);
        }

        public List<Management_FFMM_consulta_cuentas_primeraResult> GetConsultaFFMMCuentasUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentasUno(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_segundaResult> GetConsultaFFMMCuentasDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentasDos(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_terceraResult> GetConsultaFFMMCuentasTres(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentasTres(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_cuartaResult> GetConsultaFFMMCuentasCuatro(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentasCuatro(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_cuentas_quintaResult> GetConsultaFFMMCuentasCinco(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMCuentasCinco(fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<Ref_ffmm_unudadR> FFMM_unudadR()
        {
            return BusClass.FFMM_unudadR();

        }

        public List<Management_FFMM_consulta_concurrencia_primeraResult> GetConsultaFFMMConcurrenciaUno(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMConcurrenciaUno(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_segundaResult> GetConsultaFFMMConcurrenciaDos(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMConcurrenciaDos(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_terceroResult> GetConsultaFFMMConcurrenciaTercero(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMConcurrenciaTercero(fecha_inicial, fecha_final, ref MsgRes);
        }
        public List<Management_FFMM_consulta_concurrencia_cuartoResult> GetConsultaFFMMConcurrenciaCuarto(DateTime fecha_inicial, DateTime fecha_final, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetConsultaFFMMConcurrenciaCuarto(fecha_inicial, fecha_final, ref MsgRes);
        }

        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetUnidadSatelite(ref MsgRes);
        }
        public List<Ref_ffmm_origen_servicio> FFMM_origen_servicio()
        {
            return BusClass.FFMM_origen_servicio();

        }

        #endregion
    }
}