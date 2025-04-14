using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsaludEcopetrol.BussinesManager;
using ECOPETROL_COMMON.ENTIDADES;
using static AsaludEcopetrol.Controllers.InicioSesion.UsuarioController;

namespace AsaludEcopetrol.Controllers.Menu
{
   
    public class MenuController : Controller
    {

        #region PROPIEDADES

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

        private Models.Menu.Menu _menu = new Models.Menu.Menu();

        #endregion

        #region METODOS

        [ChildActionOnly]
        public ActionResult Menu()
        {
            try
            {
                List<ManagmentMenuResult> lst = new List<ManagmentMenuResult>();
                lst = _menu.ManagmentMenu(SesionVar.UserName).ToList();
                Models.Menu.MenuItem mnuNewMenuItem;
                foreach (ManagmentMenuResult item in lst)
                {
                    if (item.pared_id == 0)
                    {
                        mnuNewMenuItem = new Models.Menu.MenuItem();
                        mnuNewMenuItem.MenuItemId = item.id_menu;
                        mnuNewMenuItem.MenuItemName = item.nombre_menu;
                        mnuNewMenuItem.MenuItemAccion = item.accion;
                        mnuNewMenuItem.MenuItemControl = item.control;
                        mnuNewMenuItem.ParentItemId = item.pared_id;

                        _menu.Items.Add(mnuNewMenuItem);

                        AddMenuItem(mnuNewMenuItem, lst);
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return PartialView("Menu", _menu);
        }

        private void AddMenuItem(Models.Menu.MenuItem mnuMenuItem, List<ManagmentMenuResult> lista)
        {
            Models.Menu.MenuItem mnuNewMenuItem;

            foreach (ManagmentMenuResult menuSubItem in lista)
            {
                if (menuSubItem.pared_id == mnuMenuItem.MenuItemId)
                {
                    mnuNewMenuItem = new Models.Menu.MenuItem();
                    mnuNewMenuItem.MenuItemId = menuSubItem.id_menu;
                    mnuNewMenuItem.MenuItemName = menuSubItem.nombre_menu;
                    mnuNewMenuItem.MenuItemAccion = menuSubItem.accion;
                    mnuNewMenuItem.MenuItemControl = menuSubItem.control;
                    mnuNewMenuItem.ParentItemId = menuSubItem.pared_id;
                    mnuMenuItem.ChildMenuItems.Add(mnuNewMenuItem);

                    //llamada recursiva para ver si el nuevo menu item aun tiene elementos hijos.
                    AddMenuItem(mnuNewMenuItem, lista);
                }
            }
        }


        #endregion
    }
}