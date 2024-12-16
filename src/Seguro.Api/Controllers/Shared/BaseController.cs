using Microsoft.AspNetCore.Mvc;

namespace Seguro.Api.Controllers.Shared
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {

    }
}