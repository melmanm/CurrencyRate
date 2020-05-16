using CurrencyRate.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.ExternalAPI
{
    public interface ICurrencyRatesRetriver
    {
        Task<List<CurrencyExchangeRate>> GetCurrencyExchangeRatesAsync(string currencyFrom, string currencyTo, DateTime from, DateTime to);
    }
}
