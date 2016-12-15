using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVENT_VER5.Models;

namespace EVENT_VER5.ViewModel
{
    public class LOCATIONsViewModel
    {
        public LOCATION location_detail { get; set; }
        public PROMOTE_L location_promote { get; set; }
        public int day_of_promote { get; set; }
        public List<PROMOTE_L> location_for_promote { get; set; }
    }
}