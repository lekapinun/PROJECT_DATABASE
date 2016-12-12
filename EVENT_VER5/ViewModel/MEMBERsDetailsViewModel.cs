using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVENT_VER5.Models;

namespace EVENT_VER5.ViewModel
{
    public class MEMBERsDetailsViewModel
    {
        public MEMBER mem_profile { get; set; }
        public List<EVENT> EVENTs { get; set; }
    }
}