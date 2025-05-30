using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.InicioSesion
{
    public class Usuarios
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


        public int id_usuario { get; set; }

        public String usuario { get; set; }

        public String contraseña { get; set; }

        public String nombre { get; set; }
        public String nombreP { get; set; }
        public String nombreS { get; set; }
        public String apellidoP { get; set; }
        public String apellidoS { get; set; }

        public String numero_documento { get; set; }

        public String profesion { get; set; }

        public String tel { get; set; }

        public String cobertura { get; set; }

        public String correo { get; set; }

        public int id_rol { get; set; }

        public int id_estado { get; set; }

        public String correo_ins { get; set; }

        public int id_rol_cargo { get; set; }

        #endregion

        #region FUNCIONES

        public Int32 CrearUsuairo(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
          return  BusClass.CrearUsuairo(ObjUsuario, ref MsgRes);
        }

        public List<vw_rol_usuarios> Ref_rol_Usuario()
        {
            return BusClass.Ref_rol_Usuario();
        }
        public List<vw_cargo_usuario> Ref_cargo_Usuario()
        {
            return BusClass.Ref_cargo_Usuario();

        }
        public List<sis_usuario> BuscaAuditorDoc(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.BuscaAuditorDoc(strUsuario, ref MsgRes);
        }
        public List<sis_usuario> BuscaAuditorUsuSami(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.BuscaAuditorUsuSami(strUsuario, ref MsgRes);
        }

        public Int32 InsertarFirmadigital(ecop_firma_digital_cod_barras obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFirmadigital(obj, ref MsgRes);
        }

        public ecop_firma_digital_cod_barras GetDtll_codBarras(Int32? idDetalle)
        {
            
            return BusClass.GetDtll_codBarras(idDetalle);
        }

        public Int32 InsertarFirmadigitalsami(ecop_firma_digital_sami obj, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.InsertarFirmadigitalsami(obj, ref MsgRes);
        }

        #endregion
    }
}