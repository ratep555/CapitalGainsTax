namespace Core.ViewModels
{
    public class TaxLiabilityVM
    {
        public decimal? GrossProfit { get; set; }
        public decimal? CapitalGainsTax { get; set; }
        public decimal? Surtax { get; set; }
        public decimal? TotalTaxLiaility { get; set; }
        public decimal? NetProfit { get; set; }
        
    }
}