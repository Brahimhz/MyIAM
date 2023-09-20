using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using MyIAM.Databases.Contracts;
using MyIAM.Domain;

namespace MyIAM.Configurations.Store
{
    public class ClientStore : IClientStore
    {
        private readonly IGenericRepository<MyClient> _repository;
        private readonly IMapper _mapper;

        public ClientStore(IGenericRepository<MyClient> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await _repository.FindAsync(c => c.ClientId == clientId);

            return client == null 
                ?  new Client()
                : _mapper.Map<MyClient,Client>(client);
        }
    }
}
