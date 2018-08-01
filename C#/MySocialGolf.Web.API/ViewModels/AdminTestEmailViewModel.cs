using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class AdminTestEmailViewModel: BaseViewModel
    {
        [Required]
        [Display(Name = "To Email")]
        public string ToEmail { get; set; }

        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }

        [Required]
        [Display(Name = "Email Body")]
        public string EmailBody { get; set; }


    }
}