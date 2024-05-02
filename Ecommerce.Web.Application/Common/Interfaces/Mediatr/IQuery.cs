using Ecommerce.Web.Shared.Reponses;
using MediatR;

namespace Ecommerce.Web.Application.Common.Interfaces.Mediatr;
public interface IQuery<TResponse> : IRequest<Response<TResponse>>
{
}
