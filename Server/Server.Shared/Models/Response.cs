namespace Backend.Shared.Models;

public record ServerRequest<T>(T Data);
public record ServerResponse<T>(T Data, string Message, bool Success);