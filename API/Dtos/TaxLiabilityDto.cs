namespace API.Dtos
{
    public class TaxLiabilityDto
    {
        public decimal? GrossProfit { get; set; }
        public decimal? CapitalGainsTax { get; set; }
        public decimal? Surtax { get; set; }
        public decimal? NetProfit { get; set; }
    }
}