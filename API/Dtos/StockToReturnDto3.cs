namespace API.Dtos
{
    public class StockToReturnDto3
    {
        public int Id { get; set; }
        public string Symbol { get; set;}
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
    }
}