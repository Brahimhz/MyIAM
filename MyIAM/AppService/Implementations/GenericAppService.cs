using AutoMapper;
using MyIAM.AppService.Contracts;
using MyIAM.Databases.Contracts;
using MyIAM.Domain;

namespace MyIAM.AppService.Implementations
{
    public class GenericAppService<T, TGetResource, TSetResource> : IGenericAppService<T, TGetResource, TSetResource>
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

        public async Task<TGetResource> GetById(Guid id)
            => _mapper.Map<T, TGetResource>(await _repository.GetByIdAsync(id));
        public async Task<List<TGetResource>> GetAll()
            => _mapper.Map<List<T>, List<TGetResource>>((List<T>)await _repository.GetAllAsync());

        public async Task<TGetResource> Add(TSetResource entityResource)
        {
            var entity = _mapper.Map<TSetResource, T>(entityResource);
            await _repository.InsertAsync(entity);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<T, TGetResource>(entity);
        }

        public async Task<TGetResource> Modify(Guid id, TSetResource entity)
        {
            var oldEntity = await _repository.GetByIdAsync(id);

            if (oldEntity == null)
                return _mapper.Map<T, TGetResource>(oldEntity);


            _mapper.Map<TSetResource, T>(entity, oldEntity);
            await _unitOfWork.CompleteAsync();


            return _mapper.Map<T, TGetResource>(await _repository.GetByIdAsync(id));
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _repository.Remove(id);
            await _unitOfWork.CompleteAsync();
            return id;
        }
    }
}
