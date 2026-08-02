using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Apa8Controller(IRepository repository) : BaseController(repository)
    {
        [HttpGet("{playerId:int}")]
        public async Task<IList<APA8BallScore>> Get(int playerId)
        {
            return await Repository.Get8BallScores(playerId);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player8BallScore model)
        {
            if (!model.IsValid())
                return BadRequest("Unable to save, please ensure all fields are filled.");
            var success = await Repository.Add8BallScore(model);
            if (success)
                return Ok(model);
            return BadRequest("Unable to save, error occurred");
        }
    }
}
