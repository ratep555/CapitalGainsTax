namespace Core.ViewModels
{
   public class ClientPortfolioViewModel
    {
         public int Id { get; set; }
        public int StockId { get; set; }
        public string UserId { get; set; }
        public int TransactionId { get; set; }
        public string Symbol { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPriceOfPurchase { get; set; }
        public decimal TotalValueOfCertainStock { get; set; }
        public decimal AveragePriceOfPurchase { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Email { get; set; }
        public decimal? PortfolioPercentage { get; set; }

    }
}