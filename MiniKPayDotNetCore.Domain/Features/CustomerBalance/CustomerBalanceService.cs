using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.CustomerBalance
{
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

        public object WithdrawCustomerBalance(int customerId, Decimal amount)
        {
            var customerBalance = _db.TblCustomerBalances.AsNoTracking().FirstOrDefault(x => x.CustomerId ==
            customerId);

           
            if (customerBalance.Balance < amount)
            {
                var message = new ResponseMessage
                {
                    responseMessage = "Cannot Withdraw Your Balance is Low."
                };
                return message;

            }
            customerBalance.Balance -= amount;
            if (customerBalance.Balance < 10000)
            {
                var message = new ResponseMessage
                {
                    responseMessage = "Cannot Withdraw Your Balance will be left at least 10000."
                };
                return message;

            }
            _db.Entry(customerBalance).State = EntityState.Modified;
            _db.SaveChanges();
            return customerBalance;
        }
    }
}
