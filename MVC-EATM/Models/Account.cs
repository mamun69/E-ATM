using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EATM.Models
{
    public class Account
    {
        public int Id  { get; set; }

        [Display(Name = "Account Number")] 
        public int accountNo { get; set; }

        [Display(Name = "Pin Number")]
        public int pin { get; set; }
        [Display(Name = "Balance")]
        public double balance { get; set; }
    }
}