using Identity.Library.Domains.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Library.Contexts
{
    public class AppIdentityDbContext : IdentityDbContext<User, Role, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }


        //public DbSet<UserInvitation> UserInvitations { get; set; }

        //public DbSet<UserInvitationInformation> UserInvitationInformations { get; set; }
        //public DbSet<UserAccept> UserAccept { get; set; }

    }
}
