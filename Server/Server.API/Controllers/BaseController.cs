namespace Server.API.Controllers;

[Authorize]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Only works for authorized requests, meaning no [AllowAnonymous] attribute
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private User GetCurrentUser()
    {
        var user = HttpContext.Items["User"] as User;
        if (user == null)
        {
            throw new Exception("Couldn't find user");
        }
        return user;
    }

    protected User User => GetCurrentUser();
}