using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.CustomerBalance;

public class CustomerBalanceService
{
    private readonly AppDbContext _db = new AppDbContext();

    public TblCustomerBalance CreateCustomerBalance(TblCustomerBalance customerBalance)
    {
        _db.TblCustomerBalances
            .Add(customerBalance);
        _db.SaveChanges();
        return customerBalance;
    }

    public TblCustomerBalance DepositCustomerBalance(int customerId , Decimal amount)
    {
        var customerBalance = _db.TblCustomerBalances.AsNoTracking().FirstOrDefault(x => x.CustomerId == 
        customerId);

        if (customerBalance is null) {
            return null;
        }

        customerBalance.Balance += amount;
        _db.Entry(customerBalance).State = EntityState.Modified;
        _db.SaveChanges();

        return customerBalance;
    }

    public TblCustomerBalance WithdrawCustomerBalance(int customerId, Decimal amount)
    {
        var customerBalance = _db.TblCustomerBalances.AsNoTracking().FirstOrDefault(x => x.CustomerId == customerId);      
        if (customerBalance.Balance < amount)
        {
            return null;
        }
        customerBalance.Balance -= amount;
        if (customerBalance.Balance < 10000)
        {
            return null;
        }
        _db.Entry(customerBalance).State = EntityState.Modified;
        _db.SaveChanges();
        return customerBalance;
    }

    public TblCustomerBalance GetCustomerBalance(int customerId)
    {
        var customerBalance = _db.TblCustomerBalances.AsNoTracking().FirstOrDefault(x => x.CustomerId == customerId);
        if (customerBalance is null) 
        {
            return null; 
        }
        return customerBalance;
    }

    public TblCustomerBalance FromMobileTransferCustomerBalance(int customerId, Decimal balance)
    {
        var customerBalance = _db.TblCustomerBalances.AsNoTracking().FirstOrDefault(x => x.CustomerId == customerId);
        if (customerBalance is null)
        {
            return null;

        }
        customerBalance.Balance = balance;
        _db.Entry(customerBalance).State = EntityState.Modified;
        _db.SaveChanges();

        return customerBalance;
    }

    public TblCustomerBalance ToMobileTransferCustomerBalance(int customerId, Decimal balance)
    {
        var customerBalance = _db.TblCustomerBalances.FirstOrDefault(x => x.CustomerId == customerId);
        if (customerBalance is null)
        {
            return null;
        }

        customerBalance.Balance = balance;
        _db.Entry(customerBalance).State = EntityState.Modified;
        _db.SaveChanges();

        return customerBalance;
    }
}
