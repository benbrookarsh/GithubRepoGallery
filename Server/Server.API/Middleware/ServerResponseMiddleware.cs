using System.Text.Json;

namespace Server.API.Middleware;

public class ServerResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ServerResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body = originalBodyStream;
        responseBody.Seek(0, SeekOrigin.Begin);
        var readStream = new StreamReader(responseBody);
        var responseBodyText = await readStream.ReadToEndAsync();

        object responseData;
        try
        {
            responseData = JsonDocument.Parse(responseBodyText);
        }
        catch (JsonException)
        {
            responseData = responseBodyText;
        }

        var serverResponse = new ServerResponse<object>(responseData, "Success", context.Response.StatusCode == 200);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var responseJson = JsonSerializer.Serialize(serverResponse, options);

        context.Response.ContentType = "application/json";
        context.Response.ContentLength = null; // Important to remove the content-length since we're changing the body length

        await context.Response.WriteAsync(responseJson);
    }
    
}