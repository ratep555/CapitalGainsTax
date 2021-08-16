namespace Core.Entities
{
    public class Stock : BaseEntity
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }
    }
    public class Stock1
    {
        public string Symbol { get; set; }
        public int? NumberOfEmployees { get; set; }
        public int? SharesOutstanding { get; set; }
        public int? OwnShares { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Expenditure { get; set; }
        public decimal? EnterpriseValue { get; set; }
        public decimal? Dividend { get; set; }
    }
}






















