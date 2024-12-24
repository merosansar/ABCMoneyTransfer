using ABCMoneyTransfer.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABCMoneyTransfer.Data;

public class ABCMoneyTransferContext : IdentityDbContext<ABCMoneyTransferUser>
{
    public ABCMoneyTransferContext(DbContextOptions<ABCMoneyTransferContext> options)
        : base(options)
    {
    }
    public DbSet<ABCMoneyTransferUser> ABCMoneyTransferUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ABCMoneyTransferUser>
{
    public void Configure(EntityTypeBuilder<ABCMoneyTransferUser> builder)
    {
        builder.Property(x=>x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        throw new NotImplementedException();
    }
}
