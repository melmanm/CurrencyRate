using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRate.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRate.API.Controllers
{
    [Route("api/key")]
    [ApiController]
    public class ApiKeyProviderController : ControllerBase
    {
        private readonly IApiKeyService _apiKeyService;

        public ApiKeyProviderController(IApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var key = _apiKeyService.GenerateApiKey();
                await _apiKeyService.StoreApiKey(key);
                return Ok(key.Key);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}