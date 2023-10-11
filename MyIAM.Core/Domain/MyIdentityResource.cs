using IdentityServer4.Models;

namespace MyIAM.Core.Domain
{
    public class MyIdentityResource : IdentityResource , IAMDatabaseKey
    {
        public Guid Id { get; set; }
    }
}
