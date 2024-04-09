using Server.Shared.Exceptions;

namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : BaseController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginModel model)
    {
        await _userService.Register(model.Email, model.Password);
        return await Login(model);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var message = await _userService.Login(model);
         
            return Ok(message);
        }
        catch (UserAuthenticationException ex)
        {
            return BadRequest(ex.SafeMessage);
        }
    }

    [HttpGet("refresh-token")]
    public IActionResult RefreshToken() => Ok(_userService.GetTokenMessage(User));
}