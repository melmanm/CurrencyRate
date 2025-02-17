﻿using CurrencyRate.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Repository
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyExchangeRate>> GetAsync(string currencyFrom, string currencyTo, DateTime from, DateTime to);

        Task<bool> IsDataAvailable(string currencyFrom, string currencyTo, DateTime from, DateTime to);

        Task StoreAsync(List<CurrencyExchangeRate> entities);
    }
}
