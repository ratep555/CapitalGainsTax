using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TaxLiability : BaseEntity
    {
        [ForeignKey("Surtax")]
        public int? SurtaxId { get; set; }       
        public Surtax Surtax { get; set; }   

        public int? Year { get; set; } = DateTime.Now.Year;
        public string Email { get; set; }
        public decimal? GrossProfit { get; set; }
        public decimal? CapitalGainsTax { get; set; }
        public decimal? SurtaxAmount { get; set; }
        public decimal? TotalTaxLiaility { get; set; }
        public decimal? NetProfit { get; set; }
        public bool Locked { get; set; }
    }
}