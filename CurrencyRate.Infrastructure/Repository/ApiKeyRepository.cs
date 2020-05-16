using CurrencyRate.Core.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Repository
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ApiKey> entities;
        public ApiKeyRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ApiKey>();
        }

        public async Task<ApiKey> GetApiKeyAsync(string keyText)
        {
            return await entities.FirstOrDefaultAsync(x => x.Key == keyText);
        }

        public async Task StoreApiKeyAsync(ApiKey key)
        {
            await entities.AddAsync(key);
            await context.SaveChangesAsync();
        }
    }
}
