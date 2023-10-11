using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyIAM.Core.Infrastructure.Queries
{
    public record GetGenericQuery<T>(Expression<Func<T,bool>>? condition) : IRequest<T>;
    public record GetGenericListQuery<T>(Expression<Func<T,bool>>? condition) : IRequest<List<T>>;
}
