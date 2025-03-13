using Identity.Library.Domains.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Library.Domains.Commands.Account
{
    public class LoginCommand : LoginMiniModel
    {
        public bool AllowRememberLogin { get; set; } = false;
        public bool EnableLocalLogin { get; set; }
        public IEnumerable<ExternalProviderModel> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProviderModel>();
        public bool IsInvite { get; set; }=false;   
        public long ClubId { get; set; }
        public long SystemUserId { get; set; }
    }
}

