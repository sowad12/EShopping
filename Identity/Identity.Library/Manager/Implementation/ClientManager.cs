using Identity.Library.Contexts;
using Identity.Library.Domains.Commands.Entities;
using Identity.Library.Domains.Entities;
using Identity.Library.Manager.Interface;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities = IdentityServer4.EntityFramework.Entities;

namespace Identity.Library.Manager.Implementation
{
    public class ClientManager : IClientManager
    {
        private readonly AppConfigurationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ClientManager(AppConfigurationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Entities.Client> CreateClient(CreateClientCommand command)
        {
            var claims = new List<IdentityServer4.EntityFramework.Entities.ClientClaim>();
            claims.Add(new Entities.ClientClaim { Type = "eshop", Value = Guid.NewGuid().ToString() });
            //if (command.ClubId != null && command.ClubId > 0)
            //{
                
            //}
            //else
            //{
            //    claims.Add(new Entities.ClientClaim { Type = "access_level", Value = command.ClubId.ToString() });
            //}
            var client = new Entities.Client
            {
                ClientName = command.ClientName,
                ClientId = command.ClientId,
                ClientSecrets = command.ClientSecrets.Select(x => new ClientSecret { Type = "SharedSecret", Value = x.Sha256() }).ToList(),
                //ClientSecrets = command.ClientSecrets.Select(x => new ClientSecret { Type = "SharedSecret", Value = x }).ToList(),
                RequireClientSecret = command.RequireClientSecret,
                RedirectUris = command.RedirectUris.Select(x => new ClientRedirectUri { RedirectUri = x }).ToList(),
                PostLogoutRedirectUris = command.PostLogoutRedirectUris.Select(x => new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = x }).ToList(),
                AllowedGrantTypes = (command.AllowedGrantTypes == null || command.AllowedGrantTypes.Count == 0) ? new List<ClientGrantType> { new ClientGrantType { GrantType = GrantType.AuthorizationCode } } : command.AllowedGrantTypes.Select(x => new ClientGrantType { GrantType = x }).ToList(),
                AllowedScopes = command.AllowedScopes.Select(x => new ClientScope { Scope = x }).ToList(),
                FrontChannelLogoutUri = command.FrontChannelLogoutUri,
                BackChannelLogoutUri = command.BackChannelLogoutUri,
                Claims = claims,
                EnableLocalLogin = true,
                Enabled = true,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                RequirePkce = true,
            };

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return client;
        }


        public async Task<Entities.Client> UpdateClient(UpdateClientCommand command)
        {
            var client = _context.Clients.Where(x => x.ClientName == command.ClientName).FirstOrDefault();

            //var claims = new List<Entities.ClientClaim>();
            //claims.Add(new Entities.ClientClaim { Type = "clubId", Value = command.ClubId.ToString() });

            client.ClientName = command.ClientName;
            client.ClientId = command.ClientId;
            client.ClientSecrets = command.ClientSecrets.Select(x => new ClientSecret { Type = "SharedSecret", Value = x.Sha256() }).ToList();
            //client.RequireClientSecret = command.RequireClientSecret;
            //client.RedirectUris = command.RedirectUris.Select(x => new ClientRedirectUri { RedirectUri = x }).ToList();
            //client.PostLogoutRedirectUris = command.PostLogoutRedirectUris.Select(x => new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = x }).ToList();
            //client.AllowedGrantTypes = (command.AllowedGrantTypes == null || command.AllowedGrantTypes.Count == 0) ? new List<ClientGrantType> { new Entities.ClientGrantType { GrantType = GrantType.AuthorizationCode } } : command.AllowedGrantTypes.Select(x => new ClientGrantType { GrantType = x }).ToList();
            //client.AllowedScopes = command.AllowedScopes.Select(x => new ClientScope { Scope = x }).ToList();
            //client.FrontChannelLogoutUri = command.FrontChannelLogoutUri;
            //client.BackChannelLogoutUri = command.BackChannelLogoutUri;
            //client.Claims = claims;
            //client.EnableLocalLogin = true;
            //client.Enabled = true;
            //client.AllowOfflineAccess = true;
            //client.AlwaysIncludeUserClaimsInIdToken = true;
            //client.RequirePkce = true;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return client;
        }


        public async Task<int> DeleteClient(int clientId)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
            _context.Clients.Remove(client);
            var result = await _context.SaveChangesAsync();
            return result;
        }


        //public async Task<bool> CreateIdentityUser(CreateIdentityUserViewModel model)
        //{
        //    await _bus.Publish(new OnCreateUserMessage
        //    {
        //        Email = model.Email,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Password = model.Password,
        //        Claims = new List<ClaimViewModel> {
        //            new ClaimViewModel("access_level", "external_user"),
        //            new ClaimViewModel("user_id", "e1")
        //        }
        //    });
        //    return true;
        //}


        //public async Task<dynamic> ChangePassword(ChangePasswordCommand model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email);
        //    if (user is null)
        //    {
        //        throw new CustomException("User could not be found.", System.Net.HttpStatusCode.InternalServerError);
        //    }

        //    IdentityResult resetResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (resetResult.Succeeded)
        //    {
        //        var obj = new StatusViewModel { Code = System.Net.HttpStatusCode.OK, Message = "Password update success." };
        //        return obj;
        //    }
        //    else
        //    {
        //        //throw new CustomException("Error", System.Net.HttpStatusCode.InternalServerError);
        //        throw new CustomException(resetResult.Errors.FirstOrDefault().Description, System.Net.HttpStatusCode.InternalServerError);
        //    }
        //}

        //public async Task<dynamic> GetClient(long clubId)
        //{
        //    var clients = _context.Clients.Where(x => x.ClientName.Length > 0).Include(a => a.Claims).Include(a => a.ClientSecrets);
        //    foreach (var client in clients)
        //    {
        //        if (client.Claims != null)
        //            foreach (var claim in client.Claims)
        //            {
        //                if (claim.Type == "clubId" && claim.Value == clubId.ToString())
        //                {
        //                    //var payload = new
        //                    //{
        //                    //    clientId = client.Claims.Where(x => x.Type == "clientId").FirstOrDefault().Value,
        //                    //    clientSecret = client.Claims.Where(x => x.Type == "clientSecret").FirstOrDefault().Value,
        //                    //};
        //                    return client;
        //                }
        //            }
        //    }
        //    var clientEmpty = new Entities.Client();
        //    return clientEmpty;
        //}
    }
}
