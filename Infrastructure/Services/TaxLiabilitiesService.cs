using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TaxLiabilitiesService : ITaxLiabilitiesService
    {
        private readonly StoreContext _context;
        private readonly ITransactionService _transactionService;
        public TaxLiabilitiesService(StoreContext context, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _context = context;
        }

        public async Task<TaxLiability> FindTaxLiabilityById(int taxLiabilityId)
        {
            return await _context.TaxLiabilities
            .Include(x => x.Surtax)
            .FirstOrDefaultAsync(x => x.Id == taxLiabilityId);
        }
        public async Task UpdateTaxLiability(TaxLiability taxLiability)
        {
            _context.Entry(taxLiability).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<decimal> TotalNetProfit(string email)
        {
            decimal basket = 0;

            foreach (var item in _context.Stocks.ToList())
            {
                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false)
                .Sum(t => t.Price * t.Quantity)) -
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0)
                .Sum(t => t.Resolved * t.Price));

                basket += totalNetProfit;
            }

            return await Task.FromResult(basket);
        }

        public async Task UpdateTaxLiability1(string email,
        TaxLiabilityToEditDTO taxLiabilityToEditDto)
        {
            var user = await _context.AppUsers.Where(ap => ap.Email == email)
                       .FirstOrDefaultAsync();

            var taxLiability = await _context.TaxLiabilities
            .Include(t => t.Surtax)
            .Where(t => t.Email == email)
            .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(s => s.Id == user.SurtaxId)
                         .FirstOrDefaultAsync();

            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = surtax.Id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else
            {
                taxLiability.NetProfit = 0;
            }

            _context.Entry(taxLiability).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaxLiability2(string email,
        int id)
        {
            var taxLiability = await _context.TaxLiabilities
            .Where(t => t.Email == email && t.Locked == false)
            .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(st => st.Id == id)
                         .FirstOrDefaultAsync();

            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else
            {
                taxLiability.NetProfit = 0;
            }

            _context.Entry(taxLiability).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsersSurtaxId(string email, int surtaxId)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email)
                       .FirstOrDefaultAsync();

            user.SurtaxId = surtaxId;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<TaxLiability> FindTaxLiability(string email)
        {
            var taxLiability = await _context.TaxLiabilities
            .Include(x => x.Surtax)
            .Where(t => t.Email == email)
            .FirstOrDefaultAsync();

            return taxLiability;
        }

        public async Task<TaxLiability> UpdateNewTaxLiability(string email, int id)
        {
            var taxLiability = await _context.TaxLiabilities.Where(t => t.Email == email)
                               .Include(t => t.Surtax)
                               .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(s => s.Id == id).FirstOrDefaultAsync();


            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else
            {
                taxLiability.NetProfit = 0;
            }

            _context.Entry(taxLiability).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await Task.FromResult(taxLiability);
        }

        public async Task<TaxLiability> FindTaxLiabilityByEmail(string email)
        {
            var taxliability = await _context.TaxLiabilities.Include(t => t.Surtax)
                               .Where(t => t.Email == email && t.Locked == false)
                               .FirstOrDefaultAsync();

            return await Task.FromResult(taxliability);
        }
        public async Task<FinalTaxLiabilityVM> FindAnnualByEmail(string email, int id)
        {
            var annual = await _context.AnnualProfitsOrLosses
                               .Where(t => t.Email == email && t.Locked == false)
                               .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(st => st.Id == id)
                         .FirstOrDefaultAsync();

            var final = new FinalTaxLiabilityVM();

            decimal f = 100;
            decimal? basket4 = await _transactionService.TotalNetProfitForCurrentYear2(email);
            decimal? basket5 = (12 / f) * basket4;

            final.Email = email;
            final.Amount = annual.Amount;
            final.Year = annual.Year;
            final.Residence = surtax.Residence;

            if (basket4.HasValue && basket4 > 0)
            {
                final.CapitalGainsTax = basket5;
            }
             else
            {
                final.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                final.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                final.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                final.TotalTaxLiaility = basket5 + final.SurtaxAmount;
            }
            else
            {
                final.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                final.NetProfit = basket4 - final.TotalTaxLiaility;
            }
            else
            {
                final.NetProfit = 0;
            }

            return final;
        }
        public decimal Broj()
        {
            List<StockTransaction> list = new List<StockTransaction>
            {
                new StockTransaction {Id = 1, Purchase = true, Quantity = 10, Resolved = 10, Price = 50, Date = DateTime.Parse( "15.10.1987")},
                new StockTransaction {Id = 2, Purchase = true, Quantity = 5, Resolved = 5, Price = 70, Date = DateTime.Parse( "16.10.1987")},
                new StockTransaction {Id = 3, Purchase = true, Quantity = 8, Resolved = 8, Price = 25, Date = DateTime.Parse( "17.10.1987")},
                new StockTransaction {Id = 4, Purchase = true, Quantity = 7, Resolved = 7, Price = 77, Date = DateTime.Parse( "18.10.1987")},
                new StockTransaction {Id = 5, Purchase = true, Quantity = 1, Resolved = 1, Price = 23, Date = DateTime.Parse( "19.10.1987")},
                new StockTransaction {Id = 6, Purchase = true, Quantity = 3, Resolved = 0, Price = 14, Date = DateTime.Parse( "20.10.1987")},
                new StockTransaction {Id = 7, Purchase = true, Quantity = 2, Resolved = 0, Price = 17, Date = DateTime.Parse( "21.10.1987")},           
            };

            decimal last3 = list.OrderByDescending(x=>x.Date).Take(3).Select(x=>x.Quantity * x.Price).Sum();


            var requiredSum = list.Where(x=>x.Id==4).Select(x=>x.Price).First();
            


            var broji = requiredSum + last3;

            SumOfLastTransaction(list, 7);

            var pliz = SumOfLastTransaction1(list, 11);

            return pliz;
        }
    private static decimal SumOfLastTransaction(IEnumerable<StockTransaction> stockTransactions, int max)
    {
        decimal result = 0;
        int sum = 0;
        foreach(var stockTransaction in stockTransactions.OrderByDescending(x => x.Id))
        {
            
            if(sum + stockTransaction.Quantity <= max)
            {
                result += stockTransaction.Quantity * stockTransaction.Price;
                sum += stockTransaction.Quantity;
            }
            else
            {
                result += (max - sum) * stockTransaction.Price;
                return result;
            }
        }
        return result;
    }
    private decimal SumOfLastTransaction1(IEnumerable<StockTransaction> stockTransactions, int max)
    {
        decimal result = 0;
        int sum = 0;
        foreach(var stockTransaction in stockTransactions.OrderByDescending(x => x.Id))
        {
            
            if(sum + stockTransaction.Quantity <= max)
            {
                result += stockTransaction.Quantity * stockTransaction.Price;
                sum += stockTransaction.Quantity;
            }
            else
            {
                result += (max - sum) * stockTransaction.Price;
                return result;
            }
        }
        return result;
    }
    
    public async Task<AnnualProfitOrLoss> GiveMeAnnual(string email)
    {
        var annual = await _context.AnnualProfitsOrLosses
                               .Where(t => t.Email == email && t.Locked == false)
                               .FirstOrDefaultAsync();

       // var stream = new MemoryStream();

        return annual;
        
    }
     public decimal Broj1()
        {
            List<StockTransaction> list = new List<StockTransaction>
            {
                new StockTransaction {Id = 1, Purchase = true, Quantity = 10, Resolved = 9, Price = 1700, Date = DateTime.Now.AddDays(1)},
                new StockTransaction {Id = 2, Purchase = true, Quantity = 5, Resolved = 0, Price = 1000, Date = DateTime.Now.AddDays(2)},
                new StockTransaction {Id = 3, Purchase = false, Quantity = 8, Resolved = 0, Price = 2000, Date = DateTime.Now.AddDays(3)},
                new StockTransaction {Id = 4, Purchase = true, Quantity = 1, Resolved = 0, Price = 1000, Date = DateTime.Now.AddDays(4)},
                new StockTransaction {Id = 5, Purchase = false, Quantity = 1, Resolved = 0, Price = 2000, Date = DateTime.Now.AddYears(3)},
            }; 

            decimal last3 = list.OrderByDescending(x=>x.Date).Take(1).Select(x=>x.Quantity * x.Price).Sum();
            int last5 = list.OrderByDescending(x=>x.Date).Take(1).Select(x=>x.Quantity).Sum();

            var list1 = list.Where(x => x.Purchase == true && x.Resolved > 0);


            var requiredSum = list.Where(x=>x.Id==4).Select(x=>x.Price).First();

            decimal last4 = SumOfLastTransaction1(list1, last5);

            decimal rezultat = (list.Where(x => x.Purchase == false).Sum(x => x.Price * x.Quantity) - last3) -
                               
                               (list.Where(x => x.Purchase == true && x.Resolved > 0).Sum(x => x.Price * x.Resolved) - last4);
            
            int sellingquantity = list.Where(x => x.Purchase == false).Sum(x => x.Quantity);

            var broji = requiredSum + last3;

            SumOfLastTransaction(list, 1);

            var pliz = SumOfLastTransaction1(list, 3);

            return rezultat;
        }
  

    }
}











