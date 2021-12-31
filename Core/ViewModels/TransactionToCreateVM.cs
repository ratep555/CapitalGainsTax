using System;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class TransactionToCreateVM
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string UserId { get; set; }
        
        public bool Purchase { get; set; }   

        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public int Resolved { get; set; }
        public string Email { get; set; }
        public DateTime BuyingDate { get; set; }

    }
}








