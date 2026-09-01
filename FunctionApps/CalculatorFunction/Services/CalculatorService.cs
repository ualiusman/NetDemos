
namespace CalculatorFunction.Services;

public class CalculatorService
{
    public double Calculate (double a, double b, string operation)
    {
        operation = operation.ToLower();
        
        return operation switch
        {
            "add" => a + b,
            "subtract" => a - b,
            "multiply" => a * b,
            "divide" => b == 0
            ? throw new DivideByZeroException()
            : a / b,
            
            _ => throw new ArgumentException("Invalid Operation")
        };
    }
}