using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVENT_VER5.Models;

namespace EVENT_VER5.ViewModel
{
    public class EVENTsViewModel
    {
        public EVENT event_detail { get; set; }
        public PROMOTE_E event_promote { get; set; }
        public int day_of_promote { get; set; }
    }
}