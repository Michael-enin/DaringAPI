using DaringAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DaringAPI.Controllers
{
    [ServiceFilter(typeof(UserActivityLogger))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}