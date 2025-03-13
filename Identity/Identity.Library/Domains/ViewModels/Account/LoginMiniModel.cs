using System.ComponentModel.DataAnnotations;

namespace Identity.Library.Domains.ViewModels.Account
{
    public class LoginMiniModel
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
