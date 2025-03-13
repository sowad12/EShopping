using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Library.Configurations
{
    public static class ScopeConfiguration
    {
        public static IEnumerable<ApiScope> GetScopes()
        {

            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = IdentityServerConstants.LocalApi.ScopeName,
                    DisplayName = IdentityServerConstants.LocalApi.ScopeName
                }
            };

        }

    }
}

