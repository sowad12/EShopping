using Identity.Library.Domains.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Library.CustomInformation
{
    public class CustomUserProfileInformationAdder : IProfileService
    {
        protected UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        public CustomUserProfileInformationAdder(UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var principal = await _claimsFactory.CreateAsync(user);
            context.IssuedClaims.AddRange(principal.Claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user is not null);
        }
    }
}
