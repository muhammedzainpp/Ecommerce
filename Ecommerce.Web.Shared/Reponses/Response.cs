namespace Ecommerce.Web.Shared.Reponses;
public record Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
