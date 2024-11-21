using MiniKPayDotNetCore.Domain.Features.Transfer;
using MiniKPayDotNetCore.MiniKPay.Database.Models;

namespace MiniKPayDotNetCore.MiniKPay.Api.Controllers.Transfer
{
    public static class TransferServiceEndpoint 
    {
        private static readonly TransferService _transferService = new TransferService();

     
        public static void UseTransferServiceEndpoint(this IEndpointRouteBuilder app)
        {
             app.MapPost("/transfers/{fromMobileNo}/{toMobileNo}/{amount}/{message}/{pin}",
               static async (string fromMobileNo, string toMobileNo, decimal amount, string message, string pin) =>
           {
               var transferModel = await _transferService.CreateTransfer(fromMobileNo, toMobileNo, amount, message, pin);
               if (transferModel.Response.IsSuccess)
               {
                   return Results.Ok(transferModel);
               }
               if (transferModel.Response.RespType == EnumRespType.None)
               {
                   return Results.BadRequest(transferModel);
               }
               if (transferModel.Response.RespType == EnumRespType.SystemError)
               {
                   return Results.StatusCode(500);
               }
               return Results.Ok(transferModel);
           })
                  .WithName("CreateTransaction")
                  .WithOpenApi();
        }
    }
}