using CurrencyRate.Core.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.Auth
{
    public class ApiKeyService : IApiKeyService
    {
        public ApiKey GenerateApiKey()
        {
            throw new NotImplementedException();
        }

        public Task StoreApiKey(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyApiKey(string key)
        {
            return true;
        }
    }
}
