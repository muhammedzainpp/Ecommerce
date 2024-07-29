using Ecommerce.Web.Shared.Reponses;
using MediatR;

namespace Ecommerce.Web.Application.Common.Interfaces.Mediatr;
public interface IQueryHandler< TRequest,TResponse>
                :IRequestHandler<TRequest,Response<TResponse>>
                    where TRequest : IQuery<TResponse>
{
}
