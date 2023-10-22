using AutoMapper;
using MediatR;
using MyIAM.Admin.Application.Contracts;
using MyIAM.Core.Domain;
using MyIAM.Core.Infrastructure.Commands;
using MyIAM.Core.Infrastructure.Queries;

namespace MyIAM.Admin.Application.Implementations
{
    public class GenericAppService<T, TOutPutResource, TListOutPutResource, TInPutResource> : IGenericAppService<T, TOutPutResource, TListOutPutResource, TInPutResource>
        where T : class,IAMDatabaseKey
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GenericAppService(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TOutPutResource> GetById(Guid id)
            => _mapper.Map<T?, TOutPutResource>
            (await _mediator.Send(new GetGenericByPropQuery<T>(e => e.Id == id)));
        public async Task<List<TListOutPutResource>> GetAll()
            => _mapper.Map<List<T>, List<TListOutPutResource>>
                ((List<T>)
                await _mediator.Send(new GetGenericListQuery<T>(null)));

        public async Task<TOutPutResource> Add(TInPutResource entityResource)
        {
            var entity = _mapper.Map<TInPutResource, T>(entityResource);
            await _mediator.Send(new AddGenericCommand<T>(entity));

            await _mediator.Send(new UnitOfWorkCommand());

            return _mapper.Map<T, TOutPutResource>(entity);
        }

        public async Task<TOutPutResource> Modify(Guid id, TInPutResource entity)
        {
            var oldEntity = await _mediator.Send(new GetGenericByPropQuery<T>(e => e.Id == id));

            if (oldEntity == null)
                return _mapper.Map<T?, TOutPutResource>(oldEntity);


            _mapper.Map(entity, oldEntity);
            //Update Commad !!!!!!
            await _mediator.Send(new UnitOfWorkCommand());


            return _mapper.Map<T?, TOutPutResource>
                (await _mediator.Send(new GetGenericByPropQuery<T>(e => e.Id == id)));
        }

        public async Task<Guid?> Delete(Guid id)
        {
            var deleteEntity = await _mediator.Send(new GetGenericByPropQuery<T>(e => e.Id == id));

            if (deleteEntity == null)
                return null;
                
            await _mediator.Send(new DeleteGenericCommand<T>(deleteEntity));
            await _mediator.Send(new UnitOfWorkCommand());
            return id;
        }
    }
}
