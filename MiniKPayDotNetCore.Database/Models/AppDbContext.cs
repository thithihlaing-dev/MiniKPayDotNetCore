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

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblCustomerBalance> TblCustomerBalances { get; set; }

    public virtual DbSet<TblDeposit> TblDeposits { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblWithdraw> TblWithdraws { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MiniKPay;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity
                .HasKey(e => e.CustomerId);
            entity.ToTable("TblCustomer");

            entity.Property(e => e.CustomerFullName)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            entity.Property(e => e.CustomerMobileNo)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.CustomerPin).HasMaxLength(255);
        });

        modelBuilder.Entity<TblCustomerBalance>(entity =>
        {
            entity
                .HasKey(e => e.BalanceId);
            entity.ToTable("TblCustomerBalance");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();
            entity.Property(e => e.BalancePin)
                .HasMaxLength(255)
                .HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<TblDeposit>(entity =>
        {
            entity
                .HasKey(e => e.DepositId);
            entity.ToTable("TblDeposit");

            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepositAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DepositId).ValueGeneratedOnAdd();
            entity.Property(e => e.DepositMobileNo)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity
                .HasKey(e => e.TransactionId);
            entity.ToTable("TblTransaction");

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

        modelBuilder.Entity<TblWithdraw>(entity =>
        {
            entity
                .HasKey(e => e.WithdrawId);
            entity.ToTable("TblWithdraw");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.WithdrawAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.WithdrawId).ValueGeneratedOnAdd();
            entity.Property(e => e.WithdrawMobileNo).HasMaxLength(20);
            entity.Property(e => e.DateTime)
                 .HasDefaultValueSql("(getdate())")
                 .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
