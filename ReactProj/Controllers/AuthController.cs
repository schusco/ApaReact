using Microsoft.AspNetCore.Mvc;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IRepository repository) : BaseController(repository)
    {
        public record LoginRequest(int PlayerNumber, string Password);
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            var result = await Repository.ValidateLogin(request.PlayerNumber, request.Password);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }
    }
}
