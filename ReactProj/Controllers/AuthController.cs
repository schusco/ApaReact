using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IRepository repository) : BaseController(repository)
    {
        public record LoginRequest(int PlayerNumber, string Password, bool HasPassword);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            APAPlayer user;
            if (!request.HasPassword)
            {
                user = await Repository.SetPassword(request.PlayerNumber, request.Password);
                return Ok(user);
            }
            else
            {
                user = await Repository.ValidateLogin(request.PlayerNumber, request.Password);
                if (user is null)
                    return BadRequest();
                return Ok(user);
            }
        }
        [HttpPost("checkUser")]
        public async Task<IActionResult> CheckUser([FromBody] LoginRequest request)
        {
            var result = await Repository.CheckUser(request.PlayerNumber);
            return Ok(new { blankPassword = result });
        }
    }
}
