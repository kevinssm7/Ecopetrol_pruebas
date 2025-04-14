using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using System.ComponentModel;
using LinqToExcel;
using System.Data;

namespace AsaludEcopetrol.Models.ProcesosInternos
{
    public class ProcesosInternos
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

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        #region METODOS

        #region Consulta

        public List<indicador_regional> ConsultarIndicadorRegionalList(Int32? idregional, ref MessageResponseOBJ MsgRes)
        {
            List<indicador_regional> result = BusClass.ConsultarIndicadorRegionalList(ref MsgRes);
            if (idregional != null)
            {
                result = result.Where(l => l.id_regional == idregional.Value).ToList();
            }
            return result;
        }

        public List<vw_visitas> ConsultaCronogramaVisitas(Int32? idcronograma)
        {
            return BusClass.ConsultaCronogramaVisitas(idcronograma, ref MsgRes);
        }

        public List<Management_Consulta_Cronograma_VisitasResult> ConsultaCronogramaVisitasProcedimiento(int tipoFiltro, string Auditor)
        {
            return BusClass.ConsultaCronogramaVisitasProcedimiento(tipoFiltro, Auditor);
        }

        public List<cronograma_visita_detalle> ConsultaCronogramaVisitaDetalle(int idcronograma)
        {
            return BusClass.ConsultaCronogramaVisitaDetalle(idcronograma);
        }

        public List<cronograma_visita_detalle_indicador> ConsultaCronogramaVisitaDetalleInd(int idcronograma)
        {
            return BusClass.ConsultaCronogramaVisitaDetalleInd(idcronograma);
        }

        public cronograma_visitas getvisitabyid(Int32 idvisita)
        {
            return BusClass.getvisitabyid(idvisita, ref MsgRes);
        }

        public List<indicadores> ConsultarIndicadores(int? idindicador)
        {
            return BusClass.ConsultarIndicadores(idindicador, ref MsgRes);
        }

        public List<capitulos> GetCapitulosList()
        {
            return BusClass.GetCapitulosList();
        }

        public List<capitulo_indicador> GetCapitulosByIndicador(Int32? idindicador, Int32 idregioanl)
        {
            return BusClass.GetCapitulosByIndicador(idindicador, idregioanl, ref MsgRes);
        }

        public capitulo_indicador getcapbyindicadorcap(int idcapitulo, int idindicador, int idregional)
        {
            return BusClass.getcapbyindicadorcap(idcapitulo, idindicador, idregional);
        }

        public List<item_capitulo> Getitemcapbyindcap(Int32 idregional, Int32 idindicador, Int32? idcap)
        {
            return BusClass.Getitemcapbyindcap(idregional, idindicador, idcap);
        }

        public List<cronograma_visita_detalle> Getdetalllescronograma(int idcronograma)
        {
            return BusClass.Getdetalllescronograma(idcronograma);
        }

        public item_capitulo Getitemcapbyid(Int32 IdItem)
        {
            return BusClass.Getitemcapbyid(IdItem);
        }

        public capitulos Getcapitulobyid(Int32 idcapitulo)
        {
            return BusClass.Getcapitulobyid(idcapitulo);
        }

        public Ref_RIPS_Prestadores getPrestador(double codprestador)
        {
            return BusClass.getPrestador(codprestador, ref MsgRes);
        }

        public List<ref_usuario_responsable> ConsultResponsablesbyusuario(Int32 idusuario)
        {
            return BusClass.ConsultResponsablesbyusuario(idusuario, ref MsgRes);
        }

        public List<Ref_analaisis_caso_ambito> LstAmbito()
        {
            return BusClass.Getambito();

        }

        public List<sis_usuario> LstResponsables()
        {
            return BusClass.LstResponsables();
        }

