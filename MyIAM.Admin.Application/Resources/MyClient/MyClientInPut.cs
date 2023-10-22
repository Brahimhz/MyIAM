using IdentityServer4.Models;

namespace MyIAM.Admin.Application.Resources.MyClient
{
    public class MyClientInPut
    {
        public string ClientId { get; set; }
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();
        public ICollection<string> AllowedGrantTypes { get; set; } = new HashSet<string>();
        public ICollection<string> ClientSecrets { get; set; } = new HashSet<string>();
        public ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();
    }
}
