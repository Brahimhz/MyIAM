using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using MyIAM.Core.Domain;
using Newtonsoft.Json;

namespace MyIAM.Persistance
{
    public class IAMDbContext : DbContext
    {
        public IAMDbContext(DbContextOptions<IAMDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<MyClient> Clients { get; set; }
        public DbSet<MyApiScope> ApiScopes { get; set; }
        public DbSet<MyApiResource> ApiResources { get; set; }
        public DbSet<MyIdentityResource> IdentityResources { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Client
            MyClientFluentApi(modelBuilder);

            //ApiResource
            MyApiResourceFluentApi(modelBuilder);

            //ApiScope
            MyApiScopeFluentApi(modelBuilder);

            //IdentityResource
            MyIdentityResourceFluentApi(modelBuilder);

        }

        private void MyIdentityResourceFluentApi(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyIdentityResource>()
            .Property(c => c.Properties)
            .HasConversion(
                properties => JsonConvert.SerializeObject(properties), // Convert to JSON string
                json => (IDictionary<string, string>)JsonConvert.DeserializeObject<IDictionary<string, string>>(json) // Convert back to ICollection<Claim>
            );

            modelBuilder.Entity<MyIdentityResource>()
           .Property(c => c.UserClaims)
           .HasConversion(
               v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
           );

        }

        private void MyApiScopeFluentApi(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyApiScope>()
            .Property(c => c.Properties)
            .HasConversion(
                properties => JsonConvert.SerializeObject(properties), // Convert to JSON string
                json => (IDictionary<string, string>)JsonConvert.DeserializeObject<IDictionary<string, string>>(json) // Convert back to ICollection<Claim>
            );

            modelBuilder.Entity<MyApiScope>()
            .Property(c => c.UserClaims)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            
        }

        private void MyApiResourceFluentApi(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyApiResource>()
            .Property(c => c.AllowedAccessTokenSigningAlgorithms)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );


            modelBuilder.Entity<MyApiResource>()
            .Property(c => c.Properties)
            .HasConversion(
                properties => JsonConvert.SerializeObject(properties), // Convert to JSON string
                json => (IDictionary<string, string>)JsonConvert.DeserializeObject<IDictionary<string, string>>(json) // Convert back to ICollection<Claim>
            );

            modelBuilder.Entity<MyApiResource>()
            .Property(c => c.Scopes)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyApiResource>()
            .Property(c => c.UserClaims)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyApiResource>()
               .Property(c => c.ApiSecrets)
               .HasConversion(
                   clientSecrets => JsonConvert.SerializeObject(clientSecrets), // Convert to JSON string
                   json => (ICollection<Secret>)JsonConvert.DeserializeObject<List<Secret>>(json) // Convert back to ICollection<Claim>
               );
        }

        private void MyClientFluentApi(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyClient>()
            .Property(c => c.AllowedCorsOrigins)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.AllowedGrantTypes)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.AllowedIdentityTokenSigningAlgorithms)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.AllowedScopes)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.IdentityProviderRestrictions)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.PostLogoutRedirectUris)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Ignore(c => c.Properties);

            modelBuilder.Entity<MyClient>()
            .Property(c => c.RedirectUris)
            .HasConversion(
                v => string.Join(',', v), // Convert ICollection<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Convert back to ICollection<string>
            );

            modelBuilder.Entity<MyClient>()
            .Property(c => c.Claims)
            .HasConversion(
                claims => JsonConvert.SerializeObject(claims), // Convert to JSON string
                json => (ICollection<ClientClaim>)JsonConvert.DeserializeObject<List<ClientClaim>>(json) // Convert back to ICollection<Claim>
            );

            modelBuilder.Entity<MyClient>()
               .Property(c => c.ClientSecrets)
               .HasConversion(
                   clientSecrets => JsonConvert.SerializeObject(clientSecrets), // Convert to JSON string
                   json => (ICollection<Secret>)JsonConvert.DeserializeObject<List<Secret>>(json) // Convert back to ICollection<Claim>
               );
        }
    }
}
