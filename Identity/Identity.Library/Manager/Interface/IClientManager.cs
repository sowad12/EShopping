using Identity.Library.Domains.Commands.Account;
using Identity.Library.Domains.Commands.Entities;
using Identity.Library.Domains.ViewModels;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Library.Manager.Interface
{
    public interface IClientManager
    {
        Task<Client> CreateClient(CreateClientCommand command);
        Task<Client> UpdateClient(UpdateClientCommand command);
        Task<int> DeleteClient(int clientId);
        //Task<bool> CreateIdentityUser(CreateIdentityUserViewModel model);
        //Task<dynamic> ChangePassword(ChangePasswordCommand model);
        //Task<dynamic> GetClient(long clubId);
    }
}
