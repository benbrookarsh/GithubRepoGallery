using Server.Shared.Exceptions;

namespace Server.API.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class AuthController : BaseController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] LoginModel model)
    {
        await _userService.Register(model.Email, model.Password);
        return await Login(model);
    }

    [HttpPost("login")]
    [AllowAnonymous]
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

    [HttpGet]
    [Route("refresh-token")]
    public IActionResult RefreshToken()
    {
        var tokenMessage = _userService.GetTokenMessage(User);
        return Ok(tokenMessage.ToServerResult());
    }
}