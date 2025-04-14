using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOPETROL_COMMON.UTILOBJECTS
{
   public class MessageResponseOBJ
    {
        private string _CodeError;
        public string CodeError
        {
            get { return _CodeError; }
            set { _CodeError = value; }
        }

        private ENUM.BussinesEnums.EnumTipoRespuesta _ResponseType;
        public ENUM.BussinesEnums.EnumTipoRespuesta ResponseType
        {
            get { return _ResponseType; }
            set { _ResponseType = value; }
        }

        private string _DescriptionResponse;
        public string DescriptionResponse
        {
            get { return _DescriptionResponse; }
            set { _DescriptionResponse = value; }
        }
    }
}
