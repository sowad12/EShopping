using System.ComponentModel.DataAnnotations;

namespace Identity.Library.Domains.Commands.Account
{
    public class ForgetPasswordCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}

