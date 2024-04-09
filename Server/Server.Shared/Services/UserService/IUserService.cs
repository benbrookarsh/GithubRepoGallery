using Microsoft.AspNetCore.Identity;
using Server.Shared.Models;

namespace Server.Shared.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    
    Task<IdentityResult> Register(string email, string password);

    Task<TokenMessage> Login(LoginModel model);

    TokenMessage GetTokenMessage(User user);
}