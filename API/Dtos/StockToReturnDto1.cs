namespace API.Dtos
{
    public class StockToReturnDto1
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public int? TotalQuantity { get; set; }
         public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }

    }
}