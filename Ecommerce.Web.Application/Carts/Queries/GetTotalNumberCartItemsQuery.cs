using Ecommerce.Web.Application.Common.Interfaces.Mediatr;

namespace Ecommerce.Web.Application.Carts.Queries;
public class GetTotalNumberCartItemsQuery : IQuery<int>
{
    public int UserId { get; set; }
}

