using AutoMapper;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using MyIAM.Databases.Contracts;
using MyIAM.Domain;
using System.Linq;

namespace MyIAM.Configurations.Store
{
    public class ResourceStore : IResourceStore
    {
        private readonly IGenericRepository<MyApiResource> _apiRepository;
        private readonly IGenericRepository<MyApiScope> _scopeRepository;
        private readonly IGenericRepository<MyIdentityResource> _identityRepository;
        private readonly IMapper _mapper;

        public ResourceStore(
            IGenericRepository<MyApiResource> apiRepository,
            IGenericRepository<MyApiScope> scopeRepository,
            IGenericRepository<MyIdentityResource> identityRepository,
            IMapper mapper)
        {
            _apiRepository = apiRepository;
            _scopeRepository = scopeRepository;
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = await _apiRepository.FindAsync(r => r.Name == name);

            return api == null
                ? new ApiResource()
                : _mapper.Map<MyApiResource, ApiResource>(api);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var apis = await _apiRepository.WhereAsync(r => apiResourceNames.Contains(r.Name));

            return apis == null
                ? new List<ApiResource>().AsEnumerable()
                : _mapper.Map<IEnumerable<MyApiResource>, IEnumerable<ApiResource>>(apis);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var apis = await _apiRepository.GetAllAsync();

            var result = new List<MyApiResource>();
            foreach (var scope in scopeNames)
                result.AddRange(apis.Where(a => a.Scopes.Contains(scope)));

            IEnumerable<MyApiResource> finalResult = new List<MyApiResource>();
            finalResult = result.Distinct();

            return  _mapper.Map<IEnumerable<MyApiResource>, IEnumerable<ApiResource>>(finalResult);
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = await _scopeRepository.WhereAsync(r => scopeNames.Contains(r.Name));

            return scopes == null
                ? new List<ApiScope>().AsEnumerable()
                : _mapper.Map<IEnumerable<MyApiScope>, IEnumerable<ApiScope>>(scopes);
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var result = new List<IdentityResource>();
            
            var identities = await _identityRepository.WhereAsync(e => scopeNames.Contains(e.Name));
            if (identities != null) result.AddRange(_mapper.Map<IEnumerable<MyIdentityResource>, IEnumerable<IdentityResource>>(identities));
            /*
            var scopes = await _scopeRepository.WhereAsync(s => scopeNames.Contains(s.Name));
            scopes = scopes.Where(s => !result.Any(r => r.Name == s.Name)).ToList();
            if (scopes != null)
            {
                foreach(var scope in scopes)
                    if (scope.UserClaims.IsNullOrEmpty())
                        scope.UserClaims = new HashSet<string> { "role" };
                result.AddRange(_mapper.Map<IEnumerable<MyApiScope>, IEnumerable<IdentityResource>>(scopes));
            }

            var apis = await _apiRepository.WhereAsync(a => scopeNames.Contains(a.Name));
            apis = apis.Where(a => !result.Any(r => r.Name == a.Name)).ToList();
            if (apis != null)
            {
                foreach (var api in apis)
                    if (api.UserClaims.IsNullOrEmpty())
                        api.UserClaims = new HashSet<string> { "role" };

                result.AddRange(_mapper.Map<IEnumerable<MyApiResource>, IEnumerable<IdentityResource>>(apis));
            }

            */
            return result.Distinct().AsEnumerable();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var result = new Resources(await _identityRepository.GetAllAsync(), await _apiRepository.GetAllAsync(), await _scopeRepository.GetAllAsync());
            return result;
        }


        public async Task<ApiScope> FindApiScopeAsync(string name)
        {
            var scope = await _scopeRepository.FindAsync(r => r.Name == name);

            return scope == null
                ? new ApiScope()
                : _mapper.Map<MyApiScope, ApiScope>(scope);
        }
    }
}
