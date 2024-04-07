using Backend.Shared.Models;

namespace Server.Shared.Extensions;

public static class ResponseExtensions
{
    public static async Task<ServerResponse<T>> ToServerResponseAsync<T>(this Task<T> task, string successMessage = "success")
    {
        try
        {
            var resultData = await task;
            return new ServerResponse<T>(resultData, successMessage, true);
        }
        catch (Exception ex)
        {
            return new ServerResponse<T>(default, ex.Message, false);
        }
    }


    public static ServerResponse<T>
        ToServerResult<T>(this T value, string successMessage = "success", bool success = false) =>
        new(value, successMessage, value is not null);

}