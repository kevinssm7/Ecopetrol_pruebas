using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.InicioSesion
{
    public class CambioClave
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
        [DataType(DataType.Password)]
        [Display(Name = "contraseña Actual:")]
        public string contraseñaActual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña:")]
        public string ConfirmaContraseña { get; set; }


        [Required]
        [RegularExpression(@"(?=^.{8,14}$)(?=.*\d)(?=.*\W+)(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "La nueva contraseña debe contener minimo 8 caracteres y maximo 14 caracteres una letra mayuscula, una letra minuscula, un numero y un caracter (!#$%&/()=?¡/*)")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña:")]
        public string contraseñaNueva { get; set; }

        public Int32 id_usuario { get; set; }

        #endregion

        #region EVENTOS PRIVADOS
        public static String GetSHA1(String _ConvertPassword)
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

        public void ActualizaContraseña(String Strusuario, String StrContraseña, ref MessageResponseOBJ MsgRes)
        {
            sis_usuario objAd = new sis_usuario();
            objAd.usuario = Strusuario;
            objAd.contraseña = StrContraseña != "temporal" ? GetSHA1(StrContraseña) : StrContraseña;
            BusClass.ActualizaContraseña(objAd, ref MsgRes);
        }
        public void ActualizaContraseñaOlvido(Int32 Id_usuario, String StrContraseña, ref MessageResponseOBJ MsgRes)
        {
            sis_usuario objAd = new sis_usuario();
            objAd.id_usuario = Id_usuario;
            objAd.contraseña = StrContraseña != "temporal" ? GetSHA1(StrContraseña) : StrContraseña;

            BusClass.ActualizaContraseñaOlvido(objAd, ref MsgRes);
        }
        #endregion
    }
}