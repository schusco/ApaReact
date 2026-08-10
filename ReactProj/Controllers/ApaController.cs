using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApaController(IRepository repository, ILogger<ApaController> logger) : BaseController(repository)
    {
        private readonly ILogger<ApaController> _logger = logger;

        [HttpGet("{playerId:int}")]
        public async Task<IList<APA9BallScore>> Get(int playerId)
        {
            var scores = await Repository.Get9BallScores(playerId);
            return scores;
        }
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Player9BallScore model)
        {
            try
            {
                if (!model.IsValid())
                    return BadRequest("Unable to save, please ensure all fields are filled.");
                var success = await Repository.Add9BallScore(model);
                if (success)
                    return Ok(model);
                return BadRequest("Unable to save, error occurred");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding 9-ball score for player ID {PlayerId}", model.PlayerId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
