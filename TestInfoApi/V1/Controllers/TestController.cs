using HarSA.AspNetCore.Api.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestInfoApi.V1.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTest()
        {
            return Json("Ok");
        }
    }
}
