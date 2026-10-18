using SmartHomeAI.AI;

namespace SmartHomeAI.API.Services;

public class AIOrchestrator : IAIOrchestrator
{
    private readonly IAIService _aiService;

    public AIOrchestrator(IAIService aiService)
    {
        _aiService = aiService;
    }

    public async Task<string> ProcessAsync(string input)
    {
        return await _aiService.AskAsync(input);
    }
}
