using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using CurrencyRate.Infrastructure.Repository;
using CurrencyRate.Infrastructure.Services;
using CurrencyRate.Infrastructure.Services.ExternalAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Benchmark
{
    [RPlotExporter]
    public class CurrencyRepositoryBenchmarkTest
    {
        public const string CONNECTION_STRING = "Server = (localdb)\\mssqllocaldb; Database=CurrencyRate;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly ICurrencyRatesService _currencyRatesServiceWithCache = new CurrencyRatesService(new CurrencyRatesRetriver(),
            new CurrencyRepository(new ApplicationDbContext(new DbContextOptionsBuilder().UseSqlServer(CONNECTION_STRING).Options)));
        private readonly ICurrencyRatesService _currencyRatesServiceWithoutCache = new CurrencyRatesService(new CurrencyRatesRetriver(), new NoCurrencyRepository());
        DateTime startDate;
        DateTime endDate;
        public CurrencyRepositoryBenchmarkTest()
        {
        }
        private void CreateRandomDates()
        {
            var beginDate = DateTime.Parse("2019-06-01");
            var daysDifference = (int)(DateTime.Now - beginDate).TotalDays;
            var random = new Random();
            var firstRandom = random.Next(daysDifference);
            var secondRandom = random.Next(daysDifference - firstRandom);
            startDate = beginDate.AddDays(firstRandom);
            endDate = startDate.AddDays(secondRandom);
            Console.WriteLine($"{startDate} - {endDate}");
        }

        [Benchmark]
        public void WithoutCache()
        {

            CreateRandomDates();
            var result = _currencyRatesServiceWithoutCache.GetCurrencyExchangeRatesAsync
                   (new Dictionary<string, string>() { ["USD"] = "EUR" }, startDate, endDate).Result;
        }

        [Benchmark]
        public void WithCache()
        {
            CreateRandomDates();
            var result = _currencyRatesServiceWithCache.GetCurrencyExchangeRatesAsync
                   (new Dictionary<string, string>() { ["USD"] = "EUR" }, startDate, endDate).Result;

        }
    }
}
