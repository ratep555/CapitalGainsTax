using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class TransactionToReturnDto
    {
         public int TransactionId { get; set; }
        public int StockId { get; set; }
        public string Stock { get; set; }      
        public string UserId { get; set; }
        public string Email { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public bool Purchase { get; set; }
        public bool Lock { get; set; }
        public int Resolved { get; set; }
    }
}