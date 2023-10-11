using IdentityServer4.Models;

namespace MyIAM.Core.Domain
{
    public class MyApiResource : ApiResource,IAMDatabaseKey
    {
        public Guid Id { get; set; }

    }
}
