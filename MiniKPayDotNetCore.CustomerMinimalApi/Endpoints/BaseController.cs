using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniKPayDotNetCore.MiniKPay.Database.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace MiniKPayDotNetCore.MiniKPay.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        
        //public IActionResult Execute(object model)
        //{
        //    JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model));

        //    if (jObj.ContainsKey("Response"))
        //    {
        //        BaseResponseModel baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(jObj["Response"]!.ToString())!;

        //        if (baseResponseModel.RespType == EnumRespType.Pending)
        //            return StatusCode(201, model);
               
        //    }
         

        //    return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your ResponseModel.");
        //}
    }
}
