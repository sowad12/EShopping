using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Library.Configurations
{
    public static class IdentityResourceConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            };

        }

    }
}

