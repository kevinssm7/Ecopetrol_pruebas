using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsaludEcopetrol.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public int? SubEventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public string TextColor { get; set; }
        public string BorderColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}