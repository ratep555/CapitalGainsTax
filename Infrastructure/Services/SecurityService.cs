using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.ViewModels;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly StoreContext _context;
        public SecurityService(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetMeStocks(string symbol, decimal price)
        {
            
            var list = await _context.Stocks.Where(s => s.Symbol == symbol)
            .Include(p => p.Category)
            .Include(p => p.Country)
            .ToListAsync();

            foreach (var item in list)
            {
                item.Symbol = symbol;
                item.CurrentPrice = price;

                await _context.SaveChangesAsync();
            }

            return list;
        } 
        public async Task GetMeStocks1(string symbol, decimal price)
        {
            
            var list = await _context.Stocks.Where(s => s.Symbol == symbol)
            .Include(p => p.Category)
            .Include(p => p.Country)
            .ToListAsync();

            foreach (var item in list)
            {
                item.Symbol = symbol;
                item.CurrentPrice = price;

                await _context.SaveChangesAsync();
            }
        
        } 

    }
}