        public List<calidad_prestadores> getprestadoresList(string term, int? tipofiltro)
        {
            List<calidad_prestadores> prestadores = BusClass.getprestadoresList();
            if (!string.IsNullOrEmpty(term) && tipofiltro != null)
            {
                switch (tipofiltro)
                {
                    case 1:
                        prestadores = prestadores.Where(l => l.cod_sap.ToUpper().Contains(term.ToUpper())).ToList();
                        break;
                    case 2:
                        prestadores = prestadores.Where(l => l.no_id_prestador.ToUpper().Contains(term.ToUpper())).ToList();
                        break;
                    case 3:
                        prestadores = prestadores.Where(l => l.razon_social.ToUpper().Contains(term.ToUpper())).ToList();
                        break;
                    default:
                        prestadores = new List<calidad_prestadores>();
                        break;
                }
            }

            return prestadores;
        }

        public calidad_prestadores getPresadorById(int idprestador)
        {
            return BusClass.getPresadorById(idprestador);
        }

        public List<Ref_calidad_municipios> GetMunicipiosDane()
        {
            return BusClass.GetMunicipiosDane();
        }

        public List<vw_visitas> GetListVisitas(Int32? id_visita, Int32? id_prestador, string numcontrato)
        {
            return BusClass.GetListVisitas(id_visita, id_prestador, numcontrato);
        }

        public List<ref_cronograma_visitas_novedades> GetListTipoNovedad()
        {
            return BusClass.GetListTipoNovedad();
        }

        #endregion

        #region Insercion

        public void InsertarCronogramaVisitas(cronograma_visitas objcronograma)
        {
            BusClass.InsertarCronogramaVisitas(objcronograma, ref MsgRes);
        }

        public bool InsertarItemCapitulo(item_capitulo itemcapitulo)
        {
            return BusClass.InsertarItemCapitulo(itemcapitulo);
        }

        public bool InsertaCapitulo(capitulos capitulo)
        {
            return BusClass.InsertaCapitulo(capitulo);
        }

        public void InsertarPrestador(calidad_prestadores Obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarPrestador(Obj, ref MsgRes);
        }

        public int ActualizarContratosPrestadorVisitas(string sap, DateTime? fechaInicio, DateTime? fechaFin, string numContrato, int? tipo)
        {
            return BusClass.ActualizarContratosPrestadorVisitas(sap, fechaInicio, fechaFin, numContrato, tipo);
        }

        public void InsertarVisita(cronograma_visitas ObjCronocrama)
        {
            BusClass.InsertarVisita(ObjCronocrama, ref MsgRes);
        }


        public void insertaRegionalPrestadores(Int32 idregional, List<int> prestadores)
        {
            BusClass.insertaRegionalPrestadores(idregional, prestadores);
        }

        public void InsertarCapitulosPrestador(Int32 idregional, Int32 idindicador, List<int> capitulos)
        {
            BusClass.InsertarCapitulosPrestador(idregional, idindicador, capitulos);
        }

        public Int32 InsertarCargueRanking(calidad_cargue_ranking ranking)
        {
            return BusClass.InsertarCargueRanking(ranking);
        }

        public void InsertarDetalleCargueRanking(List<calidad_detalle_cargue_ranking> detalleranking)
        {
            BusClass.InsertarDetalleCargueRanking(detalleranking);
        }

        public void InsertarNovedadVisita(cronograma_visita_novedades obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarNovedadVisita(obj, ref MsgRes);
        }

        public void InsertarMasivamenteReportesEvaluacionVisitas(List<cronograma_visitas_reportesDoc_evaluacion_calidad> obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.InsertarMasivamenteReportesEvaluacionVisitas(obj, ref MsgRes);
        }

        public Management_get_info_visita_por_idResult GetInfoVisitaById(int idCronograma)
        {
            return BusClass.GetInfoVisitaById(idCronograma);
        }
        #endregion

        #region Actualizacion

        public void ActualizarCronogramaVisitas(cronograma_visitas objcronograma, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizarCronogramaVisitas(objcronograma, ref MsgRes);
        }

        public void insertardetallevisita(int id_cronograma, int id_regional, int id_indicador, List<item_capitulo> listadoitems, ref MessageResponseOBJ MsgRes)
        {
            BusClass.insertardetallevisita(id_cronograma, id_regional, id_indicador, listadoitems, ref MsgRes);
        }

        public void insertarcalificacionesvisita(int idcronograma, string[] calificaciones, ref MessageResponseOBJ MsgRes)
        {
            BusClass.insertarcalificacionesvisita(idcronograma, calificaciones, ref MsgRes);
        }

