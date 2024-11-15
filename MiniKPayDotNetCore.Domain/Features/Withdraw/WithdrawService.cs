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

        public object CreateWithdraw(string mobileNo , decimal amount)
        {
            var customer = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerMobileNo == mobileNo);
            if (customer is null)
            {
                var message = new ResponseMessage
                {
                    responseMessage = "Invalid Mobile Number."
                };
                return message;
            }
            if (amount > 0)
            {
                var customerBalance = _customerBalanceService.WithdrawCustomerBalance(customer.CustomerId, amount);
                if (customerBalance != null)
                {
                    var withdraw = new TblWithdraw
                    {
                        WithdrawMobileNo = mobileNo,
                        WithdrawAmount = amount,
                        DateTime = DateTime.Now
                    };
                    _db.TblWithdraws.Add(withdraw);
                    _db.SaveChanges();
                   
                    return customerBalance;
                }

               
            }

            var error = new ResponseMessage
            {
                responseMessage = "Invalid Data"
            };
            return error;
        }
    }
}
