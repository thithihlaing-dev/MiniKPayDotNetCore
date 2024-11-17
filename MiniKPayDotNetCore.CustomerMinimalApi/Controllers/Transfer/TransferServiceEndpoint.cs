using MiniKPayDotNetCore.Domain.Features.Transfer;
using MiniKPayDotNetCore.Domain.Features.Withdraw;

namespace MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Transfer
{
    public static class TransferServiceEndpoint
    {
        private static readonly TransferService _transferService = new TransferService();
        public static void UseTransferServiceEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/transfers/{fromMobileNo}/{toMobileNo}/{amount}/{message}/{pin}", 
                (String fromMobileNo,String toMobileNo,Decimal amount,String message,String pin) =>
            {
                var transferModel = _transferService.CreateTransfer(fromMobileNo, toMobileNo, amount , message , pin);
                return Results.Ok(transferModel);
            })
                   .WithName("CreateTransaction")
                   .WithOpenApi();
        }
    }
}