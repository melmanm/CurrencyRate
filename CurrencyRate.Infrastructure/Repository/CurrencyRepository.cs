using CurrencyRate.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<CurrencyExchangeRate> entities;

        public CurrencyRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CurrencyExchangeRate>();
        }

        public async Task<List<CurrencyExchangeRate>> GetAsync(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            return await entities.Where(x => x.CurrencyFrom == currencyFrom && x.CurrrencyTo == currencyTo
            && x.Date >= from && x.Date <= to).ToListAsync();
        }

        public async Task<bool> IsDataAvailable(string currencyFrom, string currencyTo, DateTime from, DateTime to)
        {
            var count = await entities.Where(x => x.CurrencyFrom == currencyFrom && x.CurrrencyTo == currencyTo
                        && x.Date >= from && x.Date <= to).CountAsync();

            return count == (to - from).TotalDays;
        }

        public async Task StoreAsync(List<CurrencyExchangeRate> list)
        {
            var toAdd = list.Where(x =>!entities.Any(y => y.CurrencyFrom == x.CurrencyFrom 
            && y.CurrrencyTo == x.CurrrencyTo && y.Value == x.Value));

            entities.AddRange(toAdd);

            await context.SaveChangesAsync();
        }
    }
}
