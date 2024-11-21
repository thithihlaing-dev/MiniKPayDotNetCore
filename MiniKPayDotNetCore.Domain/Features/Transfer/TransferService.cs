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
               


                if (fromSender is null)
                {
                    return ($"Your Mobile Number {fromMobile} is Wrong");
                }
                else if (fromSender.CustomerPin != pin)
                {
                    return ($"Your Pin Code {pin} is Wrong ");
                }
                else if (amount < 0)
                {
                    return ($"Invalid Amount {amount}");
                }
                else if (toReceiver is null)
                {
                    return ($"Transfer Mobile Number is Wrong {toMobile}");
                }



                if (fromSender is not null
                    && toReceiver is not null
                    && fromSender.CustomerPin == pin
                    && amount > 0
                    )
                {

                    var fromSenderBalance = _customerBalanceService.GetCustomerBalance(fromSender.CustomerId);
                    if (fromSenderBalance.Balance < amount || fromSenderBalance.Balance < 10000)
                    {
                        return ($"Cannot Transfer Your Balance {fromSenderBalance.Balance}  is Low");
                    }
                    fromSenderBalance.Balance -= amount;
                    if (fromSenderBalance.Balance < 10000)
                    {
                        return "Invalid Transfer Amount. Your balance will left At least 10000";
                    }
                    var send = _customerBalanceService.FromMobileTransferCustomerBalance(fromSenderBalance.CustomerId, fromSenderBalance.Balance);



                    
                    var toReciverBalance = _customerBalanceService.GetCustomerBalance(toReceiver.CustomerId);
                    toReciverBalance.Balance += amount;
                    var receive = _customerBalanceService.ToMobileTransferCustomerBalance(toReciverBalance.CustomerId, toReciverBalance.Balance);

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
                    return ($"Transaction is Successful. ToMobile Number {toMobile}. Amount{amount} ");
                }

            }

            return "Insert Different Mobile Number";


        }
    }
}
