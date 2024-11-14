using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiniKPayDotNetCore.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerBalance> CustomerBalances { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Withdraw> Withdraws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MiniKPay;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Customer");

            entity.Property(e => e.CustomerFullName)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerPin).HasMaxLength(255);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<CustomerBalance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CustomerBalance");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
            entity.Property(e => e.BalancePin)
                .HasMaxLength(255)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.CustomerId).HasMaxLength(255);
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Deposit");

            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DepositId).ValueGeneratedOnAdd();
            entity.Property(e => e.DepositMobileNo)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Transaction");

            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromMobile)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.ToMobile)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionId).ValueGeneratedOnAdd();
            entity.Property(e => e.TransactionMessage).HasColumnType("text");
            entity.Property(e => e.TransactionPin).HasMaxLength(255);
        });

        modelBuilder.Entity<Withdraw>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Withdraw");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.WithdrawAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.WithdrawId).ValueGeneratedOnAdd();
            entity.Property(e => e.WithdrawMobileNo).HasMaxLength(20);
            entity.Property(e => e.WithdrawPin)
                .HasMaxLength(255)
                .HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
