public class TrafficLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public TrafficLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBody = context.Response.Body;
        Console.WriteLine($"Response size: {originalBody.Length} bytes");
    }
}
