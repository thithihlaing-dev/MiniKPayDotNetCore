using Microsoft.EntityFrameworkCore;
using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.CustomerBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.Deposit
{
    public class DepositService
    {
        private readonly AppDbContext _db = new AppDbContext();
        private static readonly CustomerBalanceService _customerBalanceService = new CustomerBalanceService();



        public TblDeposit RegisterDeposit(String mobileNo, Decimal amount)
        {
            var deposit = new TblDeposit();
            deposit.DepositMobileNo = mobileNo;
            deposit.DepositAmount = amount;

            _db.TblDeposits.Add(deposit);
            _db.SaveChanges();
            return deposit;
        }

        public object CreateDeposit(String mobileNo, Decimal amount)
        {

            var customer = _db.TblCustomers.AsNoTracking().FirstOrDefault(x => x.CustomerMobileNo == mobileNo);
            if (customer is null) {
                var message = new ResponseMessage
                {
                    responseMessage = "Invalid Mobile Number."
                };
                return message;
            }
            if(amount > 0)
            {
                var customerBalance = _customerBalanceService.DepositCustomerBalance( customer.CustomerId, amount);
                if (customerBalance is null) {
                    var message = new ResponseMessage
                    {
                        responseMessage = "Balance Not Found"
                    };
                    return message;
                }

                var deposit = new TblDeposit
                {
                    DepositMobileNo = mobileNo,
                    DepositAmount = amount
                };
                _db.TblDeposits.Add(deposit);
                _db.SaveChanges();
                return deposit;
            }

            var error = new ResponseMessage
            {
                responseMessage = "Invalid Data"
            };
            return error;

        }
    }
}