        public int insertardetallevisitaindicador(List<capitulo_indicador> capitulos, int idcronograma, int idprestador, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.insertardetallevisitaindicador(capitulos, idcronograma, idprestador, ref MsgRes);
        }

        public bool ActualizarItemCapitulo(item_capitulo objitem)
        {
            return BusClass.ActualizarItemCapitulo(objitem);
        }

        public List<calidad_ref_especialidad> GetRefEspecialidadList()
        {
            return BusClass.GetRefEspecialidadList();
        }

        public List<calidad_ref_regimen> GetRefRegimentList()
        {
            return BusClass.GetRefRegimentList();
        }


        public List<Ref_clase_persona> GetClasePersonaList()
        {
            return BusClass.GetClasePersonaList();
        }

        public bool ActualizarCapituloIndicador(Int32 idcapituloIndicador, int pesogen)
        {
            return BusClass.ActualizarCapituloIndicador(idcapituloIndicador, pesogen);
        }


        public void actualizarPrestador(calidad_prestadores Obj, int idprestador)
        {
            BusClass.actualizarPrestador(Obj, idprestador);
        }

        public List<vw_calidad_prestador_indicador> GetListIndicadoresPrestador(int id_prestador)
        {
            return BusClass.GetListIndicadoresPrestador(id_prestador);
        }


        #endregion

        #region Elimincacion


        public void EliminarCapitulo(int idcapitulo)
        {
            BusClass.EliminarCapitulo(idcapitulo);
        }

