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
    }
}