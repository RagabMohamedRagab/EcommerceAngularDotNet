using Ecom.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("error/{status}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            return new ObjectResult(new ResponseResult<string>(){  Status = code, Message = "An error occurred." });
        }
    }
}
