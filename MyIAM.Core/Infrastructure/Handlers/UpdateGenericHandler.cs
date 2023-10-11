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
    public class AddGenericHandler<T> : IRequestHandler<AddGenericCommand<T>, T>
        where T : class, IAMDatabaseKey
    {
        private readonly IGenericRepository<T> _repository;

        public AddGenericHandler(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<T> Handle(AddGenericCommand<T> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
