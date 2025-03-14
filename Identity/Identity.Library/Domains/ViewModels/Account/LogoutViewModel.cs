using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Library.Domains.ViewModels.Account
{
    public class LogoutViewModel:LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
    }
}
