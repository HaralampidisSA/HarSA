using HarSA.AspNetCore.Api.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.V1.ViewModels;

namespace TestApi.V1.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Ping()
        {
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Pong([FromBody]AddVM add)
        {
            if (add != null)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
