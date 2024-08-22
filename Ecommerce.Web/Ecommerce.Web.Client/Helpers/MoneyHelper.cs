using Ecommerce.Web.Shared.Common.Dtos;

namespace Ecommerce.Web.Client.Helpers;

public static class MoneyHelper
{
    public static MoneyDto ToExactMoney(this MoneyDto money,decimal quantity)
    {
        return new MoneyDto
        {
            Currency = money.Currency,
            Amount = money.Amount * quantity,
        };
    }
}
