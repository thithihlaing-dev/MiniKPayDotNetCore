using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.CustomerBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.Withdraw
{
    public class WithdrawService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private static readonly CustomerBalanceService _customerBalanceService = new CustomerBalanceService();

        public String CreateWithdraw(String mobileNo , Decimal amount)
        {
            var customer = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerMobileNo == mobileNo);
            var customerBalance = _customerBalanceService.WithdrawCustomerBalance(customer.CustomerId, amount);
             
            if (customer is null)
            {               
                return ($"Invalid Mobile Number.{mobileNo}");
            }
            else if (amount < 0)
            {
                return ($"Invalid Withdraw Amount {amount}");               
            }
            else if (customerBalance is  null  )
            {
                return "Invalid Withdraw Amount. Your balance will left At least 10000";
            }
            var withdraw = new TblWithdraw
            {
                WithdrawMobileNo = mobileNo,
                WithdrawAmount = amount,
                DateTime = DateTime.Now
            };
            _db.TblWithdraws.Add(withdraw);
            _db.SaveChanges();

            return ($"Successful Withdraw. Mobile Number{mobileNo} Amount{amount}");

        }
    }
}
