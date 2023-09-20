using IdentityServer4.Models;

namespace MyIAM.Domain
{
    public class MyIdentityResource : IdentityResource , IAMDatabaseKey
    {
        public Guid Id { get; set; }
    }
}
