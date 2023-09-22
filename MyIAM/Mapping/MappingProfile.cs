using AutoMapper;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using MyIAM.AppService.Resources.MyApiResource;
using MyIAM.AppService.Resources.MyApiScope;
using MyIAM.AppService.Resources.MyClient;
using MyIAM.AppService.Resources.MyIdentityResource;
using MyIAM.Domain;

namespace MyIAM.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //AppService
            CreateMap<MyClient, MyClientOutPut>();
            CreateMap<MyClient, MyClientListOutPut>();
            CreateMap<MyClientInPut, MyClient>()
                .AfterMap((s,d,c) =>
                {
                    foreach(var item in s.ClientSecrets)
                        d.ClientSecrets.Add(new Secret(item.Sha256()));
                });

            CreateMap<MyApiScope, MyApiScopeOutPut>();
            CreateMap<MyApiScope, MyApiScopeListOutPut>();
            CreateMap<MyApiScopeInPut, MyApiScope>();

            CreateMap<MyApiResource, MyApiResourceOutPut>();
            CreateMap<MyApiResource, MyApiResourceListOutPut>();
            CreateMap<MyApiResourceInPut, MyApiResource>();

            CreateMap<MyIdentityResource, MyIdentityResourceOutPut>();
            CreateMap<MyIdentityResource, MyIdentityResourceListOutPut>();
            CreateMap<MyIdentityResourceInPut, MyIdentityResource>()
                .AfterMap((s,d,c) =>
                {
                    if (s.UserClaims.IsNullOrEmpty())
                        d.UserClaims = new HashSet<string> { "role" };
                });



            //Stores
            CreateMap<MyClient, Client>();
            CreateMap<MyApiResource, ApiResource>();
            CreateMap<MyApiScope, ApiScope>();
            CreateMap<MyIdentityResource, IdentityResource>()
                .AfterMap((s, d, c) =>
                {
                    if (s.UserClaims.IsNullOrEmpty())
                        d.UserClaims = new HashSet<string> { "role" };
                });

            CreateMap<MyApiScope, IdentityResource>()
                .AfterMap((s, d, c) =>
                {
                    if (s.UserClaims.IsNullOrEmpty())
                        d.UserClaims = new HashSet<string> { "role" };
                });
            CreateMap<MyApiResource, IdentityResource>()
                .AfterMap((s,d,c) =>
                {
                    if (s.UserClaims.IsNullOrEmpty())
                        d.UserClaims = new HashSet<string> { "role" };
                });

        }
    }
}
