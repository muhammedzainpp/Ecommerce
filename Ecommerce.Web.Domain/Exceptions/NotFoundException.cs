namespace Ecommerce.Web.Domain.Exceptions;
public class NotFoundException(string message) : ApplicationException("Not Found", message)
{
}
