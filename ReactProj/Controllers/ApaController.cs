using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApaController : ControllerBase
    {
        [HttpGet]
        public IList<APA9BallScore> Get()
        {
            var scores = Repository.Get9BallScores();
            return scores;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Player9BallScore model)
        {
            if (!model.IsValid())
                return BadRequest("Unable to save, please ensure all fields are filled.");
            var success = Repository.Add9BallScore(model);
            if (success)
                return Ok();
            return BadRequest("Unable to save, error occurred");

        }
    }
}
