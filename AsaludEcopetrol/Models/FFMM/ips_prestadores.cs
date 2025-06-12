using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.FFMM
{
    public class ips_prestadores
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

        public int ip_ips_proveedor { get; set; }
        public int tipo { get; set; }
        public int cod_departamento { get; set; }
        public String departamento { get; set; }
        public int cod_municipio { get; set; }
        public String muninombre { get; set; }
        public decimal codigohabilitacion { get; set; }
        public String nombre { get; set; }
        public int nit { get; set; }
        public decimal digito_verificacion { get; set; }
        public String direccion { get; set; }
        public String telefono { get; set; }
        public String najunombre { get; set; }
        public int naju { get; set; }
        public String red_interna { get; set; }


        #endregion

        #region propiedadescontrato

        public int ipsPre { get; set; }
        public DateTime fechaIni { get; set; }
        public DateTime fechaFin { get; set; }

        public decimal valorContrato { get; set; }
        public String observaciones { get; set; }
        public string documento { get; set; }

        public HttpPostedFileBase file { get; set; }

        #endregion

        #region FUNCIONES 

        public List<vw_ffmm_departamento> FFMM_departamento()
        {
            return BusClass.FFMM_departamento();

        }
        public Int32 InsertarFFMMAuditoria(ffmm_auditoria OBJ, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFFMMAuditoria(OBJ, ref MsgRes);
        }

        public List<Ref_tipo_documental> GetTipoDocumnetal()
        {
            return BusClass.GetTipoDocumnetal();
        }
        public List<Ref_ffmm_tipo_afiliacion> FFMM_tipo_afiliacion()
        {
            return BusClass.FFMM_tipo_afiliacion();
        }
        public List<Ref_ffmm_origen_servicio> FFMM_origen_servicio()
        {
            return BusClass.FFMM_origen_servicio();
        }
        public List<Ref_ffmm_unidad_satelite> GetUnidadSatelite(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetUnidadSatelite(ref MsgRes);
        }
        public List<Ref_ffmm_unidad_cp> GetSitioAdscripcion(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetSitioAdscripcion(ref MsgRes);
        }

        public List<ref_ffmm_ips_prestadores_tipo> tipoIpsPrestador()
        {
            return BusClass.tipoIpsPrestador();
        }

        public List<vw_ffmm_departamento> GetDepartamentos(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.GetDepartamentos(ref MsgRes);
        }

        public List<vw_ffmm_municipio> GetMunicipios(int idDepartamento, ref MessageResponseOBJ MsgRes)
        {
            List<vw_ffmm_municipio> list = BusClass.GetMunicipiosFM(idDepartamento, ref MsgRes);
            return list;
        }
        public int InsertarIpsPrestador(ref_ffmm_ips_prestadores obj)
        {
            return BusClass.InsertarIpsPrestador(obj);
        }
        public List<ref_ffmm_ips_prestadores> existenciaIpsPrestadores(int nit)
        {
            return BusClass.existenciaIpsPrestadores(nit);
        }
        public int ActualizarIpsPrestadores(ref_ffmm_ips_prestadores obj)
        {
            return BusClass.ActualizarIpsPrestadores(obj);
        }
        #endregion

        #region contratos
        public int InsertarContratosFFMM(ffmm_contratos obj)
        {
            return BusClass.InsertarContratosFFMM(obj);
        }
        #endregion contratos

    }
}