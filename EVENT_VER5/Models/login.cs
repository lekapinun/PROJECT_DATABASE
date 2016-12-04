using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EVENT_VER5.Models
{
    public class login
    {
        [Required(ErrorMessage = "please enter user name")]
        public string username { get; set; }
        [Required(ErrorMessage = "please enter password")]
        public string password { get; set; }
    }
}