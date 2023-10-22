using MediatR;
using MyIAM.Core.Databases.Contracts;
using MyIAM.Core.Domain;
using MyIAM.Core.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIAM.Core.Infrastructure.Handlers
{
    public class AddGenericHandler<T> : IRequestHandler<AddGenericCommand<T>, Task>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public AddGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<Task> Handle(AddGenericCommand<T> request, CancellationToken cancellationToken)
        {
            return Task.FromResult( _repository.InsertAsync(request.input));
        }
    }

    public class ModifyGenericHandler<T> : IRequestHandler<ModifyGenericCommand<T>, Unit>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public ModifyGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(ModifyGenericCommand<T> request, CancellationToken cancellationToken)
        {
            _repository.Update(request.input);
            return Task.FromResult(Unit.Value);
        }
    }


    public class DeleteGenericHandler<T> : IRequestHandler<DeleteGenericCommand<T>, Task>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public DeleteGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<Task> Handle(DeleteGenericCommand<T> request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Remove(request.input.Id));
        }
    }
}
