using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABCMoneyTransfer.Model;

public partial class AbcremittanceDbContext : IdentityDbContext<User, Role, int>
{
    public AbcremittanceDbContext()
    {
    }

    public AbcremittanceDbContext(DbContextOptions<AbcremittanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<Receiver> Receivers { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sender> Senders { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D681N9Q;Database=ABCRemittanceDB;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
        {
            entity.HasNoKey();
        });
        // Map IdentityUserRole<int> to UserRoles table
        //modelBuilder.Entity<IdentityUserRole<int>>(entity =>
        //{
        //    entity.ToTable("UserRoles");
        //    entity.HasKey(ur => new { ur.UserId, ur.RoleId });
        //});
        modelBuilder.Entity<IdentityUserToken<int>>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exchange__3214EC076F7B76EF");

            entity.Property(e => e.BaseCurrency).HasMaxLength(10);
            entity.Property(e => e.ExchangeRate1)
                .HasColumnType("decimal(18, 6)")
                .HasColumnName("ExchangeRate");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TargetCurrency).HasMaxLength(10);
        });

        modelBuilder.Entity<Receiver>(entity =>
        {
            entity.HasKey(e => e.ReceiverId).HasName("PK__Receiver__FEBB5F07091E50AA");

            entity.ToTable("Receiver");

            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.Rcountry)
                .HasMaxLength(100)
                .HasDefaultValue("Nepal")
                .HasColumnName("RCountry");
            entity.Property(e => e.ReceiverAddress).HasMaxLength(255);
            entity.Property(e => e.RfirstName)
                .HasMaxLength(100)
                .HasColumnName("RFirstName");
            entity.Property(e => e.Ridentity)
                .HasMaxLength(50)
                .HasColumnName("RIdentity");
            entity.Property(e => e.RlastName)
                .HasMaxLength(100)
                .HasColumnName("RLastName");
            entity.Property(e => e.RmidName)
                .HasMaxLength(100)
                .HasColumnName("RMidName");
            entity.Property(e => e.Rmobile)
                .HasMaxLength(15)
                .HasColumnName("RMobile");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEEEA0D524C0E");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.RevokedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__48CFD27E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A3AEC31CD");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160279495BF").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Sender>(entity =>
        {
            entity.HasKey(e => e.SenderId).HasName("PK__Sender__BB4991ABD37903C0");

            entity.ToTable("Sender");

            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Scountry)
                .HasMaxLength(100)
                .HasDefaultValue("Malaysia")
                .HasColumnName("SCountry");
            entity.Property(e => e.SenderAddress).HasMaxLength(255);
            entity.Property(e => e.SfirstName)
                .HasMaxLength(100)
                .HasColumnName("SFirstName");
            entity.Property(e => e.Sidentity)
                .HasMaxLength(50)
                .HasColumnName("SIdentity");
            entity.Property(e => e.SlastName)
                .HasMaxLength(100)
                .HasColumnName("SLastName");
            entity.Property(e => e.SmidName)
                .HasMaxLength(100)
                .HasColumnName("SMidName");
            entity.Property(e => e.Smobile)
                .HasMaxLength(15)
                .HasColumnName("SMobile");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07E779C16C");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.PayoutAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rcurrency)
                .HasMaxLength(10)
                .HasColumnName("RCurrency");
            entity.Property(e => e.ReceiverAddress).HasMaxLength(255);
            entity.Property(e => e.ReceiverCountry).HasMaxLength(50);
            entity.Property(e => e.ReceiverName).HasMaxLength(100);
            entity.Property(e => e.Ridentity)
                .HasMaxLength(50)
                .HasColumnName("RIdentity");
            entity.Property(e => e.Rmobile)
                .HasMaxLength(15)
                .HasColumnName("RMobile");
            entity.Property(e => e.Scurrency)
                .HasMaxLength(10)
                .HasColumnName("SCurrency");
            entity.Property(e => e.SendDate).HasColumnType("datetime");
            entity.Property(e => e.SenderAddress).HasMaxLength(255);
            entity.Property(e => e.SenderCountry).HasMaxLength(50);
            entity.Property(e => e.SenderName).HasMaxLength(100);
            entity.Property(e => e.ServiceCharge).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sidentity)
                .HasMaxLength(50)
                .HasColumnName("SIdentity");
            entity.Property(e => e.Smobile)
                .HasMaxLength(15)
                .HasColumnName("SMobile");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransferAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C71F65976");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E448A52AA1").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105346B656776").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SecurityStamp).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            // Skipping columns that do not exist in the database
            // Uncomment and set the necessary mappings if required
            entity.Property(e => e.NormalizedUserName).HasColumnName("NormalizedUserName");
            entity.Property(e => e.ConcurrencyStamp).HasColumnName("ConcurrencyStamp");
            entity.Property(e => e.AccessFailedCount).HasColumnName("AccessFailedCount");
            entity.Property(e => e.EmailConfirmed).HasColumnName("EmailConfirmed");
            entity.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");
            entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            entity.Property(e => e.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
        });

        //modelBuilder.Entity<UserRole>(entity =>
        //{
        //    entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A35D8662C9D");

        //    entity.HasIndex(e => new { e.UserId, e.RoleId }, "UK_UserRole").IsUnique();

        //    entity.Property(e => e.AssignedAt)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");

        //    entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
        //        .HasForeignKey(d => d.RoleId)
        //        .HasConstraintName("FK__UserRoles__RoleI__440B1D61");

        //    entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
        //        .HasForeignKey(d => d.UserId)
        //        .HasConstraintName("FK__UserRoles__UserI__4316F928");
        //});



        // Exclude IdentityUserRole<int> from the model
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("IgnoredIdentityUserRole").HasNoKey();

        // Map your custom UserRole entity
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A35D8662C9D");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "UK_UserRole").IsUnique();

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__4316F928");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
