using CurrencyRate.Core.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.Auth
{
    public interface IApiKeyService
    {
        ApiKey GenerateApiKey();
        Task StoreApiKey(string key);
        Task<bool> VerifyApiKey(string key);
    }
}
