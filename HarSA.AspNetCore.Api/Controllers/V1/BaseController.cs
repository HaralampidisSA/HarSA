using Microsoft.AspNetCore.Mvc;

namespace HarSA.AspNetCore.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
    }
}
