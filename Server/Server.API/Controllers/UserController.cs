namespace Server.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get() => await _userService.GetAll();
}