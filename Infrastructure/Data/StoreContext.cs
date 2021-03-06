
using System.Reflection;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : IdentityDbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Surtax> Surtaxes { get; set; }
        public DbSet<StockTransaction> StockTransactions {get; set;}
        public DbSet<TaxLiability> TaxLiabilities {get; set;}
        public DbSet<AnnualProfitOrLoss> AnnualProfitsOrLosses {get; set;}
 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}





