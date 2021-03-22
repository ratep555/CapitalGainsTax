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
    }
}