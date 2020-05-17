using CurrencyRate.Infrastructure.Services.ExternalAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyRate.Tests
{
    [TestClass]
    public class CurrencyRatesRetreiverTests
    {
        private readonly ICurrencyRatesRetriver _currencyRatesRetriver;
        public CurrencyRatesRetreiverTests()
        {
            _currencyRatesRetriver = new CurrencyRatesRetriver();
        }

        [TestMethod]
        public void GetCurrencyExchangeRatesAsync_GetsExchangeRatesForWorkDays()
        {
            //Arrange
            var dateFrom = DateTime.Parse("2020-05-11"); //Monday
            var dateTo = DateTime.Parse("2020-05-15"); //Friday
            var currencyFrom = "USD";
            var currencyTo = "EUR";

            //Act
            var result = _currencyRatesRetriver.GetCurrencyExchangeRatesAsync(currencyFrom, currencyTo, dateFrom, dateTo).Result;

            //Assert
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result.TrueForAll(x => x.Value > 0));
            for(int i = 0; i < 5; i++)
            {
                Assert.IsTrue(result.Any(x => x.Date == dateFrom.AddDays(i)));
            }
        }

        [TestMethod]
        public void GetCurrencyExchangeRatesAsync_GetsExchangeRatesForWorkDaysNotForHolidays()
        {
            //Arrange
            var dateFrom = DateTime.Parse("2020-05-11"); //Monday
            var dateTo = DateTime.Parse("2020-05-17"); //Sunday
            var currencyFrom = "USD";
            var currencyTo = "EUR";

            //Act
            var result = _currencyRatesRetriver.GetCurrencyExchangeRatesAsync(currencyFrom, currencyTo, dateFrom, dateTo).Result;

            //Assert
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result.TrueForAll(x => x.Value > 0));
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(result.Any(x => x.Date == dateFrom.AddDays(i)));
            }
        }

        [TestMethod]
        public void GetCurrencyExchangeRatesAsync_GetsEmpyDataListForHolyday()
        {
            //Arrange
            var date = DateTime.Parse("2020-05-17"); //Sunday
            var currencyFrom = "USD";
            var currencyTo = "EUR";

            //Act
            var result = _currencyRatesRetriver.GetCurrencyExchangeRatesAsync(currencyFrom, currencyTo, date, date).Result;

            //Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
