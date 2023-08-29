using Microsoft.AspNetCore.Mvc;

namespace Zamzam.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
