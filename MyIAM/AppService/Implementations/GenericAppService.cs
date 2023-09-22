using AutoMapper;
using MyIAM.AppService.Contracts;
using MyIAM.Databases.Contracts;
using MyIAM.Domain;

namespace MyIAM.AppService.Implementations
{
    public class GenericAppService<T, TOutPutResource, TListOutPutResource, TInPutResource> : IGenericAppService<T, TOutPutResource, TListOutPutResource, TInPutResource>
        where T : class,IAMDatabaseKey
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericAppService(IMapper mapper, IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TOutPutResource> GetById(Guid id)
            => _mapper.Map<T, TOutPutResource>(await _repository.GetByIdAsync(id));
        public async Task<List<TListOutPutResource>> GetAll()
            => _mapper.Map<List<T>, List<TListOutPutResource>>((List<T>)await _repository.GetAllAsync());

        public async Task<TOutPutResource> Add(TInPutResource entityResource)
        {
            var entity = _mapper.Map<TInPutResource, T>(entityResource);
            await _repository.InsertAsync(entity);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<T, TOutPutResource>(entity);
        }

        public async Task<TOutPutResource> Modify(Guid id, TInPutResource entity)
        {
            var oldEntity = await _repository.GetByIdAsync(id);

            if (oldEntity == null)
                return _mapper.Map<T, TOutPutResource>(oldEntity);


            _mapper.Map<TInPutResource, T>(entity, oldEntity);
            await _unitOfWork.CompleteAsync();


            return _mapper.Map<T, TOutPutResource>(await _repository.GetByIdAsync(id));
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _repository.Remove(id);
            await _unitOfWork.CompleteAsync();
            return id;
        }
    }
}
