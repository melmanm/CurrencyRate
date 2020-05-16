using CurrencyRate.Core.Domain;
using CurrencyRate.Infrastructure.Repository;
using CurrencyRate.Infrastructure.Services.ExternalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private readonly ICurrencyRatesRetriver _currencyRatesRetriver;
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyRatesService(ICurrencyRatesRetriver currencyRatesRetriver, ICurrencyRepository currencyRepository)
        {
            _currencyRatesRetriver = currencyRatesRetriver;
            _currencyRepository = currencyRepository;
        }

        public async Task<List<CurrencyExchangeRate>> GetCurrencyExchangeRatesAsync(Dictionary<string, string> currencyCodes, DateTime from, DateTime to)
        {
            var result = new List<CurrencyExchangeRate>();
            foreach (var currencyCode in currencyCodes)
            {

                List<CurrencyExchangeRate> currencyRates;

                //chceck if data is available in db otherwise ask external api
                if (await _currencyRepository.IsDataAvailable(currencyCode.Key, currencyCode.Value, from, to))
                {
                    currencyRates = await _currencyRepository.GetAsync(currencyCode.Key, currencyCode.Value, from, to);
                }
                else
                {
                    currencyRates = await _currencyRatesRetriver.GetCurrencyExchangeRatesAsync(currencyCode.Key, currencyCode.Value, from, to);

                    //resolve missing days
                    var requiredDates = Enumerable.Range(0, 1 + to.Subtract(from).Days)
                        .Select(offset => from.AddDays(offset))
                        .ToArray();

                    var missingDates = requiredDates.Where(x => !currencyRates.Any(y => y.Date == x));
                    foreach (var missingDate in missingDates.OrderBy(x => x.Date))
                    {
                        var previousRate = currencyRates.FirstOrDefault(x => x.Date == missingDate.Date.AddDays(-1));
                        if (previousRate != null)
                        {
                            currencyRates.Add(new CurrencyExchangeRate()
                            {
                                CurrencyFrom = previousRate.CurrencyFrom,
                                CurrrencyTo = previousRate.CurrrencyTo,
                                Date = missingDate,
                                Value = previousRate.Value,
                                IsFromPreviousDay = true
                            });
                        }
                        else
                        {
                            var offset = 30;
                            var pastRates = await _currencyRatesRetriver.GetCurrencyExchangeRatesAsync(currencyCode.Key, currencyCode.Value, missingDate.Date.AddDays(-1 * offset), missingDate);
                            for (int i = 1; i < offset; i++)
                            {
                                var found = pastRates.FirstOrDefault(x => x.Date == missingDate.AddDays(-1 * i));
                                if (found != null)
                                {
                                    found.IsFromPreviousDay = true;
                                    found.Date = missingDate;
                                    currencyRates.Add(found);
                                    break;
                                }
                            }
                        }
                    }
                    await _currencyRepository.StoreAsync(currencyRates);
                }
                result.AddRange(currencyRates.OrderBy(x => x.Date));
            }
            return result;
        }
    }
}
