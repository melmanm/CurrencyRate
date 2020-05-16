using CurrencyRate.Core.Domain;
using CurrencyRate.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Benchmark
{
    public class NoCurrencyRepository : ICurrencyRepository
    {
        public async Task<List<CurrencyExchangeRate>> GetAsync(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            return new List<CurrencyExchangeRate>();
        }

        public async Task<bool> IsDataAvailable(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            return false;
        }

        public async Task StoreAsync(List<CurrencyExchangeRate> entities)
        {
        }
    }
}
