using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            //this for id is not necessary, just for demonstration
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.CompanyName).IsRequired().HasMaxLength(70);
            builder.Property(p => p.Symbol).IsRequired().HasMaxLength(10);
            builder.Property(p => p.CurrentPrice).HasColumnType("decimal(18,2");
            //ef already did this but we can do it here
            builder.HasOne(b => b.Category).WithMany()
            .HasForeignKey(p => p.CategoryId);
            builder.HasOne(b => b.Country).WithMany()
            .HasForeignKey(p => p.CountryId);
        }
    }
}