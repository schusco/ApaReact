using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController(IRepository repository,ILogger<PlayersController> logger) : BaseController(repository)
    {
        private readonly ILogger<PlayersController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await Repository.GetPlayers();
            return Ok(players);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] APAPlayer player)
        {
            try
            {
                if (!player.PlayerNumber.HasValue || player.PlayerNumber <= 0)
                    return BadRequest("Player number must be a positive integer");
                if (await Repository.IsDuplicatePlayer(player.PlayerNumber.Value))
                    return BadRequest("Player number already in use.");
                var success = await Repository.AddPlayer(new APAPlayer(player.PlayerNumber.Value, player.LastName, player.FirstName, player.CanScoreFor));
                if (success)
                    return Ok(player);
                else
                    return BadRequest("Add player failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding player with number {PlayerNumber}", player.PlayerNumber);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePlayer([FromBody] APAPlayer player)
        {
            try
            {
                var success = await Repository.UpdatePlayer(player);
                if (success)
                    return Ok(player);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating player with number {PlayerNumber}", player.PlayerNumber);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
