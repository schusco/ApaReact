using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IRepository repository, ILogger<AuthController> logger) : BaseController(repository)
    {
        private readonly ILogger<AuthController> _logger = logger;
        public record LoginRequest(int PlayerNumber, string Password, bool HasPassword);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for player number {PlayerNumber}", request.PlayerNumber);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPost("checkUser")]
        public async Task<IActionResult> CheckUser([FromBody] LoginRequest request)
        {
            try
            {
                var result = await Repository.CheckUser(request.PlayerNumber);
                return Ok(new { blankPassword = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking user for player number {PlayerNumber}", request.PlayerNumber);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
