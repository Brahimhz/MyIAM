using MediatR;
using MyIAM.Core.Databases.Contracts;
using MyIAM.Core.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIAM.Core.Infrastructure.Handlers
{
    public class UnitOfWorkHandler : IRequestHandler<UnitOfWorkCommand, Task>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Task> Handle(UnitOfWorkCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.CompleteAsync());
        }
    }
}
