using Microsoft.Extensions.Configuration;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using OpenAI;
using System.Runtime.CompilerServices;

var config = new ConfigurationBuilder()
    .SetBasePath(GetProjectDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Captures the compile-time path of THIS file (Program.cs), so appsettings.json
// is loaded from the same folder Program.cs lives in, regardless of where the
// built exe/dll actually runs from.
static string GetProjectDirectory([CallerFilePath] string sourceFilePath = "")
    => Path.GetDirectoryName(sourceFilePath)!;

var apiKey = config["OpenAI:ApiKey"]!;
var model = config["OpenAI:Model"]!;

// Single IChatClient shared across all agents in this demo
var chatClient = new OpenAI.Chat.ChatClient(model, apiKey).AsIChatClient();


AIAgent researchAgent = new ChatClientAgent(
chatClient,
name: "ResearchAgent",
instructions: """
        You are a research specialist. When given a topic, return 5-7 factual bullet
        points with key statistics and context. Do not summarize. Do not editorialize.
        State facts only. If you don't know something with confidence, say so.
        """);

AIAgent summarizerAgent = new ChatClientAgent(
    chatClient,
    name: "SummarizerAgent",
    instructions: """
        You are a technical writer. Read the research output in this conversation
        and produce a clear, readable summary of 150-200 words. Use plain language.
        Write in continuous prose - no bullet points, no headers. Do not add any
        information that was not present in the research output.
        """);

AIAgent reviewerAgent = new ChatClientAgent(
    chatClient,
    name: "ReviewerAgent",
    instructions: """
        You are a quality reviewer. Read the summary produced in this conversation.
        Evaluate it on three criteria:
        1. Does it accurately reflect the research? (no hallucination)
        2. Is it free of vague, unsupported claims?
        3. Is it readable by a non-specialist?

        If it passes all three, respond with exactly: APPROVED
        If it fails any criterion, respond with: REVISION NEEDED - [specific feedback]
        """);


var researchResult = await researchAgent.RunAsync(
"Research the impact of soil microbiome diversity on crop yield in dryland farming.");

Console.WriteLine("=== Research Output ===");
Console.WriteLine(researchResult.Text);

const int maxIterations = 3;
List<ChatMessage> conversation = new()
{
    new ChatMessage(ChatRole.User, researchResult.Text)
};

string? approvedSummary = null;

for (int i = 0; i < maxIterations; i++)
{
    var summaryResponse = await summarizerAgent.RunAsync(conversation);
    Console.WriteLine($"\n[Attempt {i + 1}] SummarizerAgent:");
    Console.WriteLine(summaryResponse.Text);

    conversation.Add(new ChatMessage(ChatRole.Assistant, summaryResponse.Text) { AuthorName = "SummarizerAgent" });

    var reviewResponse = await reviewerAgent.RunAsync(conversation);
    Console.WriteLine($"\n[Attempt {i + 1}] ReviewerAgent:");
    Console.WriteLine(reviewResponse.Text);

    conversation.Add(new ChatMessage(ChatRole.Assistant, reviewResponse.Text) { AuthorName = "ReviewerAgent" });

    if (reviewResponse.Text.Contains("APPROVED", StringComparison.OrdinalIgnoreCase))
    {
        approvedSummary = summaryResponse.Text;
        break;
    }

    conversation.Add(new ChatMessage(ChatRole.User,
        $"Revise the summary based on this feedback: {reviewResponse.Text}"));
}

Console.WriteLine("\n=== Final Approved Summary ===");
Console.WriteLine(approvedSummary ?? "No approved summary after maximum iterations.");