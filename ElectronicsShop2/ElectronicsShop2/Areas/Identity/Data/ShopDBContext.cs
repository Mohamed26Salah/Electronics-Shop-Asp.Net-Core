using ElectronicsShop2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicsShop2.Areas.Identity.Data;

public class ShopDBContext : IdentityDbContext<ElectronicsShopUser>
{
    public ShopDBContext(DbContextOptions<ShopDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        // To Configure to database
        builder.ApplyConfiguration(new ElectronicsShopUserEntityConfiguration());
    }

}

public class ElectronicsShopUserEntityConfiguration : IEntityTypeConfiguration<ElectronicsShopUser>
{
    public void Configure(EntityTypeBuilder<ElectronicsShopUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.Role).HasMaxLength(255);
    }
}
