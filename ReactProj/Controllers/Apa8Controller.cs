using Microsoft.AspNetCore.Mvc;
using ReactProj.Models;

namespace ReactProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Apa8Controller : ControllerBase
    {
        [HttpGet]
        public IList<APA8BallScore> Get()
        {
            return Repository.Get8BallScores();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Player8BallScore model)
        {
            if (!model.IsValid())
                return BadRequest("Unable to save, please ensure all fields are filled.");
            var success = Repository.Add8BallScore(model);
            if (success)
                return Ok(model);
            return BadRequest("Unable to save, error occurred");
        }
    }
}
