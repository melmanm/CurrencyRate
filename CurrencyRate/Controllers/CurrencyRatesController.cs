using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRate.Infrastructure.Services.ExternalAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRate.API.Controllers
{
    [Route("api/rates")]
    [ApiController]
    public class CurrencyRatesController : ControllerBase
    {
        private readonly ICurrencyRatesService _currencyRatesService;

        public CurrencyRatesController(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await _currencyRatesService.GetCurrencyExchangeRatesAsync(
                
                new Dictionary<string, string>() { ["USD"] = "EUR" }, DateTime.Parse("2020-01-01"), DateTime.Parse("2020-01-07"));
            return "";
        }
    }
}