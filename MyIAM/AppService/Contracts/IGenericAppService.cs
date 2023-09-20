using MyIAM.Domain;

namespace MyIAM.AppService.Contracts
{
    public interface IGenericAppService<T, TGetResource, TSetResource>
        where T : class , IAMDatabaseKey
    {
        Task<TGetResource> GetById(Guid id);
        Task<List<TGetResource>> GetAll();

        Task<TGetResource> Add(TSetResource entity);
        Task<TGetResource> Modify(Guid id, TSetResource entity);
        Task<Guid> Delete(Guid id);
    }
}
