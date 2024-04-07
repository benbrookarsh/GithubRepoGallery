using Microsoft.AspNetCore.Identity;
using Server.Shared.Models;
using Server.Shared.Services.UserService;

namespace Server.Shared.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }
}

public interface IAuthService
{
    
}