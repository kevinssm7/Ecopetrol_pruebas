using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models.Menu
{
    public class MenuItem
    {
        public MenuItem()
        {
            this.ChildMenuItems = new List<MenuItem>();
        }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuItemAccion { get; set; }
        public string MenuItemControl { get; set; }
        public Nullable<int> ParentItemId { get; set; }
        public virtual ICollection<MenuItem> ChildMenuItems { get; set; }

    }
}