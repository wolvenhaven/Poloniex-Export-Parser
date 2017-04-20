using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexExportParser.Entities
{
    public class LendingExportEntity
    {
        public string currencyName { get; set; }
        public decimal lendingRate { get; set; }
        public decimal amountLent { get; set; }
        public decimal interestAmount { get; set; }
        public decimal feeAmount { get; set; }
        public decimal earnedAmount { get; set; }
        public DateTime loanStart { get; set; }
        public DateTime loanEnd { get; set; }
    }
}
