using MediatR;
using MyIAM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyIAM.Core.Infrastructure.Commands
{
    public record AddGenericCommand<T>(T input) : IRequest<T> where T : class, IAMDatabaseKey;
    public record ModifyGenericCommand<T>(T input,Expression<Func<T, bool>>? condition) : IRequest<T> where T : class, IAMDatabaseKey;
    public record DeleteGenericCommand<T>(T input,Expression<Func<T, bool>>? condition) : IRequest<bool> where T : class, IAMDatabaseKey;
}
