using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVENT_VER5.Models;

namespace EVENT_VER5.ViewModel
{
    public class UserHomeViewModel
    {
        public MEMBER member { get; set; }
        public List<FOLLOWING> mem_following { get; set; }
        public List<EVENT> event_for_home { get; set; }
    }
}