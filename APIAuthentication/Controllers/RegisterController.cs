using Microsoft.AspNetCore.Mvc;

namespace APIAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        //POST Api Controller
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
