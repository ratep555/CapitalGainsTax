using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                 builder.HasOne(b => b.Surtax).WithMany()
                 .HasForeignKey(b => b.SurtaxId)
                 .IsRequired(false);
            }
    }
}