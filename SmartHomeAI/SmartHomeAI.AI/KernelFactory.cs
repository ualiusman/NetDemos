using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel.Plugins;
using SmartHomeAI.Infrastructure.Repositories;

namespace SemanticKernel;

public static class KernelFactory
{
    public static Kernel CreateKernel(IConfiguration configuration)
    {
        var openAiSettings = configuration.GetSection("OpenAI");
        var modelId = openAiSettings["ModelId"] ?? throw new InvalidOperationException("OpenAI:ModelId is not configured.");
        var apiKey = openAiSettings["ApiKey"] ?? throw new InvalidOperationException("OpenAI:ApiKey is not configured.");
        var connectionString = configuration["ConnectionString"] ?? throw new InvalidOperationException("ConnectionString is not configured.");

        var builder = Kernel.CreateBuilder();

        builder.AddOpenAIChatCompletion(
            modelId: modelId,
            apiKey: apiKey);

        var kernel = builder.Build();
        var lightsRepository = new SqlLightsRepository(connectionString);
        kernel.Plugins.AddFromObject(new LightsPlugin(lightsRepository));

        return kernel;
    }
}
