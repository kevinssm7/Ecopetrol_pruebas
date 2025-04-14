using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.ENUM;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace DATA_ACCESS
{
    public class ComonClass
    {
        #region PROPIEDADES

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        #endregion

        #region COMMONCLASS

        public List<Ref_tipo_documental> GetTipoDocumnetal()
        {
            List<Ref_tipo_documental> lstResult = new List<Ref_tipo_documental>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_documental.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_ips_ciudad> GetIPS()
        {
            List<vw_ips_ciudad> lstResult = new List<vw_ips_ciudad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ips_ciudad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_ips> GetPrstador()
        {
            List<Ref_ips> lstResult = new List<Ref_ips>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ips.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ips_cuentas> GetPrstadorCuentas()
        {
            List<Ref_ips_cuentas> lstResult = new List<Ref_ips_cuentas>();
            //Ref_ips_cuentas nuevo = new Ref_ips_cuentas();
            //nuevo.id_ref_ips_cuentas = 10000;
            //nuevo.Nombre = "OTRO";

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ips_cuentas.ToList();
                    //lstResult.Add(nuevo);
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<vw_ciudad_auditor> GetCiudad_auditor()
        {
            List<vw_ciudad_auditor> lstResult = new List<vw_ciudad_auditor>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ciudad_auditor.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_auditor> Get_auditor()
        {
            List<vw_auditor> lstResult = new List<vw_auditor>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_auditor.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }
        public List<Ref_origen_evento> GetOrigenEvento()
        {
            List<Ref_origen_evento> lstResult = new List<Ref_origen_evento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_origen_evento.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_regional> GetRefRegion()
        {
            List<Ref_regional> lstResult = new List<Ref_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_regional.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public Ref_regional GetRefRegionId(int id)
        {
            Ref_regional lstResult = new Ref_regional();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_regional.Where(x => x.id_ref_regional == id).FirstOrDefault();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_tipo_habitacion> GetTipoHabitacion()
        {
            List<Ref_tipo_habitacion> lstResult = new List<Ref_tipo_habitacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_habitacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_ingreso_cohorte> GetIngresoCohorte()
        {
            List<Ref_ingreso_cohorte> lstResult = new List<Ref_ingreso_cohorte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ingreso_cohorte.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }


        public List<Ref_tipo_ingreso> GetTipoIngreso()
        {
            List<Ref_tipo_ingreso> lstResult = new List<Ref_tipo_ingreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_ingreso.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_condicion_alta_censo> GetCondicionAlta()
        {
            List<Ref_condicion_alta_censo> lstResult = new List<Ref_condicion_alta_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_condicion_alta_censo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_cie10> GetCie10()
        {
            List<Ref_cie10> lstResult = new List<Ref_cie10>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_cie10.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_cie10> GetCie10Unido()
        {
            List<vw_cie10> lstResult = new List<vw_cie10>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_cie10.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_cie10> GetCie10UnidoDetalle()
        {
            List<vw_cie10> lstResult = new List<vw_cie10>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_cie10.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_ref_cups> GetCups()
        {
            List<vw_ref_cups> lstResult = new List<vw_ref_cups>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ref_cups.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_ref_enfermedades_huerfanas> GetEnfermedadesHuerfanas()
        {
            List<vw_ref_enfermedades_huerfanas> lstResult = new List<vw_ref_enfermedades_huerfanas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_ref_enfermedades_huerfanas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_cuentas_glosa> GetCuentaGlosa()
        {
            List<Ref_cuentas_glosa> lstResult = new List<Ref_cuentas_glosa>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_cuentas_glosa.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }
        public List<Ref_causal_glosa> GetCausalGlosa()
        {
            List<Ref_causal_glosa> lstResult = new List<Ref_causal_glosa>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_causal_glosa.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_responsable_glosa> GetResGlosa()
        {
            List<Ref_responsable_glosa> lstResult = new List<Ref_responsable_glosa>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_responsable_glosa.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;


        }

        public List<Ref_eventos_adversos> GetEventosAdversos()
        {
            List<Ref_eventos_adversos> lstResult = new List<Ref_eventos_adversos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_adversos.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_categorias_eventos_adv> GetRefCategoriaEvad()
        {
            List<Ref_categorias_eventos_adv> lstResult = new List<Ref_categorias_eventos_adv>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_categorias_eventos_adv.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_categorias_situaciones_de_calidad> GetRefCategoriaSituacion()
        {
            List<Ref_categorias_situaciones_de_calidad> lstResult = new List<Ref_categorias_situaciones_de_calidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_categorias_situaciones_de_calidad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_grado_lesion> GetGradoLesion()
        {
            List<Ref_grado_lesion> lstResult = new List<Ref_grado_lesion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_grado_lesion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_factores_contribuyentes> GetFactoresContribuyentes()
        {
            List<Ref_factores_contribuyentes> lstResult = new List<Ref_factores_contribuyentes>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_factores_contribuyentes.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_barreras_seguridad> GetBarrerasDeSeguridad()
        {
            List<Ref_barreras_seguridad> lstResult = new List<Ref_barreras_seguridad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_barreras_seguridad.OrderBy(p => p.des).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_acciones_inseguras> GetAccionesInseguras()
        {
            List<Ref_acciones_inseguras> lstResult = new List<Ref_acciones_inseguras>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_acciones_inseguras.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }
        public List<Ref_plan_de_manejo> GetPlanDeManejo()
        {
            List<Ref_plan_de_manejo> lstResult = new List<Ref_plan_de_manejo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_plan_de_manejo.OrderBy(p => p.des).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_situaciones_de_calidad> GetSituacionesDeCalidad()
        {
            List<Ref_situaciones_de_calidad> lstResult = new List<Ref_situaciones_de_calidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_situaciones_de_calidad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_motivo_cancelacion_procedimiento> GetMotivoCancelacion()
        {
            List<Ref_motivo_cancelacion_procedimiento> lstResult = new List<Ref_motivo_cancelacion_procedimiento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_motivo_cancelacion_procedimiento.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_condicion_del_egreso> GetCondicionEgreso()
        {
            List<Ref_condicion_del_egreso> lstResult = new List<Ref_condicion_del_egreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_condicion_del_egreso.OrderBy(p => p.descripcion).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_servicio_tratante> GetServiciotratante()
        {
            List<Ref_servicio_tratante> lstResult = new List<Ref_servicio_tratante>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_servicio_tratante.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_salud_publica> GetSaludPublica()
        {
            List<Ref_salud_publica> lstResult = new List<Ref_salud_publica>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_salud_publica.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_origen_hospitalizacion> GetOrigenHospitalizacion()
        {
            List<Ref_origen_hospitalizacion> lstResult = new List<Ref_origen_hospitalizacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_origen_hospitalizacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_lesiones_severas_y_alto_costo> GetAltoCosto()
        {
            List<Ref_lesiones_severas_y_alto_costo> lstResult = new List<Ref_lesiones_severas_y_alto_costo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_lesiones_severas_y_alto_costo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_tablero_censo> GetTableroCenso()
        {
            List<vw_tablero_censo> lstResult = new List<vw_tablero_censo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tablero_censo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }



        public List<management_vw_tablero_censoResult> GetTableroCensoCompleto()
        {
            List<management_vw_tablero_censoResult> lstResult = new List<management_vw_tablero_censoResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_vw_tablero_censo().ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_tablero_censo2> GetTableroCenso2()
        {
            List<vw_tablero_censo2> lstResult = new List<vw_tablero_censo2>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tablero_censo2.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_tablero_concurrencia> GetTableroConcurrencia()
        {
            List<vw_tablero_concurrencia> lstResult = new List<vw_tablero_concurrencia>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_tablero_concurrencia.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ciudades> GetCiudades()
        {
            List<Ref_ciudades> lstResult = new List<Ref_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ciudades.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }



        public List<Ref_ciudades> GetCiudadesXUnis(int? idUnis)
        {
            List<Ref_ciudades> lstResult = new List<Ref_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ciudades.Where(x => x.id_ref_odont_unis == idUnis).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ciudades> GetCiudadesXRegional(int? idRegional)
        {
            List<Ref_ciudades> lstResult = new List<Ref_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ciudades.Where(x => x.id_ref_regional == idRegional).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_causal_egreso> GetCausaEgreso()
        {
            List<Ref_causal_egreso> lstResult = new List<Ref_causal_egreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_causal_egreso.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_consulta_alertas> GetConsultaAlertas()
        {
            List<vw_consulta_alertas> lstResult = new List<vw_consulta_alertas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_consulta_alertas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Total_ciudades> GetTotalCiudades()
        {

            List<Total_ciudades> lstResult = new List<Total_ciudades>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Total_ciudades.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_motivo_devolucion_fac> GetMotivoDevolucionFac()
        {

            List<Ref_motivo_devolucion_fac> lstResult = new List<Ref_motivo_devolucion_fac>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_motivo_devolucion_fac.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_sis_auditor_ciudad> GetCiudadesAuditor()
        {

            List<vw_sis_auditor_ciudad> lstResult = new List<vw_sis_auditor_ciudad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_sis_auditor_ciudad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<sis_auditor_regional> GetRegionalAuditor()
        {

            List<sis_auditor_regional> lstResult = new List<sis_auditor_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.sis_auditor_regional.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_sis_auditor_regional> GetVWRegionalAuditor()
        {

            List<vw_sis_auditor_regional> lstResult = new List<vw_sis_auditor_regional>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_sis_auditor_regional.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_hallazgos_RIPS> GetRefHallazgos()
        {

            List<Ref_hallazgos_RIPS> lstResult = new List<Ref_hallazgos_RIPS>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_hallazgos_RIPS.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_motivo_reingreso> GetRefMotivoRiesgo()
        {

            List<Ref_motivo_reingreso> lstResult = new List<Ref_motivo_reingreso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_motivo_reingreso.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_cie10_alertas> GetRefcie10Alertas()
        {
            List<vw_cie10_alertas> lstResult = new List<vw_cie10_alertas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_cie10_alertas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_cie10_alertas_detalle> GetRefcie10AlertasNuevo()
        {
            List<vw_cie10_alertas_detalle> lstResult = new List<vw_cie10_alertas_detalle>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_cie10_alertas_detalle.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_enfermedades_Huerfanas> GetRefHuerfanas()
        {
            List<Ref_enfermedades_Huerfanas> lstResult = new List<Ref_enfermedades_Huerfanas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_enfermedades_Huerfanas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_tipo_ahorro> GetRefTipoAhorro()
        {
            List<Ref_tipo_ahorro> lstResult = new List<Ref_tipo_ahorro>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_tipo_ahorro.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_ingreso_cohorte> GetRefTipoCohorte()
        {
            List<Ref_ingreso_cohorte> lstResult = new List<Ref_ingreso_cohorte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_ingreso_cohorte.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_valor_ahorro> GetRefValorAhorro()
        {
            List<Ref_valor_ahorro> lstResult = new List<Ref_valor_ahorro>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_valor_ahorro.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_PQRS_Atributo> GetRefPqrsAtributo()
        {
            List<Ref_PQRS_Atributo> lstResult = new List<Ref_PQRS_Atributo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_Atributo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_PQRS_estado_Gestion> GetRefPqrsGestion()
        {
            List<Ref_PQRS_estado_Gestion> lstResult = new List<Ref_PQRS_estado_Gestion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_estado_Gestion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_PQRS_llamada> GetRefPqrsLLamada()
        {
            List<Ref_PQRS_llamada> lstResult = new List<Ref_PQRS_llamada>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_llamada.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_PQRS_Subtematica> GetRefPqrsSubtematica()
        {
            List<Ref_PQRS_Subtematica> lstResult = new List<Ref_PQRS_Subtematica>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_Subtematica.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_PQRS_tipo_solicitud> GetRefPqrsSolicitud()
        {
            List<Ref_PQRS_tipo_solicitud> lstResult = new List<Ref_PQRS_tipo_solicitud>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_PQRS_tipo_solicitud.Where(x => x.estado == 1).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_PQRS_usuarios> GetRefPqrsUsuarios()
        {
            List<vw_PQRS_usuarios> lstResult = new List<vw_PQRS_usuarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_PQRS_usuarios.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_analaisis_caso_ambito> Getambito()
        {
            List<Ref_analaisis_caso_ambito> lstResult = new List<Ref_analaisis_caso_ambito>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_analaisis_caso_ambito.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_eventos_decision> GetEventoDecision()
        {
            List<Ref_eventos_decision> lstResult = new List<Ref_eventos_decision>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_decision.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;


        }

        public List<Ref_eventos_habilidad> GetEventosHabilidad()
        {
            List<Ref_eventos_habilidad> lstResult = new List<Ref_eventos_habilidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_habilidad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_percepcion> GetEventosPercepcion()
        {
            List<Ref_eventos_percepcion> lstResult = new List<Ref_eventos_percepcion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_percepcion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_eventos_rutinaria> GetEventosRutinaria()
        {
            List<Ref_eventos_rutinaria> lstResult = new List<Ref_eventos_rutinaria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_rutinaria.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_excepcionales> GetEventosexcepcionales()
        {
            List<Ref_eventos_excepcionales> lstResult = new List<Ref_eventos_excepcionales>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_excepcionales.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_eventos_paciente> GetEventosPaciente()
        {
            List<Ref_eventos_paciente> lstResult = new List<Ref_eventos_paciente>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_paciente.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_tarea_tec> GetEventostarea()
        {
            List<Ref_eventos_tarea_tec> lstResult = new List<Ref_eventos_tarea_tec>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_tarea_tec.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_equipo> GetEventosEquipoT()
        {
            List<Ref_eventos_equipo> lstResult = new List<Ref_eventos_equipo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_equipo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_eventos_individuo> GetEventosIndividuo()
        {
            List<Ref_eventos_individuo> lstResult = new List<Ref_eventos_individuo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_individuo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_ambiente_tra> GetEventosambienteTrabajo()
        {
            List<Ref_eventos_ambiente_tra> lstResult = new List<Ref_eventos_ambiente_tra>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_ambiente_tra.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_organizacion> GetEventosOrganizacion()
        {
            List<Ref_eventos_organizacion> lstResult = new List<Ref_eventos_organizacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_organizacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_contexto> GetEventosContexto()
        {
            List<Ref_eventos_contexto> lstResult = new List<Ref_eventos_contexto>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_contexto.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_severidad> GetEventosSeveridad()
        {
            List<Ref_eventos_severidad> lstResult = new List<Ref_eventos_severidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_severidad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_repeticion> GetEventosProbavilidadR()
        {
            List<Ref_eventos_repeticion> lstResult = new List<Ref_eventos_repeticion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_repeticion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_eventos_tipo_evento> GetEventosTipoEvento()
        {
            List<Ref_eventos_tipo_evento> lstResult = new List<Ref_eventos_tipo_evento>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_eventos_tipo_evento.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<Ref_procesos> GetProcesosGD()
        {
            List<Ref_procesos> lstResult = new List<Ref_procesos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_procesos.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }


        public List<vw_md_ref_glosa> GetMedglosa()
        {
            List<vw_md_ref_glosa> lstResult = new List<vw_md_ref_glosa>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_ref_glosa.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<md_Ref_resultado_auditoria> GetResultadoAUD()
        {
            List<md_Ref_resultado_auditoria> lstResult = new List<md_Ref_resultado_auditoria>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_resultado_auditoria.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_md_Ref_indicador> GetMDIndicadores()
        {
            List<vw_md_Ref_indicador> lstResult = new List<vw_md_Ref_indicador>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_Ref_indicador.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<md_Ref_consultas> GetRefConsulta()
        {
            List<md_Ref_consultas> lstResult = new List<md_Ref_consultas>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_consultas.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_Ref_proveedor> GetMD_Ref_proveedor()
        {
            List<md_Ref_proveedor> lstResult = new List<md_Ref_proveedor>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_proveedor.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<GestionDocumentalMedicamentos> TraerPdf()
        {
            List<GestionDocumentalMedicamentos> lstResult = new List<GestionDocumentalMedicamentos>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.GestionDocumentalMedicamentos.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_Ref_com_dirigido> GetDirigido()
        {
            List<md_Ref_com_dirigido> lstResult = new List<md_Ref_com_dirigido>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_com_dirigido.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_Ref_com_tipo> GetMdTipo()
        {
            List<md_Ref_com_tipo> lstResult = new List<md_Ref_com_tipo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_Ref_com_tipo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_tipo_visita> GetMdTipoVisita()
        {
            List<md_ref_tipo_visita> lstResult = new List<md_ref_tipo_visita>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_tipo_visita.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<vw_md_crono_auditores> GetRefCronoAuditor()
        {
            List<vw_md_crono_auditores> lstResult = new List<vw_md_crono_auditores>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.vw_md_crono_auditores.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<md_ref_puntos_dispensacion> GetPuntosDispensacion()
        {
            List<md_ref_puntos_dispensacion> lstResult = new List<md_ref_puntos_dispensacion>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.md_ref_puntos_dispensacion.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<sis_usuario> GetSisUsuario()
        {
            List<sis_usuario> lstResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.sis_usuario.Where(x => x.id_rol == 1).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<sis_usuario> GetSisUsuarioactivo()
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.sis_usuario.Where(l => l.id_estado == 1).ToList();
        }

        public List<sis_usuario> GetSisUsuarioMd()
        {
            List<sis_usuario> lstResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.sis_usuario.Where(x => x.id_rol == 1).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<sis_usuario> GetSisUsuarioOdont()
        {
            List<sis_usuario> lstResult = new List<sis_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.sis_usuario.Where(x => x.id_rol == 15).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }

        public List<ref_meses_del_año> meses()
        {
            List<ref_meses_del_año> lstResult = new List<ref_meses_del_año>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.ref_meses_del_año.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<ref_tipo_cohorte> tipoCohortes()
        {
            List<ref_tipo_cohorte> lstResult = new List<ref_tipo_cohorte>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.ref_tipo_cohorte.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_rol_cargo> RolCargo()
        {
            List<Ref_rol_cargo> lstResult = new List<Ref_rol_cargo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.Ref_rol_cargo.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_odont_unis> Odont_unis()
        {
            List<Ref_odont_unis> lstResult = new List<Ref_odont_unis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.Ref_odont_unis.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<Ref_odont_unis> Odont_unisIdRegional(int? idRegional)
        {
            List<Ref_odont_unis> lstResult = new List<Ref_odont_unis>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.Ref_odont_unis.Where(x => x.id_regional == idRegional).ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public Ref_odont_unis traerUnisId(int? idUnis)
        {
            Ref_odont_unis datos = new Ref_odont_unis();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    datos = db.Ref_odont_unis.FirstOrDefault(x => x.id_ref_unis == idUnis);
                }
            }catch(Exception ex)
            {
                var error = ex.Message;
            }
            return datos;
        }

        public List<Ref_si_no> Ref_sino()
        {
            List<Ref_si_no> lstResult = new List<Ref_si_no>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.Ref_si_no.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public List<vw_rol_usuarios> Ref_rol_Usuario()
        {
            List<vw_rol_usuarios> lstResult = new List<vw_rol_usuarios>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.vw_rol_usuarios.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }
        public List<vw_cargo_usuario> Ref_cargo_Usuario()
        {
            List<vw_cargo_usuario> lstResult = new List<vw_cargo_usuario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {

                    lstResult = db.vw_cargo_usuario.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;

        }

        public ecop_firma_digital_sami GetFirmas(Int32? idusuario)
        {
            ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext();
            return db.ecop_firma_digital_sami.Where(l => l.id_usuario == idusuario).FirstOrDefault();
        }

        public management_ecop_firma_digital_samiResult GetFirmasId(int? idUsuario)
        {
            management_ecop_firma_digital_samiResult dato = new management_ecop_firma_digital_samiResult();

            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    dato = db.management_ecop_firma_digital_sami(idUsuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return dato;

        }


        public List<ecop_firma_digital_sami> listaFirmasUsuarios()
        {
            List<ecop_firma_digital_sami> lista = new List<ecop_firma_digital_sami>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ecop_firma_digital_sami.Where(x => x.ruta == null | x.ruta == "").ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return lista;
        }



        public List<calidad_ref_especialidad> GetEspecialidades()
        {
            List<calidad_ref_especialidad> lstResult = new List<calidad_ref_especialidad>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.calidad_ref_especialidad.ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
            }
            return lstResult;
        }


        public List<management_usuarios_regionalResult> ListadoRegionalUsuario()
        {
            List<management_usuarios_regionalResult> lista = new List<management_usuarios_regionalResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_usuarios_regional().ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #region FFMM

        public List<Ref_ffmm_glosas> FFMM_glosas()
        {
            List<Ref_ffmm_glosas> lstResult = new List<Ref_ffmm_glosas>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_glosas.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }


        public List<Ref_ffmm_alto_costo> FFMM_altocosto()
        {
            List<Ref_ffmm_alto_costo> lstResult = new List<Ref_ffmm_alto_costo>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_alto_costo.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_tipo_proveedor> FFMM_tipo_proveedor()
        {
            List<Ref_ffmm_tipo_proveedor> lstResult = new List<Ref_ffmm_tipo_proveedor>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_tipo_proveedor.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_especialidad_remite> FFMM_especialidad_remite()
        {
            List<Ref_ffmm_especialidad_remite> lstResult = new List<Ref_ffmm_especialidad_remite>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_especialidad_remite.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_especialidad_remitio> FFMM_especialidad_remitio()
        {
            List<Ref_ffmm_especialidad_remitio> lstResult = new List<Ref_ffmm_especialidad_remitio>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_especialidad_remitio.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_estado> FFMM_estado()
        {
            List<Ref_ffmm_estado> lstResult = new List<Ref_ffmm_estado>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_estado.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_fuerza> FFMM_fuerza()
        {
            List<Ref_ffmm_fuerza> lstResult = new List<Ref_ffmm_fuerza>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_fuerza.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_imagen_proc> FFMM_imagen_proc()
        {
            List<Ref_ffmm_imagen_proc> lstResult = new List<Ref_ffmm_imagen_proc>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_imagen_proc.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_modalidad_pago> FFMM_modalidad_pago()
        {
            List<Ref_ffmm_modalidad_pago> lstResult = new List<Ref_ffmm_modalidad_pago>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_modalidad_pago.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }
        public List<Ref_ffmm_nivel_atencion> FFMM_nivel_atencion()
        {
            List<Ref_ffmm_nivel_atencion> lstResult = new List<Ref_ffmm_nivel_atencion>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_nivel_atencion.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_nivel_complejidad> FFMM_nivel_complejidad()
        {
            List<Ref_ffmm_nivel_complejidad> lstResult = new List<Ref_ffmm_nivel_complejidad>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_nivel_complejidad.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_origen_servicio> FFMM_origen_servicio()
        {
            List<Ref_ffmm_origen_servicio> lstResult = new List<Ref_ffmm_origen_servicio>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_origen_servicio.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_prestadores> FFMM_prestadores()
        {
            List<Ref_ffmm_prestadores> lstResult = new List<Ref_ffmm_prestadores>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_prestadores.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_prestadores_proveedor> FFMM_prestadores_Proveedor()
        {
            List<Ref_ffmm_prestadores_proveedor> lstResult = new List<Ref_ffmm_prestadores_proveedor>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_prestadores_proveedor.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }
        public List<vw_ffmm_ips> FFMM_prestadores_vw()
        {
            List<vw_ffmm_ips> lstResult = new List<vw_ffmm_ips>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.vw_ffmm_ips.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_proceso> FFMM_proceso()
        {
            List<Ref_ffmm_proceso> lstResult = new List<Ref_ffmm_proceso>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_proceso.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_servicio> FFMM_servicio()
        {
            List<Ref_ffmm_servicio> lstResult = new List<Ref_ffmm_servicio>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_servicio.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_sexo> FFMM_sexo()
        {
            List<Ref_ffmm_sexo> lstResult = new List<Ref_ffmm_sexo>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_sexo.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;
        }

        public List<Ref_ffmm_sino> FFMM_sino()
        {
            List<Ref_ffmm_sino> lstResult = new List<Ref_ffmm_sino>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_sino.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_tipo_afiliacion> FFMM_tipo_afiliacion()
        {
            List<Ref_ffmm_tipo_afiliacion> lstResult = new List<Ref_ffmm_tipo_afiliacion>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_tipo_afiliacion.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_tipo_servicio> FFMM_tipo_servicio()
        {
            List<Ref_ffmm_tipo_servicio> lstResult = new List<Ref_ffmm_tipo_servicio>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_tipo_servicio.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_unidad_satelite> FFMM_unidad_satelite()
        {
            List<Ref_ffmm_unidad_satelite> lstResult = new List<Ref_ffmm_unidad_satelite>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_unidad_satelite.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_unudadR> FFMM_unudadR()
        {
            List<Ref_ffmm_unudadR> lstResult = new List<Ref_ffmm_unudadR>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_unudadR.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<vw_ffmm_departamento> FFMM_departamento()
        {
            List<vw_ffmm_departamento> lstResult = new List<vw_ffmm_departamento>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.vw_ffmm_departamento.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<vw_ffmm_municipio> FFMM_municipio()
        {
            List<vw_ffmm_municipio> lstResult = new List<vw_ffmm_municipio>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.vw_ffmm_municipio.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<vw_FMM_RefGeneral> FFMM_Reg_General()
        {
            List<vw_FMM_RefGeneral> lstResult = new List<vw_FMM_RefGeneral>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.vw_FMM_RefGeneral.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_glosas_respuesta> FFMM_respuestas_glosas()
        {
            List<Ref_ffmm_glosas_respuesta> lstResult = new List<Ref_ffmm_glosas_respuesta>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_glosas_respuesta.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }

            return lstResult;

        }

        public List<Ref_ffmm_unidad_cp> FFMM_Unidad_CP()
        {
            List<Ref_ffmm_unidad_cp> lstResult = new List<Ref_ffmm_unidad_cp>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_unidad_cp.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            return lstResult;
        }

        public List<Ref_ffmm_tipo_visita_cp> FFMM_TipoV_CP()
        {
            List<Ref_ffmm_tipo_visita_cp> lstResult = new List<Ref_ffmm_tipo_visita_cp>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_ffmm_tipo_visita_cp.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            return lstResult;
        }


        public List<vw_ref_regiona_ciudad> Ref_regional_ciudad()
        {
            List<vw_ref_regiona_ciudad> lstResult = new List<vw_ref_regiona_ciudad>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.vw_ref_regiona_ciudad.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            return lstResult;
        }

        public List<management_ref_regiona_ciudadResult> ListadoPrestadoresRegionalCiudad(int? regional, int? ciudad)
        {
            List<management_ref_regiona_ciudadResult> lista = new List<management_ref_regiona_ciudadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.management_ref_regiona_ciudad(regional, ciudad).ToList();
                }
            }catch(Exception ex)
            {
                var rror = ex.Message;
            }
            return lista;
        }

        public List<management_regional_ciudadResult> Ref_regional_ciudad_detallado()
        {
            List<management_regional_ciudadResult> lstResult = new List<management_regional_ciudadResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lstResult = db.management_regional_ciudad().ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lstResult;
        }


        public List<Ref_plan_mejora_estado_tarea> estadoTarea()
        {
            List<Ref_plan_mejora_estado_tarea> lstResult = new List<Ref_plan_mejora_estado_tarea>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.Ref_plan_mejora_estado_tarea.ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            return lstResult;
        }

        public List<ManagementPrestadoresAlertasFechaResult> ManagmentAlertas(ref MessageResponseOBJ MsgRes)
        {
            List<ManagementPrestadoresAlertasFechaResult> ListResult = new List<ManagementPrestadoresAlertasFechaResult>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    ListResult = db.ManagementPrestadoresAlertasFecha().ToList();
                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    return ListResult;
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = ex.Message;
                return ListResult;
            }
        }

        public List<ref_consulta_ecopetrol> Ref_ConsultasEcopetrol()
        {
            List<ref_consulta_ecopetrol> lstResult = new List<ref_consulta_ecopetrol>();
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                lstResult = db.ref_consulta_ecopetrol.ToList();
                lstResult = lstResult.Where(x => x.estado == "A").ToList();
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
            }
            return lstResult;
        }

        public List<ref_ffmm_ips_prestadores_tipo> tipoIpsPrestador()
        {
            List<ref_ffmm_ips_prestadores_tipo> list = new List<ref_ffmm_ips_prestadores_tipo>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.ref_ffmm_ips_prestadores_tipo.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }

        }

        public List<vw_rips_homologacion_tarifario> TarifarioRips()
        {
            List<vw_rips_homologacion_tarifario> list = new List<vw_rips_homologacion_tarifario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    list = db.vw_rips_homologacion_tarifario.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return list;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 24 de enero de 2023
        /// Metodo para insertar una actividad reciente en la base de datos
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="MsgRes"></param>
        public void InsertarActividadReciente(sis_actividad_reciente obj, ref MessageResponseOBJ MsgRes)
        {
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    db.sis_actividad_reciente.InsertOnSubmit(obj);
                    db.SubmitChanges();

                    MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Correcto;
                    MsgRes.DescriptionResponse = "Actividad añadida correctamente.";
                }
            }
            catch (Exception ex)
            {
                MsgRes.ResponseType = BussinesEnums.EnumTipoRespuesta.Error;
                MsgRes.DescriptionResponse = "Ha ocurrido un error al momento de ingresar la actividad: " + ex.Message;
            }
        }

        /// <summary>
        /// Autor: Alexis Quiñones Castillo
        /// Fecha: 24 de enero de 2023
        /// Obtener el listado de las actividades recientes agregadas
        /// </summary>
        /// <returns></returns>
        public List<Management_sis_actividad_recienteResult> GetListActividadReciente()
        {
            using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
            {
                return db.Management_sis_actividad_reciente().ToList();
            }
        }

        #endregion

        #endregion

        #region evaluacionDispensacionVisitas

        #endregion

        #region REEMBOLSO

        public List<ref_tipo_moneda> TipoMoneda()
        {
            List<ref_tipo_moneda> lista = new List<ref_tipo_moneda>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tipo_moneda.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_estado_reembolso> EstadoReembolso()
        {
            List<ref_estado_reembolso> lista = new List<ref_estado_reembolso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_estado_reembolso.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        public List<ref_tipo_reembolso> TipoReembolso()
        {
            List<ref_tipo_reembolso> lista = new List<ref_tipo_reembolso>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tipo_reembolso.ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #endregion REEMBOLSO


        #region BENEFICIARIOS

        public List<ref_tipo_beneficiario> ListadoTipoBeneficiarios()
        {
            List<ref_tipo_beneficiario> lista = new List<ref_tipo_beneficiario>();
            try
            {
                using (ECOPETROL_DataContexDataContext db = new ECOPETROL_DataContexDataContext())
                {
                    lista = db.ref_tipo_beneficiario.Where(x => x.estado == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return lista;
        }

        #endregion BENEFICIARIOS
    }
}
