using Ahura.Application.Contracts.Requests;
using Ahura.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Radenoor.Utilities;

namespace Ahura.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkFlowController : ControllerBase
{
    private readonly IWorkFlowServices _forgeServices;

    public WorkFlowController(IWorkFlowServices forgeServices)
    {
        _forgeServices = forgeServices;
    }

    [HttpGet]
    [Route(nameof(InitiateWorkFlow))]
    public async Task<CustomResponse> InitiateWorkFlow([FromQuery] string forgeName, CancellationToken cancellationToken) =>
       await _forgeServices.InitiateWorkFlow(forgeName, cancellationToken);
}
