using CurrencyRate.Core.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Repository
{
    public interface IApiKeyRepository
    {
        Task<ApiKey> GetApiKeyAsync(string keyText);
        Task StoreApiKeyAsync(ApiKey key);
    }
}
