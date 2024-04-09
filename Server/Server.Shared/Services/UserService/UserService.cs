using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Server.Shared.Exceptions;
using Server.Shared.Interfaces;
using Server.Shared.Models;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Server.Shared.Services.UserService;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<User> userManager, IRepository<User> userRepository, IConfiguration configuration)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAll().ToListAsync();
    }

    public async Task<IdentityResult> Register(string email, string password)
    {
        User user = new()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = email
        };
        
        var existingUser = await _userManager.FindByNameAsync(user.UserName);
        
        if (existingUser is not null)
            throw new UserExistsException(user.UserName);

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<TokenMessage> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if (user is null) 
            throw new UserNotFoundException(model.Email);
        
        var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        
        if (!isValidPassword) 
            throw new UserIncorrectPasswordException(model.Email);
        
        return GetTokenMessage(user);
    }

    public TokenMessage GetTokenMessage(User user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new TokenMessage(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.Email);
    }
}
