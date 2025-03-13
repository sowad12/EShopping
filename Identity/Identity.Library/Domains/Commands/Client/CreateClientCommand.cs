using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.Library.Domains.Commands.Entities
{
    public class CreateClientCommand
    {
        //
        // Summary:
        //     Unique ID of the client
        //[Required(ErrorMessage = "Club Id can not be null")]
        //public long? ClubId { get; set; }
        [Required(ErrorMessage = "Client Id can not be null")]
        public string ClientId { get; set; }

        //
        // Summary:
        //     Client secrets - only relevant for flows that require a secret
        [Required(ErrorMessage = "Client Secret can not be null")]
        public ICollection<string> ClientSecrets { get; set; } = new HashSet<string>();


        //
        // Summary:
        //     If set to false, no client secret is needed to request tokens at the token endpoint
        public bool RequireClientSecret { get; set; }


        //
        // Summary:
        //     Client display name (used for logging and consent screen)
        [Required(ErrorMessage = "Client Name can not be null")]
        public string ClientName { get; set; }


        //
        // Summary:
        //     Specifies the allowed grant types (legal combinations of AuthorizationCode, Implicit,
        //     Hybrid, ResourceOwner, ClientCredentials).
        [Required(ErrorMessage = "Grant type can not be null")]
        public ICollection<string> AllowedGrantTypes { get; set; }

        public ICollection<string> AllowedScopes { get; set; }

        //
        // Summary:
        //     Specifies whether a proof key is required for authorization code based token
        //     requests (defaults to true).
        //public bool RequirePkce { get; set; } = true;


        //
        // Summary:
        //     Specifies allowed URIs to return tokens or authorization codes to
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

        //
        // Summary:
        //     Specifies allowed URIs to redirect to after logout
        public ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();


        //
        // Summary:
        //     Specifies logout URI at client for HTTP front-channel based logout.
        public string FrontChannelLogoutUri { get; set; }


        //
        // Summary:
        //     Specifies logout URI at client for HTTP back-channel based logout.
        public string BackChannelLogoutUri { get; set; }


    }


    public class UpdateClientCommand
    {
        //
        // Summary:
        //     Unique ID of the client
        [Required(ErrorMessage = "Club Id can not be null")]
        public long? ClubId { get; set; }
        [Required(ErrorMessage = "Client Id can not be null")]
        public string ClientId { get; set; }

        //
        // Summary:
        //     Client secrets - only relevant for flows that require a secret
        [Required(ErrorMessage = "Client Secret can not be null")]
        public ICollection<string> ClientSecrets { get; set; } = new HashSet<string>();


        //
        // Summary:
        //     If set to false, no client secret is needed to request tokens at the token endpoint
        public bool RequireClientSecret { get; set; }


        //
        // Summary:
        //     Client display name (used for logging and consent screen)
        [Required(ErrorMessage = "Client Name can not be null")]
        public string ClientName { get; set; }


        //
        // Summary:
        //     Specifies the allowed grant types (legal combinations of AuthorizationCode, Implicit,
        //     Hybrid, ResourceOwner, ClientCredentials).
        [Required(ErrorMessage = "Grant type can not be null")]
        public ICollection<string> AllowedGrantTypes { get; set; }

        public ICollection<string> AllowedScopes { get; set; }

        //
        // Summary:
        //     Specifies whether a proof key is required for authorization code based token
        //     requests (defaults to true).
        //public bool RequirePkce { get; set; } = true;


        //
        // Summary:
        //     Specifies allowed URIs to return tokens or authorization codes to
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();

        //
        // Summary:
        //     Specifies allowed URIs to redirect to after logout
        public ICollection<string> PostLogoutRedirectUris { get; set; } = new HashSet<string>();


        //
        // Summary:
        //     Specifies logout URI at client for HTTP front-channel based logout.
        public string FrontChannelLogoutUri { get; set; }


        //
        // Summary:
        //     Specifies logout URI at client for HTTP back-channel based logout.
        public string BackChannelLogoutUri { get; set; }


    }
}
