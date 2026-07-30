using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApaController(IRepository repository) : BaseController(repository)
    {
        [HttpGet]
        public async Task<IList<APA9BallScore>> Get()
        {
            var scores = await Repository.Get9BallScores();            
            return scores;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Player9BallScore model)
        {
            if (!model.IsValid())
                return BadRequest("Unable to save, please ensure all fields are filled.");
            var success =await Repository.Add9BallScore(model);
            if (success)
                return Ok(model);
            return BadRequest("Unable to save, error occurred");

        }
    }
}
