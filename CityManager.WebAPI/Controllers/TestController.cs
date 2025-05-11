using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        // GET: api/<TestController>
        [HttpGet]
        public string Get()
        {
            return "Hello from TestController";
        }

    }
}
