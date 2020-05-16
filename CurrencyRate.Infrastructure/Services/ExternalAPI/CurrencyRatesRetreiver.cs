using CurrencyRate.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyRate.Infrastructure.Services.ExternalAPI
{
    public class CurrencyRatesRetriver : ICurrencyRatesRetriver
    {
        public async Task<List<CurrencyExchangeRate>> GetCurrencyExchangeRatesAsync(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            var result = new List<CurrencyExchangeRate>();

            using (var httpClient = new HttpClient())
            {
                var path = ConstructPath(currencyFrom, currencyTo, from, to);
                using (var response = await httpClient.GetAsync(path))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    XDocument doc = XDocument.Parse(apiResponse);
                    var dataSeries = doc.Root.Elements().First(x => x.Name.LocalName == "DataSet")
                     .Elements().First(x => x.Name.LocalName == "Series")
                     .Elements().Where(x => x.Name.LocalName == "Obs");

                    foreach(var dataSerie in dataSeries)
                    {
                        var date = dataSerie.Elements().First(x => x.Name.LocalName == "ObsDimension").Attribute("value").Value;
                        var value = dataSerie.Elements().First(x => x.Name.LocalName == "ObsValue").Attribute("value").Value;

                        result.Add(new CurrencyExchangeRate()
                        {
                            CurrencyFrom = currencyFrom,
                            CurrrencyTo = currencyTo,
                            Date = DateTime.Parse(date),
                            Value = Decimal.Parse(value)
                        });
                    }
                }
                return result;
            }
        }

        private string ConstructPath(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            return $"https://sdw-wsrest.ecb.europa.eu/service/data/EXR/D.{currencyFrom}.{currencyTo}.SP00.A?startPeriod={from.ToString("yyyy-MM-dd")}&endPeriod={to.ToString("yyyy-MM-dd")}";
        }
    }
}
