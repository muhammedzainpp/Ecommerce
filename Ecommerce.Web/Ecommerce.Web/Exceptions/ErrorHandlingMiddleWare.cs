
using Ecommerce.Web.Shared.Reponses;
using FluentValidation;
namespace Ecommerce.Web.Exceptions;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (exception.GetType() == typeof(ValidationException))
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", ((ValidationException)exception).Errors);

                var problemDetails = new
                {
                    Errors = ((ValidationException)exception).Errors.Select(x=>x.ErrorMessage),
                    IsSuccess = false
                };

                context.Response.StatusCode =
                    StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            else
            {
                _logger.LogError(
               exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new Response<string>
                {
                    Result =  null,
                    Errors =new[] { exception.Message },
                    IsSuccess = false
                };

                context.Response.StatusCode =
                    StatusCodes.Status200OK;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }

        }
    }
}
    //private readonly RequestDelegate next;

    //public ErrorHandlingMiddleware(RequestDelegate next)
    //{
    //    this.next = next;
    //}

    //public async Task Invoke(HttpContext context /* other dependencies */)
    //{
    //    try
    //    {
    //        await next(context);
    //    }
    //    catch (Exception ex)
    //    {
    //        await HandleExceptionAsync(context, ex);
    //    }
    //}


    //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    //{
    //    // Log issues and handle exception response
        
    //    if (exception.GetType() == typeof(ValidationException))
    //    {
    //        var result = ((ValidationException)exception).Errors.First().ErrorMessage;
    //        var customresult = new Response<Empty>() { Errors = new[] { result }, IsSuccess = false };
    //        return context.Response.WriteAsJsonAsync<Response<Empty>>(customresult);
    //    }
    //    else
    //    {
    //        var code = HttpStatusCode.InternalServerError;
    //        var result = JsonConvert.SerializeObject(new { isSuccess = false, error = exception.Message });
    //        context.Response.ContentType = "application/json";
    //        context.Response.StatusCode = (int)code;
    //        return context.Response.WriteAsync(result);
    //    }
    //}
