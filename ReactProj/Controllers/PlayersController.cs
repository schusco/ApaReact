using Microsoft.AspNetCore.Mvc;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : BaseController
    {
        public PlayersController(IRepository repository) : base(repository) { }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            var players = Repository.GetPlayers();
            return Ok(players);
        }
        [HttpPost]
        public IActionResult AddPlayer([FromBody] APAPlayer player)
        {
            if (!player.PlayerNumber.HasValue || player.PlayerNumber <= 0)
                return BadRequest("Player number must be a positive integer");
            if (Repository.IsDuplicatePlayer(player.PlayerNumber.Value))
                return BadRequest("Player number already in use.");
            var success = Repository.AddPlayer(new APAPlayer(player.PlayerNumber.Value, player.LastName, player.FirstName, player.CanScoreFor));
            if (success)
                return Ok(player);
            else
                return BadRequest("Add player failed");
        }
    }
}
