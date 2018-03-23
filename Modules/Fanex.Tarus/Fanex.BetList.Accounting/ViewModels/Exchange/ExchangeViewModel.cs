using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.BetList.Accounting.Models.Exchange;

namespace Fanex.BetList.Accounting.ViewModels.Exchange
{
    public class ExchangeViewModel
    {
        public ExchangeViewModel(
            IEnumerable<ExchangeSummaryTicket> summaries,
            IEnumerable<ExchangeDetailTicket> details,
            decimal grandTotal,
            int totalSummaryRows)
        {
            Summaries = summaries.OrderByDescending(i => i.SettledDate);
            Details = details;            
            TotalSummaryRows = totalSummaryRows;
            TotalCommission = grandTotal;
        }

        public IEnumerable<ExchangeSummaryTicket> Summaries { get; private set; }

        public IEnumerable<ExchangeDetailTicket> Details { get; private set; }

        public decimal TotalCommission { get; private set; }

        public int TotalSummaryRows { get; }

        public int Index { get; set; }
    }
}
