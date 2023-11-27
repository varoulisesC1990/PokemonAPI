using Microsoft.AspNetCore.Mvc;
namespace PokemonAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [NonAction]
        [Route("test/get1")]
        public string Get()
        {
            return "Returning from TestController Get Method";
        }

        [NonAction]
        [Route("test/get2")]
        public string Get2()
        {
            return "Returning from TestController Get2 Method";
        }
    }
}
