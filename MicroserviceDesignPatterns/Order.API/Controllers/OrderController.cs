using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        // GET
        [HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(DateTime.Now);
    }
}