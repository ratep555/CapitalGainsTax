using Core.Entities;

namespace Core.ViewModels
{
    public class FinalTaxLiabilityVM
    {
        public int Year { get; set; }
        public string Email { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CapitalGainsTax { get; set; }
        public decimal? SurtaxAmount { get; set; }
        public decimal? TotalTaxLiaility { get; set; }
        public decimal? NetProfit { get; set; }
        public string Residence { get; set; }

    }
}