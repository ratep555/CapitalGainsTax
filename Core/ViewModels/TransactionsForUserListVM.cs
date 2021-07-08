using System.Collections;
using System.Collections.Generic;

namespace Core.ViewModels
{
    public class TransactionsForUserListVM
    {
        public IEnumerable<TransactionsForUserVM> ListOfTransactions { get; set; }
        public decimal? TotalNetProfit { get; set; }
        public decimal? TotalNetProfit1 { get; set; }
        public decimal? TotalTraffic { get; set; }
        public decimal? TotalTraffic1 { get; set; }
    }
}