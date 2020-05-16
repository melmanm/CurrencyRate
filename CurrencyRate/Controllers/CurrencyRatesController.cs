using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRate.API.Models;
using CurrencyRate.Infrastructure.Services;
using CurrencyRate.Infrastructure.Services.ExternalAPI;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Get([FromBody] GetCurrencyRatesModel model)
        {
            if(model.endDate < model.startDate)
            {
                return NotFound("Start date is grater than end date");
            }
            if(model.startDate > DateTime.Now)
            {
                return NotFound("Start date is grater than current date");
            }
            if (model.endDate > DateTime.Now)
            {
                return NotFound("End date is grater than current date");
            }
            try
            {
                var result = await _currencyRatesService.GetCurrencyExchangeRatesAsync(
                    model.currencyCodes, model.startDate, model.endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}