using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Core.Domain
{
    public class CurrencyExchangeRate : IBaseEntity
    {
        public string CurrencyFrom { get; set; }
        public string CurrrencyTo { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
