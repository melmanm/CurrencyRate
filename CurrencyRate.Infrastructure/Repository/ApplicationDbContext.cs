using CurrencyRate.Core.Auth;
using CurrencyRate.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Infrastructure.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApiKey>().HasKey(x => x.Key);
            builder.Entity<CurrencyExchangeRate>().HasKey(x => new { x.CurrencyFrom, x.CurrrencyTo, x.Date });
            builder.Entity<CurrencyExchangeRate>().Property(p => p.Value).HasColumnType("decimal(18,10)");
        }
    }
}
