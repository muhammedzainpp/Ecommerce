
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

                var problemDetails = new Response<Empty>
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
    