using Ecommerce.Web.Domain.ValueObjects;
using Ecommerce.Web.Shared.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Web.Application.Common.Extension;
public static class CostExtensions
{
    public static MoneyDto ConvertToMoneyDto(this Money money)
    {
        return new MoneyDto()
        {
            Amount = money.Amount,
            Currency =  money.Currency.ConvertToCurrencyDto(),
        };
    }

    public static CurrencyDto ConvertToCurrencyDto(this Currency currency)
    {
        return new CurrencyDto()
        {
            Name = currency.Name,
            Symbol = currency.Symbol,
        };
    }

    public static Money ConvertToMoney(this MoneyDto moneyDto)
    {
        Money money = Money.Create(moneyDto.Currency.Name,
                                   moneyDto.Currency.Symbol,
                                   moneyDto.Amount);
        return money;
    }
}
