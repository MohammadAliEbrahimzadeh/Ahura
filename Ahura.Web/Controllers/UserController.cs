using Ahura.Application.Contracts.Requests;
using Ahura.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Radenoor.Utilities;

namespace Ahura.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route(nameof(AddNewUser))]
    public async Task<CustomResponse> AddNewUser(AddUserDto dto, CancellationToken cancellationToken) =>
       await _userService.AddNewUser(dto, cancellationToken);
}
