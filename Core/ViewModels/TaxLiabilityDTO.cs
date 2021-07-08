using System;

namespace Core.ViewModels
{
    public class TaxLiabilityDTO
    {
         public int Id { get; set; }       
        public string Residence { get; set; }   
        public decimal Percentage { get; set; }
        public int? Year { get; set; } = DateTime.Now.Year;
        public string Email { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? CapitalGainsTax { get; set; }
        public decimal? SurtaxAmount { get; set; }
        public decimal? TotalTaxLiaility { get; set; }
        public decimal? NetProfit { get; set; }
    }
}