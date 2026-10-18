using Microsoft.AspNetCore.Mvc;
using SmartHomeAI.API.Services;

namespace SmartHomeAI.API.Controllers;

public class AskRequest
{
    public string? Input { get; set; }
}

[ApiController]
[Route("api/ai")]
public class AIController : ControllerBase
{
    private readonly IAIOrchestrator _ai;

    public AIController(IAIOrchestrator ai)
    {
        _ai = ai;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Input))
        {
            return BadRequest("Input is required.");
        }

        var result = await _ai.ProcessAsync(request.Input);
        return Ok(result);
    }
}
