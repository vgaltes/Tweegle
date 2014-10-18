using System.ComponentModel.DataAnnotations;

namespace Tweegle.Presentation.WebClient.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}