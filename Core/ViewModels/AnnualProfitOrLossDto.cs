namespace Core.ViewModels
{
    public class AnnualProfitOrLossDto
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public decimal? Amount { get; set; }
        public string Email { get; set; }

    }
}