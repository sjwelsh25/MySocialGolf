using System.ComponentModel.DataAnnotations;


namespace MySocialGolf.Web.API.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}