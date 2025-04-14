using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.BussinesManager
{
    public class SessionState
    {

        #region CONSTRUCTOR

        public SessionState()
        {
            _Context = HttpContext.Current.Session;
        }

        #endregion

        #region PROPIEDADES

        System.Web.SessionState.HttpSessionState _Context;

        #endregion

        #region LOGIN

        private Int32 _IDUser;
        public Int32 IDUser
        {
            get
            {
                _IDUser = (Int32)_Context["_IDUser"];
                return _IDUser;
            }
            set { _Context["_IDUser"] = value; }
        }

        private Int32 _IDCargo;
        public Int32 IDCargo
        {
            get
            {
                _IDCargo = (Int32)_Context["_IDCargo"];
                return _IDCargo;
            }
            set { _Context["_IDCargo"] = value; }
        }

        
        private string _UserName;
        public string UserName
        {
            get
            {
                _UserName = (String)_Context["UserName"];
                if (string.IsNullOrEmpty(_UserName))
                {
                    _UserName = string.Empty;
                }
                return _UserName;
            }
            set { _Context["UserName"] = value; }
        }

        private String _IPAddress;
        public String IPAddress
        {
            get
            {
                _IPAddress = _Context["_IPAddress"] != null ? (String)_Context["_IPAddress"] : "No Registra";
                return _IPAddress;
            }
            set { _Context["_IPAddress"] = value; }
        }

        private Int32 _CountAttempts;
        public Int32 CountAttempts
        {
            get
            {
                _CountAttempts = _Context["_CountAttempts"] != null ? (Int32)_Context["_CountAttempts"] : 0;
                return _CountAttempts;
            }
            set { _Context["_CountAttempts"] = value; }
        }

        private String _ROL;
        public String ROL
        {
            get
            {
                _ROL = _Context["_ROL"] != null ? (String)_Context["_ROL"] : "I";
                return _ROL;
            }
            set { _Context["_ROL"] = value; }
        }

        private String _ROL_CARGO;
        public String ROL_CARGO
        {
            get
            {
                _ROL_CARGO = _Context["_ROL_CARGO"] != null ? (String)_Context["_ROL_CARGO"] : "I";
                return _ROL_CARGO;
            }
            set { _Context["_ROL_CARGO"] = value; }
        }

        private String _Estado;
        public String Estado
        {
            get
            {
                _Estado = _Context["_Estado"] != null ? (String)_Context["_Estado"] : "I";
                return _Estado;
            }
            set { _Context["_Estado"] = value; }
        }

        private Boolean _Restriction;
        public Boolean Restriction
        {
            get
            {
                _Restriction = (Boolean)_Context["_Restriction"];
                return _Restriction;
            }
            set { _Context["_Restriction"] = value; }
        }

        private String _CiudadAuditor;
        public String CiudadAuditor
        {
            get
            {
                _CiudadAuditor = _Context["_CiudadAuditor"] != null ? Convert.ToString(_Context["_CiudadAuditor"]) : String.Empty;
                return _CiudadAuditor;
            }
            set { _Context["_CiudadAuditor"] = value; }
        }

        private String _NombreUsuario;
        public String NombreUsuario
        {
            get
            {
                _NombreUsuario = _Context["_NombreUsuario"] != null ? Convert.ToString(_Context["_NombreUsuario"]) : String.Empty;
                return _NombreUsuario;
            }
            set { _Context["_NombreUsuario"] = value; }
        }

        private Int32 _OPCInt;
        public Int32 OPCInt
        {
            get
            {
                _OPCInt = (Int32)_Context["_OPCInt"];
                return _OPCInt;
            }
            set { _Context["_OPCInt"] = value; }
        }


        #endregion

    }
}