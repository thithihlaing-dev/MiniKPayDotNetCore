using MiniKPayDotNetCore.Domain.Features.Customer;
using MiniKPayDotNetCore.Domain.Features.Deposit;
using MiniKPayDotNetCore.Domain.Features.Withdraw;
using System.Runtime.CompilerServices;

namespace MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Withdraw
{
    public static class WithdrawServiceEndpoint
    {
        private static readonly WithdrawService _withdrawService = new WithdrawService();

        public static void UseWithdrawServiceEndpoint(this IEndpointRouteBuilder app) {

            app.MapPost("/withdraws/{moblieNo}/{balance}", (String mobileNo, Decimal amount) =>
            {
                var withdrawModel = _withdrawService.CreateWithdraw(mobileNo, amount);

                return Results.Ok(withdrawModel);
            })
                   .WithName("CreateWithdraw")
                   .WithOpenApi();
        }
    }
}
