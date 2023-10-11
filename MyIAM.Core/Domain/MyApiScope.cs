using IdentityServer4.Models;

namespace MyIAM.Core.Domain
{
    public class MyApiScope : ApiScope, IAMDatabaseKey
    {
        public Guid Id { get; set; }
    }
}
