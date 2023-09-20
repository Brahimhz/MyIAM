using IdentityServer4.Models;

namespace MyIAM.AppService.Resources.MyClient
{
    public class MyClientOutPut
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public ICollection<string> RedirectUris { get; set; } = new HashSet<string>();
        public ICollection<string> AllowedGrantTypes { get; set; } = new HashSet<string>();
        public ICollection<Secret> ClientSecrets { get; set; } = new HashSet<Secret>();
        public ICollection<string> AllowedScopes { get; set; } = new HashSet<string>();
    }
}
