﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ABCMoneyTransfer.Model;

public partial class AbcremittanceDbContext : DbContext
{
    public AbcremittanceDbContext()
    {
    }

    public AbcremittanceDbContext(DbContextOptions<AbcremittanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D681N9Q;Database=ABCRemittanceDB;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07E779C16C");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.PayoutAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiverAddress).HasMaxLength(255);
            entity.Property(e => e.ReceiverCountry).HasMaxLength(50);
            entity.Property(e => e.ReceiverName).HasMaxLength(100);
            entity.Property(e => e.SenderAddress).HasMaxLength(255);
            entity.Property(e => e.SenderCountry).HasMaxLength(50);
            entity.Property(e => e.SenderName).HasMaxLength(100);
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
        });

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
