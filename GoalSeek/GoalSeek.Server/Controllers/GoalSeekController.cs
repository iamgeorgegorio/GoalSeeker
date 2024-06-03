using GoalSeek.Server.Helpers;
using GoalSeek.Server.Interfaces;
using GoalSeek.Server.Models;
using GoalSeek.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoalSeek.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalSeekController : ControllerBase
    {
        private readonly IValidation _validation;
        private readonly IGoalSeekProcessor _seekProcessor;
        private readonly ILogger<GoalSeekController> _logger;

        public GoalSeekController(IValidation validation, IGoalSeekProcessor seekProcessor, ILogger<GoalSeekController> logger)
        {
            _validation = validation;
            _seekProcessor = seekProcessor;
            _logger = logger;
        }

        [HttpPost("goalseek")]
        public IActionResult Post([FromBody] GoalSeekData data)
        {
            try
            {
                var valError = _validation.CheckExpression(data);

                if(!string.IsNullOrEmpty(valError))
                {
                    _logger.LogError("Validation failed!");
                    return BadRequest(valError);
                }

                _logger.LogInformation("Validation passed!");

                var processResult = _seekProcessor.Process(data);

                _logger.LogInformation("Goal seek processing completed!");

                return Ok(processResult);

            }
            catch (Exception ex)
            {
                _logger.LogError("Goal seek processing failed!");
                return BadRequest(ex.Message);
            }

        }
    }
}
