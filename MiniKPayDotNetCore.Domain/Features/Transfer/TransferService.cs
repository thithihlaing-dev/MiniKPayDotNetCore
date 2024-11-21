using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.Customer;
using MiniKPayDotNetCore.Domain.Features.CustomerBalance;
using MiniKPayDotNetCore.MiniKPay.Database.Models;
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

        public async Task<TransferResponseModel>CreateTransfer(String fromMobile,
                                                         String toMobile,
                                                         Decimal amount,
                                                         String message,
                                                         String pin)
        {
            TransferResponseModel model = new TransferResponseModel();
            if (fromMobile != toMobile )
            {
                var fromSender = _customerService.GetCustomer(fromMobile);
                var toReceiver = _customerService.GetCustomer(toMobile);
               


                if (fromSender is null)
                {
                    model.Response = BaseResponseModel.ValidationError("999", $"Your Mobile Number {fromMobile} is Wrong");                  
                    goto Result;
                }
                else if (fromSender.CustomerPin != pin)
                {
                    model.Response = BaseResponseModel.ValidationError("999", $"Your Pin Code {pin} is Wrong ");
                    goto Result;
                }
                else if (amount < 0)
                {
                    model.Response = BaseResponseModel.ValidationError("999", $"Invalid Amount {amount}");
                    goto Result;
                }
                else if (toReceiver is null)
                {
                    model.Response = BaseResponseModel.ValidationError("999", $"Transfer Mobile Number is Wrong {toMobile}");
                    goto Result;
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
                        model.Response = BaseResponseModel.ValidationError("999", $"Cannot Transfer Your Balance {fromSenderBalance.Balance}  is Low");
                        goto Result;
                    }
                    fromSenderBalance.Balance -= amount;
                    if (fromSenderBalance.Balance < 10000)
                    {
                        model.Response = BaseResponseModel.ValidationError("999", "Invalid Transfer Amount. Your balance will left At least 10000");
                        goto Result;

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
                    model.Response = BaseResponseModel.Success("000", $"Transaction is Successful. ToMobile Number {toMobile}. Amount{amount} ");
                    goto Result;

                }

            }

            model.Response = BaseResponseModel.ValidationError("999", "Insert Different Mobile Number");
        Result:
            return model;


        }
    }
}