        public void EliminarVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarVisita(idvisita, obj, ref MsgRes);
        }

        public void EliminarEvaluacionVisita(Int32 idvisita, log_eliminacion_visitas obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.EliminarEvaluacionVisita(idvisita, obj, ref MsgRes);
        }

        #endregion

        #region metodos extra
        public string GetDescription(CondicionesMeta value)
        {
            System.Reflection.FieldInfo oFieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public string GetDescriptionTipofiltro(TipoFiltroCronogramaVisitas value)
        {
            System.Reflection.FieldInfo oFieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public string MsgRestpuesta(string tipomensaje, string msjprincipal, string mensaeje)
        {
            string result = "";
            result += "<div class='alert alert-" + tipomensaje + " row'>" +
                   "<strong>" + msjprincipal + "&nbsp; </strong>" + mensaeje + " </div>";
            return result;
        }

        public void GuardarActaVisitas(cronograma_visita_documento obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.GuardarActaVisitas(obj, ref MsgRes);
        }


        public void GuardarInformeOperativo(cronograma_visita_informeOperativo obj, ref MessageResponseOBJ MsgRes)
        {
            BusClass.GuardarInformeOperativo(obj, ref MsgRes);
        }


        public cronograma_visita_documento Getactavisita(int idvisita)
        {
            return BusClass.Getactavisita(idvisita);
        }

        public management_cronograma_visita_documento_idResult Getactavisita2(int idvisita)
        {
            return BusClass.Getactavisita2(idvisita);
        }

        public cronograma_visita_documento Getactavisitas(int idvisita)
        {
            return BusClass.Getactavisita(idvisita);
        }

        public List<vw_visitas_documentos> GetActasVisitas(int regional, int mes, int año)
        {
            return BusClass.GetActasVisitas(regional, mes, año);
        }

        public List<ManagementConsultaGnralVisitasResult> GetConsultageneralVisitas(int regional, int prestador, DateTime fecha, string nit, string codsap)
        {
            return BusClass.GetConsultageneralVisitas(regional, prestador, fecha, nit, codsap);
        }

        public cronograma_visita_detalle Getresultadovisitaindicador(int idvisita, int idindicador)
        {
            return BusClass.Getresultadovisitaindicador(idvisita, idindicador);
        }

        public double? ObtenerPuntuacion(string condicion_meta, double? Valor_digitado, double? valor_meta, decimal? Peso_individual)
        {
            string[] result = new string[2];
            double? puntuacion = 0;
            string signo_condicion = "";
            #region
            switch (condicion_meta)
            {
                case "1":
                    signo_condicion = "=";
                    if (Valor_digitado == valor_meta)
                    {
                        puntuacion = Convert.ToDouble(Peso_individual);
                    }
                    else
                    {
                        if (valor_meta > 0.5)
                        {
                            puntuacion = ((Valor_digitado * Convert.ToDouble(Peso_individual)) / 100);
                        }
                        else
                        {
                            puntuacion = 0;
                        }
                    }
                    break;
                case "2":
                    signo_condicion = "<=";
                    if (Valor_digitado <= valor_meta)
                    {
                        puntuacion = Convert.ToDouble(Peso_individual);
                    }
                    else
                    {

                        if (valor_meta > 0.5)
                        {
                            if (valor_meta < Valor_digitado)
                            {
                                puntuacion = 0;
                            }
                            else
                            {
                                puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                            }
                        }
                        else
                        {
                            puntuacion = 0;
                        }
                    }
                    break;
                case "3":
                    signo_condicion = "<";
                    if (Valor_digitado < valor_meta)
                    {
                        puntuacion = Convert.ToDouble(Peso_individual);
                    }
                    else
                    {
                        if (valor_meta > 0.5)
                        {
                            if (valor_meta < Valor_digitado)
                            {
                                puntuacion = 0;
                            }
                            else
                            {
                                puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                            }
                        }
                        else
                        {
                            puntuacion = 0;
                        }
                    }
                    break;
                case "4":
                    signo_condicion = ">=";
                    if (Valor_digitado >= valor_meta)
                    {
                        puntuacion = Convert.ToDouble(Peso_individual);
                    }
                    else
                    {
                        puntuacion = ((Valor_digitado / valor_meta) * Convert.ToDouble(Peso_individual));
                    }
                    break;
                default:
                    signo_condicion = ">";
                    if (Valor_digitado > valor_meta)
                    {
                        puntuacion = Convert.ToDouble(Peso_individual);
                    }
                    else
                    {
                        if (valor_meta > 0.5)
                        {
                            puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                        }
                        else
                        {
                            puntuacion = 0;
                        }
                    }
                    break;
            }

            #endregion

            return puntuacion;
        }

        public double ObtenerResultadosPuntuacionMaxima(string p, decimal Peso_individual)
        {
            double puntuacion = Convert.ToDouble(p);

            if (puntuacion > 0)
            {
                return (puntuacion * 100) / Convert.ToDouble(Peso_individual);
            }
            else
            {
                return (puntuacion * 100);
            }
        }

        public string ObtenerSignoCondicion(int idItem, string condicion_meta)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            item_capitulo item = db.item_capitulo.Where(l => l.id_item == idItem).FirstOrDefault();

            string signo_condicion = "";

            if (item.condicion_especial == false)
            {
                switch (condicion_meta)
                {
                    case "1":
                        signo_condicion = "= " + item.valor_meta + "%";
                        break;
                    case "2":
                        signo_condicion = "<= " + item.valor_meta + "%";
                        break;
                    case "3":
                        signo_condicion = "< " + item.valor_meta + "%";
                        break;
                    case "4":
                        signo_condicion = ">= " + item.valor_meta + " % ";
                        break;
                    default:
                        signo_condicion = "> " + item.valor_meta + "%";
                        break;
                }
            }
            else
            {
                var condiciones = db.item_capitulo_condiciones_especiales.Where(l =>
                    l.tipo_prestador.ToUpper().StartsWith(item.indicadores.nom_indicador.ToUpper()) &&
                    l.capitulo.ToUpper().StartsWith(item.capitulos.nom_capitulo.ToUpper()) &&
                    l.nombre_indicador.ToUpper().StartsWith(item.nom_item.ToUpper())).ToList();

                int cont = 0;

                var cumple_puntuacion = condiciones.Where(l => l.accion == "PUNTUACION").ToList();
                foreach (var condicion in cumple_puntuacion)
                {
                    cont++;
                    switch (condicion.condicion_meta.ToString())
                    {
                        case "1":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%)";
                            }

                            break;
                        case "2":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%)";
                            }
                            break;
                        case "3":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%)";
                            }
                            break;
                        case "4":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%)";
                            }
                            break;
                        case "5":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (" + item.Peso_individual + "%)";
                            }
                            break;
                        case "6":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + item.Peso_individual + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + item.Peso_individual + "%)";
                            }
                            break;
                    }

                }

                var cumple_mitad = condiciones.Where(l => l.accion == "MITAD").ToList();
                foreach (var condicion in cumple_mitad)
                {
                    cont++;
                    switch (condicion.condicion_meta.ToString())
                    {
                        case "1":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }

                            break;
                        case "2":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }
                            break;
                        case "3":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }
                            break;
                        case "4":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }
                            break;
                        case "5":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "%(" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }
                            break;
                        case "6":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + Convert.ToDouble(item.Peso_individual / 2) + "%)";
                            }
                            break;
                    }

                }

                var cumple_cuarto = condiciones.Where(l => l.accion == "CUARTO").ToList();
                foreach (var condicion in cumple_cuarto)
                {
                    double puntuacion = Convert.ToDouble(((item.Peso_individual * 30) / 100));
                    puntuacion = Math.Round(puntuacion, 1);

                    cont++;
                    switch (condicion.condicion_meta.ToString())
                    {
                        
                        case "1":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1)  + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }

                            break;
                        case "2":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }
                            break;
                        case "3":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }
                            break;
                        case "4":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }
                            break;
                        case "5":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "%(" + Math.Round(puntuacion, 1) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }
                            break;
                        case "6":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + Math.Round(puntuacion, 1) + "%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (" + Math.Round(puntuacion, 1) + "%)";
                            }
                            break;
                    }

                }

                var cumple_cero = condiciones.Where(l => l.accion == "CERO").ToList();
                foreach (var condicion in cumple_cero)
                {
                    cont++;
                    switch (condicion.condicion_meta.ToString())
                    {
                        case "1":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "= " + condicion.valor_meta_uno + "% (0%)";
                            }

                            break;
                        case "2":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "<= " + condicion.valor_meta_uno + "% (0%)";
                            }
                            break;
                        case "3":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "< " + condicion.valor_meta_uno + "% (0%)";
                            }
                            break;
                        case "4":
                            if (cont < condiciones.Count)
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += ">= " + condicion.valor_meta_uno + "% (0%)";
                            }
                            break;
                        case "5":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "> " + condicion.valor_meta_uno + "% (0%)";
                            }
                            break;
                        case "6":

                            if (cont < condiciones.Count)
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (0%) Ó ";
                            }
                            else
                            {
                                signo_condicion += "Entre " + condicion.valor_meta_uno + "% Y " + condicion.valor_meta_dos + "% (0%)";
                            }
                            break;
                    }
                }

            }

            return signo_condicion;
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// </summary>
        /// <param name="condicion_meta"></param>
        /// <param name="Valor_digitado"></param>
        /// <param name="valor_meta"></param>
        /// <param name="Peso_individual"></param>
        /// <returns></returns>
        public double? ObtenerPuntuacionNuevoFormato(int idItem, string condicion_meta, double? Valor_digitado, double? valor_meta, decimal? Peso_individual)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            item_capitulo item = db.item_capitulo.Where(l => l.id_item == idItem).FirstOrDefault();

            string[] result = new string[2];
            double? puntuacion = 0;
            string signo_condicion = "";

            try
            {
                if (item.condicion_especial.Value)
                {
                    var condiciones = db.item_capitulo_condiciones_especiales.Where(l =>
                        l.tipo_prestador.ToUpper().StartsWith(item.indicadores.nom_indicador.ToUpper()) &&
                        l.capitulo.ToUpper().StartsWith(item.capitulos.nom_capitulo.ToUpper()) &&
                        l.nombre_indicador.ToUpper().StartsWith(item.nom_item.ToUpper())).ToList();


                    var cumple_puntuacion = condiciones.Where(l => l.accion == "PUNTUACION").ToList();
                    foreach (var condicion in cumple_puntuacion)
                    {
                        switch (condicion.condicion_meta)
                        {
                            case 1:
                                if (Valor_digitado == condicion.valor_meta_uno)
                                {
                                    signo_condicion = "=";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                            case 2:
                                if (Valor_digitado <= condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<=";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                            case 3:
                                if (Valor_digitado < condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                            case 4:
                                if (Valor_digitado >= condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">=";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                            case 5:
                                if (Valor_digitado > condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                            case 6:
                                if (Valor_digitado >= condicion.valor_meta_uno && Valor_digitado <= condicion.valor_meta_dos)
                                {
                                    signo_condicion = "-";
                                    puntuacion = Convert.ToDouble(Peso_individual);
                                }
                                break;
                        }

                    }

                    var cumple_mitad = condiciones.Where(l => l.accion == "MITAD").ToList();
                    foreach (var condicion in cumple_mitad)
                    {
                        switch (condicion.condicion_meta)
                        {
                            case 1:
                                if (Valor_digitado == condicion.valor_meta_uno)
                                {
                                    signo_condicion = "=";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                            case 2:
                                if (Valor_digitado <= condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<=";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                            case 3:
                                if (Valor_digitado < condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                            case 4:
                                if (Valor_digitado >= condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">=";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                            case 5:
                                if (Valor_digitado > condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                            case 6:
                                if (Valor_digitado >= condicion.valor_meta_uno && Valor_digitado <= condicion.valor_meta_dos)
                                {
                                    signo_condicion = "-";
                                    puntuacion = Convert.ToDouble((Peso_individual / 2));
                                }
                                break;
                        }

                    }

                    var cumple_cuarto = condiciones.Where(l => l.accion == "CUARTO").ToList();
                    foreach (var condicion in cumple_cuarto)
                    {
                        switch (condicion.condicion_meta)
                        {
                            case 1:
                                if (Valor_digitado == condicion.valor_meta_uno)
                                {
                                    signo_condicion = "=";
                                    puntuacion = Convert.ToDouble(((Peso_individual * 30) /100));
                                }
                                break;
                            case 2:
                                if (Valor_digitado <= condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<=";
                                    puntuacion = Convert.ToDouble(((Peso_individual * 30) / 100));
                                }
                                break;
                            case 3:
                                if (Valor_digitado < condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<";
                                    puntuacion = Convert.ToDouble(((Peso_individual * 30) / 100));
                                }
                                break;
                            case 4:
                                if (Valor_digitado >= condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">=";
                                    puntuacion = Convert.ToDouble(((Peso_individual * 30) / 100));
                                }
                                break;
                            case 5:
                                if (Valor_digitado > condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">";
                                    puntuacion = Convert.ToDouble(((Peso_individual * 30) / 100));
                                }
                                break;
                            case 6:
                                if (Valor_digitado >= condicion.valor_meta_uno && Valor_digitado <= condicion.valor_meta_dos)
                                {
                                    signo_condicion = "-";
                                    puntuacion =Convert.ToDouble(( (Peso_individual * 30) / 100));
                                }
                                break;
                        }

                        if(puntuacion != null)
                        {
                            puntuacion = Math.Round(puntuacion.Value, 1);
                        }
                    }

                    var cumple_cero = condiciones.Where(l => l.accion == "CERO").ToList();
                    foreach (var condicion in cumple_cero)
                    {
                        switch (condicion.condicion_meta)
                        {
                            case 1:
                                if (Valor_digitado == condicion.valor_meta_uno)
                                {
                                    signo_condicion = "=";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                            case 2:
                                if (Valor_digitado <= condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<=";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                            case 3:
                                if (Valor_digitado < condicion.valor_meta_uno)
                                {
                                    signo_condicion = "<";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                            case 4:
                                if (Valor_digitado >= condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">=";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                            case 5:
                                if (Valor_digitado > condicion.valor_meta_uno)
                                {
                                    signo_condicion = ">";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                            case 6:
                                if (Valor_digitado >= condicion.valor_meta_uno && Valor_digitado <= condicion.valor_meta_dos)
                                {
                                    signo_condicion = "-";
                                    puntuacion = Convert.ToDouble(0);
                                }
                                break;
                        }
                    }

                }
                else
                {
                    switch (condicion_meta)
                    {
                        case "1":
                            signo_condicion = "=";
                            if (Valor_digitado == valor_meta)
                            {
                                puntuacion = Convert.ToDouble(Peso_individual);
                            }
                            else
                            {
                                if (valor_meta > 0.5)
                                {
                                    puntuacion = ((Valor_digitado * Convert.ToDouble(Peso_individual)) / 100);
                                }
                                else
                                {
                                    puntuacion = 0;
                                }
                            }
                            break;
                        case "2":
                            signo_condicion = "<=";
                            if (Valor_digitado <= valor_meta)
                            {
                                puntuacion = Convert.ToDouble(Peso_individual);
                            }
                            else
                            {

                                if (valor_meta > 0.5)
                                {
                                    if (valor_meta < Valor_digitado)
                                    {
                                        puntuacion = 0;
                                    }
                                    else
                                    {
                                        puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                                    }
                                }
                                else
                                {
                                    puntuacion = 0;
                                }
                            }
                            break;
                        case "3":
                            signo_condicion = "<";
                            if (Valor_digitado < valor_meta)
                            {
                                puntuacion = Convert.ToDouble(Peso_individual);
                            }
                            else
                            {
                                if (valor_meta > 0.5)
                                {
                                    if (valor_meta < Valor_digitado)
                                    {
                                        puntuacion = 0;
                                    }
                                    else
                                    {
                                        puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                                    }
                                }
                                else
                                {
                                    puntuacion = 0;
                                }
                            }
                            break;
                        case "4":
                            signo_condicion = ">=";
                            if (Valor_digitado >= valor_meta)
                            {
                                puntuacion = Convert.ToDouble(Peso_individual);
                            }
                            else
                            {
                                puntuacion = ((Valor_digitado / valor_meta) * Convert.ToDouble(Peso_individual));
                            }
                            break;
                        default:
                            signo_condicion = ">";
                            if (Valor_digitado > valor_meta)
                            {
                                puntuacion = Convert.ToDouble(Peso_individual);
                            }
                            else
                            {
                                if (valor_meta > 0.5)
                                {
                                    puntuacion = (Valor_digitado * Convert.ToDouble(Peso_individual)) / 100;
                                }
                                else
                                {
                                    puntuacion = 0;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return puntuacion;
        }
        #endregion

        #endregion

        #region cargueContratos
        public int CargueMasivodeContratos(DataTable dt, contratos_cargue cargue, ref MessageResponseOBJ MsgRes)
        {
            List<contratos_detalle> Listado = new List<contratos_detalle>();

            var RtaInsercion = 0;
            int fila = 1;
            string columna = "";
            var textError = "";

            try
            {
                try
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        contratos_detalle obj = new contratos_detalle();

                        fila++;
                        if ((item["Número de contrato"] != null && item["Número de contrato"] != "") || (item["Periodo Finalización"] != null && item["Periodo Finalización"] != "") ||
                            (item["Fecha Inicial"] != null && item["Fecha Inicial"] != ""))
                        {
                            var texto = "";
                            var numero = 0;
                            DateTime fechas = new DateTime();
                            decimal decima = new decimal();

                            columna = "Número de contrato";
                            texto = Convert.ToString(item["Número de contrato"]);

                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 99)
                                {
                                    obj.numero_contrato = Convert.ToString(item["Número de contrato"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 99 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Periodo Finalización";
                            texto = Convert.ToString(item["Periodo Finalización"]);

                            if (texto.All(char.IsDigit) && !string.IsNullOrEmpty(texto))
                            {
                                if (texto.Length <= 10)
                                {
                                    obj.periodo_finalizacion = Convert.ToString(item["Periodo Finalización"]).ToUpper();
                                }
                                else
                                {
                                    textError = columna + ", solo puede contener 99 caracteres.";
                                    throw new Exception(textError);
                                }
                            }
                            else
                            {
                                textError = columna + ", solo puede contener números.";
                                throw new Exception(textError);
                            }

                            columna = "Fecha Inicial";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha Inicial"]);
                                if (fechas != null)
                                {
                                    obj.fecha_inicio = Convert.ToDateTime(item["Fecha Inicial"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Fecha Final";
                            try
                            {
                                fechas = Convert.ToDateTime(item["Fecha Final"]);
                                if (fechas != null)
                                {
                                    obj.fecha_fin = Convert.ToDateTime(item["Fecha Final"]);
                                }
                                else
                                {
                                    textError = columna + ", no puede ir vacio";
                                    throw new Exception(textError);
                                }
                            }
                            catch (Exception ex)
                            {
                                textError = columna + ", tiene formato incorrecto";
                                throw new Exception(textError);
                            }

                            columna = "Estado Contrato";
                            texto = Convert.ToString(item["Estado Contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.estado_contrato = Convert.ToString(item["Estado Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre Gestor Contratación";
                            texto = Convert.ToString(item["Nombre Gestor Contratación"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_gestor_contratacion = Convert.ToString(item["Nombre Gestor Contratación"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre Funcionario Autorizado";
                            texto = Convert.ToString(item["Nombre Funcionario Autorizado"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_funcionario_autorizado = Convert.ToString(item["Nombre Funcionario Autorizado"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Objeto Contrato";
                            texto = Convert.ToString(item["Objeto Contrato"]);
                            if (texto.Length <= 500)
                            {
                                obj.objeto_contrato = Convert.ToString(item["Objeto Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 500 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Subcategoría Contrato Nombre";
                            texto = Convert.ToString(item["Subcategoría Contrato Nombre"]);
                            if (texto.Length <= 500)
                            {
                                obj.subcategoria_contrato_nombre = Convert.ToString(item["Subcategoría Contrato Nombre"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 500 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Categoría Nombre";
                            texto = Convert.ToString(item["Categoría Nombre"]);
                            if (texto.Length <= 100)
                            {
                                obj.categoria_nombre = Convert.ToString(item["Categoría Nombre"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Lugar Ejecución Contrato";
                            texto = Convert.ToString(item["Lugar Ejecución Contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.lugar_ejecucion_contrato = Convert.ToString(item["Lugar Ejecución Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Departamento Ejecución Contrato";
                            texto = Convert.ToString(item["Departamento Ejecución Contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.departamento_ejecucion_contrato = Convert.ToString(item["Departamento Ejecución Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Subregional Ejecución Contrato";
                            texto = Convert.ToString(item["Subregional Ejecución Contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.subregional_ejecucion_contrato = Convert.ToString(item["Subregional Ejecución Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Regional Ejecución Contrato";
                            texto = Convert.ToString(item["Regional Ejecución Contrato"]);
                            if (texto.Length <= 100)
                            {
                                obj.regional_ejecucion_contrato = Convert.ToString(item["Regional Ejecución Contrato"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 100 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre Administrador / Funcionario Calificador";
                            texto = Convert.ToString(item["Nombre Administrador / Funcionario Calificador"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_administrador_funcionario = Convert.ToString(item["Nombre Administrador / Funcionario Calificador"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre Interventor";
                            texto = Convert.ToString(item["Nombre Interventor"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_interventor = Convert.ToString(item["Nombre Interventor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nit Proveedor";
                            texto = Convert.ToString(item["Nit Proveedor"]);
                            if (texto.Length <= 50)
                            {
                                obj.nit_proveedor = Convert.ToString(item["Nit Proveedor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "documento SAP";
                            texto = Convert.ToString(item["documento SAP"]);

                            if (texto.Length <= 50)
                            {
                                obj.documento_SAP = Convert.ToString(item["documento SAP"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 50 caracteres.";
                                throw new Exception(textError);
                            }

                            columna = "Nombre Proveedor";
                            texto = Convert.ToString(item["Nombre Proveedor"]);
                            if (texto.Length <= 200)
                            {
                                obj.nombre_proveedor = Convert.ToString(item["Nombre Proveedor"]).ToUpper();
                            }
                            else
                            {
                                textError = columna + ", solo puede contener 200 caracteres.";
                                throw new Exception(textError);
                            }

                            Listado.Add(obj);
                            obj = new contratos_detalle();
                        }
                    }

                    if (Listado.Count() > 0)
                    {
                        RtaInsercion = BusClass.CargueMasivoContratos(cargue, Listado, ref MsgRes);
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

                    if (ex.Message.Contains("column name does not exist."))
                    {
                        MsgRes.DescriptionResponse = "Error en titulos: " + ex.Message;
                    }

                    else if (textError != "" && textError != null)
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
        #endregion
    }
}