using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Zamzam.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
