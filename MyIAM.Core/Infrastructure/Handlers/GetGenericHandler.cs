using MediatR;
using MyIAM.Core.Databases.Contracts;
using MyIAM.Core.Domain;
using MyIAM.Core.Infrastructure.Queries;

namespace MyIAM.Core.Infrastructure.Handlers
{

    public class GetGenericByPropHandler<T> : IRequestHandler<GetGenericByPropQuery<T>, T?>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public GetGenericByPropHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public Task<T?> Handle(GetGenericByPropQuery<T> request, CancellationToken cancellationToken)
        {
            return _repository.GetByPropAsync(request.condition);
        }
    }

    public class GetGenericListHandler<T> : IRequestHandler<GetGenericListQuery<T>, IEnumerable<T>>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public GetGenericListHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<T>> Handle(GetGenericListQuery<T> request, CancellationToken cancellationToken)
        {
            return _repository.WhereAsync(request.condition);
        }
    }
}
