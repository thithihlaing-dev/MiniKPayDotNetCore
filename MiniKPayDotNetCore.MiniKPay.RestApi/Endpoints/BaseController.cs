using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKPayDotNetCore.MiniKPay.Database.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiniKPayDotNetCore.MiniKPay.RestApi.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute(object model)
        {

            JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model));
            if (jObj.ContainsKey("Response"))
            {
                BaseResponseModel baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jObj["Response"]!.ToString())!;

                if (baseResponseModel.RespType == EnumRespType.Pending)
                    return StatusCode(201, model);

                if (baseResponseModel.RespType == EnumRespType.ValidationError)
                    return BadRequest(model);

                if (baseResponseModel.RespType == EnumRespType.SystemError)
                    return StatusCode(500, model);

                return Ok(model);
            }

            return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your ResponseModel.");
        }
    }
}
