using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.Customer;
using MiniKPayDotNetCore.Domain.Features.CustomerBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.Domain.Features.Transfer
{
    public class TransferService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public static readonly CustomerService _customerService = new CustomerService();
        public static readonly CustomerBalanceService _customerBalanceService = new CustomerBalanceService();

        public String CreateTransfer(String fromMobile, String toMobile , Decimal amount, String message , String pin)
        {
            if (fromMobile != toMobile )
            {
                var fromSender = _customerService.GetCustomer(fromMobile);
                var toReceiver = _customerService.GetCustomer(toMobile);

                if (fromSender != null 
                    && toReceiver != null 
                    && fromSender.CustomerPin == pin
                    && amount > 0
                    ) {

                    var fromSenderBalance = _customerBalanceService.GetCustomerBalance(fromSender.CustomerId);
                    var toReciverBalance = _customerBalanceService.GetCustomerBalance(toReceiver.CustomerId);

                    if (fromSenderBalance.Balance < amount || fromSenderBalance.Balance < 10000 )
                    {
                        return "Cannot Transfer Your Balance is Low";
                    }
                    
                    

                    var send = _customerBalanceService.FromMobileCustomerBalance(fromSenderBalance.CustomerId,amount);
                    var receive = _customerBalanceService.ToMobileCustomerBalance(toReciverBalance.CustomerId, amount);

                    var transaction = new TblTransaction
                    {
                        ToMobile = toMobile,
                        FromMobile = fromMobile,
                        TransactionAmount = amount,
                        TransactionMessage = message,
                        TransactionPin = pin
                    };
                    _db.TblTransactions.Add(transaction);
                    _db.SaveChanges();
                    return "Transaction is Successful";
                }
                else if ( fromSender is null )
                {
                    return "Your Mobile Number is Wrong";
                }
                else if (toReceiver is null)
                {
                    return "Transfer Mobile Number is Wrong";
                }
                else if (amount < 0)
                {
                    return "Invalid Amount";
                }
                else if (fromSender.CustomerPin != pin)
                {
                    return "Your Pin Code is Wrong";
                }

            }

            return "Insert Different Mobile Number";


        }
    }
}
