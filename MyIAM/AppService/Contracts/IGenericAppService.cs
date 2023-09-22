using MyIAM.Domain;

namespace MyIAM.AppService.Contracts
{
    public interface IGenericAppService<T, TOutPutResource, TListOutPutResource, TInPutResource>
        where T : class , IAMDatabaseKey
    {
        Task<TOutPutResource> GetById(Guid id);
        Task<List<TListOutPutResource>> GetAll();

        Task<TOutPutResource> Add(TInPutResource entity);
        Task<TOutPutResource> Modify(Guid id, TInPutResource entity);
        Task<Guid> Delete(Guid id);
    }
}
