using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.Deposit;

namespace MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Deposit
{
    public static class DepositServiceEndpoint
    {
        private static readonly DepositService _depositService = new DepositService();


        public static void UseDepositServiceEndpoint (this IEndpointRouteBuilder app)
        {

            app.MapPost("/deposits/{moblieNo}/{balance}",  (String mobileNo, Decimal amount) =>
            {
                var depositModel = _depositService.CreateDeposit(mobileNo, amount);
                
                return Results.Ok(depositModel);
            })
               .WithName("CreateDeposit")
               .WithOpenApi();

        }
    }
}
