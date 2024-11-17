using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.CustomerBalance;
using MiniKPayDotNetCore.Domain.Features.Deposit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.Customer
{
    public class CustomerService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private static readonly CustomerBalanceService _customerBalanceService = new CustomerBalanceService();
        private static readonly DepositService _depositService = new DepositService();


        public List<TblCustomer> GetCustomers()
        {
            var model = _db.TblCustomers.AsNoTracking().ToList();
            return model;
        }

        public TblCustomer GetCustomer(int id) 
        {
            var item = _db.TblCustomers.FirstOrDefault(x => x.CustomerId == id);
            if (item is null) {
                return null;
            }
            return item;
        }

        public TblCustomer GetCustomer(string mobile)
        {
            var item = _db.TblCustomers.FirstOrDefault(x => x.CustomerMobileNo == mobile);
            if (item is null)
            {
                return null;
            }
            return item;
        }

        public TblCustomer CreateCustomer( TblCustomer customer , Decimal amount)
        {
            _db.TblCustomers.Add(customer);
            _db.SaveChanges();

            var customerBalance = new TblCustomerBalance();
            customerBalance.CustomerId = customer.CustomerId;
            customerBalance.BalancePin = customer.CustomerPin;
            if(amount < 10000)
            {
                return null;
            }
            customerBalance.Balance = amount;
            _customerBalanceService.CreateCustomerBalance(customerBalance); 
            
            _depositService.CreateDeposit(customer.CustomerMobileNo,amount);

            return customer;
        }

        public TblCustomer UpdateCustomer(int id, TblCustomer customer) 
        { 
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);
            if (item is null) {
                return null;
            }

            item.CustomerFullName = customer.CustomerFullName;
            item.CustomerMobileNo = customer.CustomerMobileNo;
            item.CustomerPin = customer.CustomerPin;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
           
        }

        public TblCustomer PatchCustomer(int id, TblCustomer customer)
        {
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(customer.CustomerFullName)) { 
                item.CustomerFullName = customer.CustomerFullName;
            }
            if (!string.IsNullOrEmpty(customer.CustomerMobileNo))
            {
                item.CustomerMobileNo = customer.CustomerMobileNo;
            }
            if (!string.IsNullOrEmpty(customer.CustomerPin))
            {
                item.CustomerPin = customer.CustomerPin;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public bool DeleteCustomer(int id)
        {
            var item = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);
            if (item is null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return true;

        }
    }   
}
