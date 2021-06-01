namespace API.Dtos
{
    public class StockToReturnDto2
    {
        public int Id { get; set; }
        public string Symbol { get; set;}
        public decimal CurrentPrice { get; set; }
        public string CompanyName { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
    }
}