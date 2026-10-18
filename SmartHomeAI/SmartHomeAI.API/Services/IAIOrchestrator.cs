namespace SmartHomeAI.API.Services;

public interface IAIOrchestrator
{
    Task<string> ProcessAsync(string input);
}
