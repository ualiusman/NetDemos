using CalculatorFunction.Models;
using CalculatorFunction.Services;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorFunction;

public class CalculatorFunctions
{
private readonly ILogger<CalculatorFunctions> _logger;
private readonly CalculatorService _calculatorService;

public CalculatorFunctions(
    ILogger<CalculatorFunctions> logger,
    CalculatorService calculatorService)
{
    _logger = logger;
    _calculatorService = calculatorService;
}

[Function("CalculatorFunction")]
public IActionResult Run(
    [HttpTrigger(
        AuthorizationLevel.Anonymous,
        "get",
        "post")]
    HttpRequest req)
{
    _logger.LogInformation(
        "Calculator function received a request.");

    // Read values from query string
    string? aValue = req.Query["a"];
    string? bValue = req.Query["b"];
    string? operation = req.Query["operation"];

    // Validate A
    if (!double.TryParse(aValue, out double a))
    {
        return new BadRequestObjectResult(
            "Parameter 'a' must be a valid number.");
    }

    // Validate B
    if (!double.TryParse(bValue, out double b))
    {
        return new BadRequestObjectResult(
            "Parameter 'b' must be a valid number.");
    }

    // Validate operation
    if (string.IsNullOrWhiteSpace(operation))
    {
        return new BadRequestObjectResult(
            "Parameter 'operation' is required.");
    }

    try
    {
        // Call CalculatorService
        double result = _calculatorService.Calculate(
            a,
            b,
            operation);

        // Create response object
        var calculatorResponse = new CalculatorResponse
        {
            A = a,
            B = b,
            Operation = operation,
            Result = result
        };

        // Return HTTP 200 + JSON response
        return new OkObjectResult(calculatorResponse);
    }
    catch (DivideByZeroException)
    {
        return new BadRequestObjectResult(
            "Cannot divide by zero.");
    }
    catch (ArgumentException ex)
    {
        return new BadRequestObjectResult(
            ex.Message);
    }
}


}
