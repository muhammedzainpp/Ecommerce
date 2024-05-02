using Ecommerce.Web.Shared.Reponses;

namespace Ecommerce.Web.Application.Common.Helpers;
public static class ResponseHelpers
{
    public static Response<T> OnSuccess<T>(T response)
        => new Response<T> { IsSuccess = true, Result = response };

    public static Response<T> OnError<T>(Exception ex)
       => new Response<T> { IsSuccess = false,Errors = [ex.Message]};

}
