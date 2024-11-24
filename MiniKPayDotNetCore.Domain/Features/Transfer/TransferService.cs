using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<ResultTransferResponseModel>> CreateTransfer(String fromMobile,
                                                         String toMobile,
                                                         Decimal amount,
                                                         String message,
                                                         String pin)
        {
            //TransferResponseModel model = new TransferResponseModel();
            Result<ResultTransferResponseModel> model = new Result<ResultTransferResponseModel>();

            if (fromMobile != toMobile )
            {
                //var fromSender = _customerService.GetCustomer(fromMobile);
                //var toReceiver = _customerService.GetCustomer(toMobile);

                var fromSender = await _db.TblCustomers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerMobileNo == fromMobile);
                var toReceiver = await _db.TblCustomers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerMobileNo == toMobile);


                if (fromSender is null)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError($"Your Mobile Number {fromMobile} is Wrong");                  
                    goto Result;
                }
                else if (fromSender.CustomerPin != pin)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError($"Your Pin Code {pin} is Wrong ");
                    goto Result;
                }
                else if (amount < 0)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError( $"Invalid Amount {amount}");
                    goto Result;
                }
                else if (toReceiver is null)
                {
                    model = Result<ResultTransferResponseModel>.ValidationError($"Transfer Mobile Number is Wrong {toMobile}");
                    goto Result;
                }



                if (fromSender is not null
                    && toReceiver is not null
                    && fromSender.CustomerPin == pin
                    && amount > 0
                    )
                {

                    var fromSenderBalance = await _db.TblCustomerBalances.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == fromSender.CustomerId);
                    if (fromSenderBalance.Balance < amount || fromSenderBalance.Balance < 10000)
                    {
                        model = Result<ResultTransferResponseModel>.ValidationError($"Cannot Transfer Your Balance {fromSenderBalance.Balance}  is Low");
                        goto Result;
                    }
                    fromSenderBalance.Balance -= amount;
                    if (fromSenderBalance.Balance < 10000)
                    {
                        model = Result<ResultTransferResponseModel>.ValidationError("Invalid Transfer Amount. Your balance will left At least 10000");
                        goto Result;

                    }
                    var send = _customerBalanceService.FromMobileTransferCustomerBalance(fromSenderBalance.CustomerId, fromSenderBalance.Balance);



                    var toReciverBalance = await _db.TblCustomerBalances.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == toReceiver.CustomerId);

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

                    ResultTransferResponseModel result = new ResultTransferResponseModel()
                    {
                        Transaction = transaction
                    };
                    model = Result<ResultTransferResponseModel>.Success(result,"Success.");
                    goto Result;
                    //return model;

                }

            }

            model = Result<ResultTransferResponseModel>.ValidationError("Insert Different Mobile Number");
        Result:
            return model;


        }
    }
}
