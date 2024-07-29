using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Client.Services;

public interface IApiService
{
    Task<Response<T>> Get<T>(string url);
    Task<Response<T>> GetById<T>(string url, int id);
    Task<Response<int>> Post<T>(string url, T request);
    Task<Response<TResponse>> Post<TRequest, TResponse>(string url, TRequest request);
}
