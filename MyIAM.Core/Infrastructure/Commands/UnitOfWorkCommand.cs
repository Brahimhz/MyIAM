using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIAM.Core.Infrastructure.Commands
{
    public record UnitOfWorkCommand() : IRequest<Task>;
}
