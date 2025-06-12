using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using ECOPETROL_COMMON.UTILOBJECTS;
using ECOPETROL_COMMON.ENTIDADES;
using System.Configuration;

namespace AsaludEcopetrol.Models.InicioSesion
{
    public class Login
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

        [Required]
        [Display(Name = "Usuario:")]
        public string usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña:")]
        public string contraseña { get; set; }

        [Display(Name = "Recordar en este equipo")]
        public bool RememberMe { get; set; }

        public String numero_documento { get; set; }

        public String usuarioClave { get; set; }
        
        [Display(Name = "Código:")]
        public string codigo { get; set; }

        #endregion

        #region EVENTOS PRIVADOS


        public int SessionTimeout
        {
            get
            {
                int timeout = 20; // Valor predeterminado de 20 minutos (o cualquier valor que desees)
                int.TryParse(ConfigurationManager.AppSettings["SessionTimeoutMinutes"], out timeout);
                return timeout;
            }
            set
            {
                // Aquí puedes agregar la lógica para cambiar la configuración si es necesario.
                // En general, no se cambia el tiempo de sesión en tiempo de ejecución.
            }
        }


        private static String GetSHA1(String _ConvertPassword)
        {
            SHA1 _sha1 = SHA1Managed.Create();
            ASCIIEncoding _encoding = new ASCIIEncoding();
            Byte[] _stream = null;
            StringBuilder sb = new StringBuilder();
            _stream = _sha1.ComputeHash(_encoding.GetBytes(_ConvertPassword));
            for (int i = 0; i < _stream.Length; i++) sb.AppendFormat("{0:x2}", _stream[i]);
            return sb.ToString();
        }

        #endregion

        #region METODOS

        public List<sis_usuario> ValidaIngreso(String _usuario, String _contraseña)
        {

            sis_usuario auditor = new sis_usuario();
            auditor.usuario = _usuario;
            auditor.contraseña = _contraseña != "temporal" ? GetSHA1(_contraseña) : _contraseña;

            return BusClass.ValidaIngreso(auditor, ref MsgRes);
        }

        public sis_usuario ValidaIngresoClave(String _usuario, String documento)
        {
            sis_usuario auditor = new sis_usuario();
            auditor.usuario = _usuario;
            auditor.numero_documento = documento;

            return BusClass.ValidaIngresoClave(auditor, ref MsgRes);
        }

        public void ActualizaEstadoUsuario(sis_usuario ObjUsuario, ref MessageResponseOBJ MsgRes)
        {
            BusClass.ActualizaEstadoUsuario(ObjUsuario, ref MsgRes);
        }

        public List<sis_usuario> BuscaAuditorUsu(String strUsuario, ref MessageResponseOBJ MsgRes)
        {
            return BusClass.BuscaAuditorUsu(strUsuario, ref MsgRes);
        }
        public List<ManagementPrestadoresAlertasFechaResult> ManagmentAlertas(ref MessageResponseOBJ MsgRes)
        {
            return BusClass.ManagmentAlertas(ref MsgRes);
        }
        #endregion
    }
}