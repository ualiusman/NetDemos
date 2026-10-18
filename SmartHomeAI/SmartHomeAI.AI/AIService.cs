using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel;

namespace SmartHomeAI.AI;

public interface IAIService
{
    Task<string> AskAsync(string input);
}

public class AIService : IAIService
{
    private readonly Kernel _kernel;
    private readonly ILogger<AIService> _logger;

    public AIService(IConfiguration configuration, ILogger<AIService> logger)
    {
        _logger = logger;
        _kernel = KernelFactory.CreateKernel(configuration);
    }

    public async Task<string> AskAsync(string input)
    {
        try
        {
            var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            var executionSettings = new OpenAIPromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
            };

            var history = new ChatHistory();
            history.AddUserMessage(input);

            var result = await chatCompletionService.GetChatMessageContentAsync(
                history,
                executionSettings: executionSettings,
                kernel: _kernel);

            return result.Content ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AI request failed.");
            return "Sorry, I could not process that request.";
        }
    }
}
