using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.CuentasMedicas
{
    public class homologacion
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

        public String num_factura { get; set; }

        public String tipo_id_prestador { get; set; }

        public String num_id_prestador { get; set; }

        public int id_rips { get; set; }

        public int id_rips_homologacion_dtll { get; set; }
        public int id_rips_homologacion_dtll2 { get; set; }

        public String obsGlosa { get; set; }

        public String cod_homologacion { get; set; }

        public Int32 tiene_glosa { get; set; }

        public int TipoGlosa { get; set; }

        public int id_rips_homologacion_dtllG { get; set; }

        public String obsGlosaG { get; set; }

        public Int32 tiene_glosaG { get; set; }

        public int TipoGlosaG { get; set; }

        public int id_rips_homologacion_tarifario { get; set; }



        #endregion

        #region METODOS

        public List<ManagmentRipsHomologacionFacResult> ConsultaHomologacionFac(String num_factura, String tipo_id_prestador, String num_id_prestador)
        {
            return BusClass.ConsultaHomologacionFac(num_factura, tipo_id_prestador, num_id_prestador);
        }
        public List<Ref_tipo_documental> GetTipoIdentificacion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetTipoIdentificacion(ref MsgRes);
        }

        public List<ManagmentRipsHomologacionFacDTLLResult> ConsultaHomologacionFacdtll(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            return BusClass.ConsultaHomologacionFacdtll(num_factura, tipo_id_prestador, num_id_prestador, id_rips);
        }

        public int Insertar_rips_homologacion(rips_homologacion objbase, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertar_rips_homologacion(objbase, ref MsgRes);
        }
        public int Insertar_rips_homologacion_dtll(List<rips_homologacion_dtll> objbase, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Insertar_rips_homologacion_dtll(objbase, ref MsgRes);
        }

        public List<rips_homologacion> Traerhomologacion_dtll(rips_homologacion obj)
        {
            return BusClass.Traerhomologacion_dtll(obj);
        }
        public List<ManagmentRipsHomologacionFacDTLLFinalResult> ConsultaHomologacionFacdtllFinal(String num_factura, Int32 id_rips)
        {
            return BusClass.ConsultaHomologacionFacdtllFinal(num_factura, id_rips);
        }
        public List<vw_rips_homologacion_tarifario> TarifarioRips()
        {
            return BusClass.TarifarioRips();
        }
        public int Actualizar_md_Lupe_cargue_base_H(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Actualizar_md_Lupe_cargue_base_H(obj,ref MsgRes);
        }
        public int Actualizar_md_Lupe_cargue_base_G(rips_homologacion_dtll obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Actualizar_md_Lupe_cargue_base_G(obj,ref MsgRes);
        }
        public List<vw_md_ref_glosa> GetMedglosa()
        {
            return BusClass.GetMedglosa();
        }
        public int Actualizar_rips_homologacion(rips_homologacion obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.Actualizar_rips_homologacion(obj, ref MsgRes);
        }
        public List<ManagmentRipsHomologacionFacFinalResult> ConsultaHomologacionFacFinal(String num_factura, String tipo_id_prestador, String num_id_prestador, Int32 id_rips)
        {
            return BusClass.ConsultaHomologacionFacFinal(num_factura, tipo_id_prestador, num_id_prestador, id_rips);
        }
        #endregion
    }
}