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
        [Display(Name = "ToEmail")]
        public string ToEmail { get; set; }

        [Display(Name = "EmailSubject")]
        public string EmailSubject { get; set; }

        [Required]
        [Display(Name = "EmailBody")]
        public string EmailBody { get; set; }


    }
}