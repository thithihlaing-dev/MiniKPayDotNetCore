using MiniKPayDotNetCore.Database.Models;
using MiniKPayDotNetCore.Domain.Features.Customer;

namespace MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Customer;

public static class CustomersServiceEndpoint
{
    private static readonly CustomerService _service = new CustomerService();


    public static void UseCustomersServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", () =>
        {            
            var lst = _service.GetCustomers();

            return Results.Ok(lst);
        })
        .WithName("GetCustomers")
        .WithOpenApi();

        app.MapGet("/customers/{id}", (int id) =>
        {
            var customer = _service.GetCustomer(id);
            if(customer is null)
            {
                return Results.BadRequest("No Data found");
            }
            return Results.Ok(customer);
        })
        .WithName("GetCustomer")
        .WithOpenApi();

        app.MapPost("/customers{balance}", (TblCustomer customer, Decimal balance) =>
        {
            var customerModel = _service.CreateCustomer(customer,balance);
            if(customerModel is null)
            {
                return Results.BadRequest("Deposit Balance at least 10000");
            }
            return Results.Ok(customerModel);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        app.MapPut("/customers/{id}", (int id, TblCustomer customer) =>
        {
            var item = _service.UpdateCustomer(id, customer);
            if (item is null)
            {
                return Results.BadRequest("No Data Found");
            }
            return Results.Ok(item);
        })
       .WithName("UpdateCustomer")
       .WithOpenApi();

        app.MapPatch("/customers/{id}", (int id, TblCustomer customer) =>
        {
            var item = _service.PatchCustomer(id, customer);
            if (item is null)
            {
                return Results.BadRequest("No Data Found");
            }
            return Results.Ok(item);
        })
       .WithName("PatchCustomer")
       .WithOpenApi();

         app.MapDelete("/customers{id}", (int id) =>
         {
             var item = _service.DeleteCustomer(id);
             if (item is false)
             {
                 return Results.BadRequest("No Data Found");
             }
             return Results.Ok("Deleting Successful");
         })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
