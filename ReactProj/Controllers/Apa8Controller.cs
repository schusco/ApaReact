using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Apa8Controller(IRepository repository,ILogger<Apa8Controller> logger) : BaseController(repository)
    {
        private readonly ILogger<Apa8Controller> _logger = logger;

        [HttpGet("{playerId:int}")]
        public async Task<IList<APA8BallScore>> Get(int playerId)
        {
            return await Repository.Get8BallScores(playerId);
        }
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Player8BallScore model)
        {
            try
            {
                if (!model.IsValid())
                    return BadRequest("Unable to save, please ensure all fields are filled.");
                var success = await Repository.Add8BallScore(model);
                if (success)
                    return Ok(model);
                return BadRequest("Unable to save, error occurred");
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, "Error occurred while adding 8-ball score for player ID {PlayerId}", model.PlayerId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
