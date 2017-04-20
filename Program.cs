using PoloniexExportParser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoloniexExportParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Export file path: ");
            string input = Console.ReadLine();

            var contents = File.ReadAllText(input).Split('\n');
            var csv = from line in contents select line.Split(',').ToArray();

            List<LendingExportEntity> lending = new List<LendingExportEntity>();

            foreach(var row in csv.Skip(1))
            {
                lending.Add(new LendingExportEntity
                {
                    currencyName = row[0],
                    lendingRate = Convert.ToDecimal(row[1]),
                    amountLent = Convert.ToDecimal(row[2]),
                    interestAmount = Convert.ToDecimal(row[4]),
                    feeAmount = Convert.ToDecimal(row[5]),
                    earnedAmount = Convert.ToDecimal(row[6]),
                    loanStart = Convert.ToDateTime(row[7]),
                    loanEnd = Convert.ToDateTime(row[8])
                });
            }

            var lendingSummary = new Dictionary<string, LendingSummaryEntity>();

            foreach(var loan in lending)
            {
                if(!lendingSummary.ContainsKey(loan.currencyName))
                {
                    lendingSummary.Add(loan.currencyName, new LendingSummaryEntity {totalAmount = 0, totalCount = 0, totalEarned = 0, totalFee = 0, totalInterest = 0, totalRate = 0 });
                }

                var tempSummary = lendingSummary[loan.currencyName];
                tempSummary.totalAmount += loan.amountLent;
                tempSummary.totalRate += loan.lendingRate;
                tempSummary.totalInterest += loan.lendingRate;
                tempSummary.totalFee += loan.feeAmount;
                tempSummary.totalEarned += loan.earnedAmount;
                tempSummary.totalCount++;
            }

            foreach (var summary in lendingSummary)
            {
                Console.Write(String.Format("What is the dollar value of {0}?: ", summary.Key));
                summary.Value.dollarValue = Convert.ToDecimal(Console.ReadLine());
            }

                foreach (var summary in lendingSummary)
            {
                var count = summary.Value.totalCount;
                var avgAmount = Math.Round(summary.Value.totalAmount / count, 4);
                var avgRate = Math.Round((summary.Value.totalRate / count) * 100, 4);
                var totalGain = Math.Round(summary.Value.totalEarned, 4);
                var totalLost = Math.Round(Math.Abs(summary.Value.totalFee), 4);
                var totalDollarValueEarned = Math.Round(totalGain * summary.Value.dollarValue, 2);
                var totalDollarValueLost = Math.Round(totalLost * summary.Value.dollarValue, 2);
                string result = String.Format("You loaned an avg of {0} {1} at an avg rate of {2}% for {3} {1} (${5}) profit and you paid {4}{1} (${6}) in fees", avgAmount, summary.Key, avgRate, totalGain, totalLost, totalDollarValueEarned, totalDollarValueLost);
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
