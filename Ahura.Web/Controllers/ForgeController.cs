using Ahura.Application.Contracts.Requests;
using Ahura.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Radenoor.Utilities;

namespace Ahura.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ForgeController : ControllerBase
{
    private readonly IForgeServices _forgeServices;

    public ForgeController(IForgeServices forgeServices)
    {
        _forgeServices = forgeServices;
    }

    [HttpPost]
    [Route(nameof(AddNewForge))]
    public async Task<CustomResponse> AddNewForge(AddForgeDto dto, CancellationToken cancellationToken) =>
       await _forgeServices.AddNewForge(dto, cancellationToken);
}
