using CurrencyRate.Core.Auth;
using CurrencyRate.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.Auth
{
    public class ApiKeyService : IApiKeyService
    {

        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public ApiKey GenerateApiKey()
        {
            return new ApiKey()
            {
                Key = Guid.NewGuid().ToString(),
            };
        }

        public async Task StoreApiKey(ApiKey key)
        {
            await _apiKeyRepository.StoreApiKeyAsync(key);
        }

        public async Task<bool> VerifyApiKey(string key)
        {
            var existingKey = await _apiKeyRepository.GetApiKeyAsync(key);
            return existingKey != null;
        }
    }
}
