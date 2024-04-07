namespace Server.API.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class AuthController : BaseController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
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
        var message = await _userService.Login(model);

        if (message is null)
            return Unauthorized();

        return Ok(message.ToServerResult());
    }

    [HttpGet]
    [Route("refresh-token")]
    public IActionResult RefreshToken()
    {
        var user = GetCurrentUser();

        var tokenMessage = _userService.GetTokenMessage(user);

        return Ok(tokenMessage.ToServerResult());
    }
}