using CurrencyRate.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.ExternalAPI
{
    public interface ICurrencyRatesService
    {
        Task<List<CurrencyExchangeRate>> GetCurrencyExchangeRatesAsync(Dictionary<string, string> currencyCodes, DateTime from, DateTime to);

    }
}
