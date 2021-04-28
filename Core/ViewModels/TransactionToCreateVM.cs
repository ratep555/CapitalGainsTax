using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class TransactionToCreateVM
    {
        public int StockId { get; set; }
        
        public bool Purchase { get; set; }   

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public int Resolved { get; set; }
    }
}