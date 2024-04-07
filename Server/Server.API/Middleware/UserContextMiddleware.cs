namespace Server.API.Middleware;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {   
        // Check if user is authenticated
        if (context.User.Identity is {IsAuthenticated: true})
        {
            var userManager = context.RequestServices.GetService<UserManager<User>>();
            var userEmail = context.User.Identity.Name;
            
            if (userManager != null && !string.IsNullOrWhiteSpace(userEmail))
            {
                var user = await userManager.FindByEmailAsync(userEmail);
                // Attach the user object to the HttpContext.Items to make it accessible in controllers
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }
        }

        await _next(context);
    }
}