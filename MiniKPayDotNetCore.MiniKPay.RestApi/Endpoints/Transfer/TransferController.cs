using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniKPayDotNetCore.Domain.Features.Transfer;
using MiniKPayDotNetCore.MiniKPay.Database.Models;

namespace MiniKPayDotNetCore.MiniKPay.RestApi.Endpoints
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : BaseController
    {
        private readonly TransferService _transferService;
        public TransferController()
        {
            _transferService = new TransferService();
        }

        [HttpPost]

        public async Task<IActionResult> PostTransfer(string fromMobileNo, string toMobileNo, decimal amount, string message, string pin)
        {
            //var transferModel = await _transferService.CreateTransfer(fromMobileNo, toMobileNo, amount, message, pin);
            var transferModel2 = await _transferService.CreateTransfer(fromMobileNo, toMobileNo, amount, message, pin);

            //if (transferModel.Response.IsSuccess)
            //    return Ok(transferModel);
            //if (transferModel.Response.RespType == EnumRespType.ValidationError)
            //    return BadRequest(transferModel);

            //if (transferModel.Response.RespType == EnumRespType.SystemError)
            //    return StatusCode(500, transferModel);

            //return Execute(transferModel);
            return Execute(transferModel2);

        }
    }
}
