using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySocialGolf.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile")]
        public string Phone { get; set; }

        [Display(Name = "Current Handicap")]
        public Nullable<decimal> CurrentHandicap { get; set; }
    }
}