using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexExportParser.Entities
{
    public class LendingSummaryEntity
    {
        public decimal totalAmount { get; set; }
        public decimal totalRate { get; set; }
        public decimal totalInterest { get; set; }
        public decimal totalFee { get; set; }
        public decimal totalEarned { get; set; }
        public int totalCount { get; set; }
        public decimal dollarValue { get; set; }
    }
}
