using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOPETROL_COMMON.ENTIDADES;
using ECOPETROL_COMMON.UTILOBJECTS;

namespace AsaludEcopetrol.Models.Menu
{
    public class Menu
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
        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public List<MenuItem> Items;

        public List<ManagmentMenuResult> ManagmentMenu(String Usuario)
        {
            return BusClass.ManagmentMenu(Usuario, ref MsgRes);
        }

       

        #endregion
    }
}