using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRate.API.Models
{
    public class GetCurrencyRatesModel
    {
        [Required]
        public Dictionary<string, string> currencyCodes { get; set; }
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
    }
}
