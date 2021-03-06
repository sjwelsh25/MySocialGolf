﻿using MySocialGolf.Web.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MySocialGolf.Web.API.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}