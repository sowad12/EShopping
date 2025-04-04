using Microsoft.AspNetCore.Identity;

namespace Identity.Library.Domains.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }
    }
}
