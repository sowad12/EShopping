using System.ComponentModel.DataAnnotations;

namespace Identity.Library.Domains.Commands.Account
{
    public class SignUpCommand
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        //public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match.")]
        public string ConfirmPassword { get; set; }


        //public string ReturnUrl { get; set; }

        //public bool TC { get; set; }   
        //public long? ClubId { get;set; }
        //public long SystemUserId { get; set; }
        //public string query { get; set; }
        //public long RoleId { get; set; }

    }
    
}
