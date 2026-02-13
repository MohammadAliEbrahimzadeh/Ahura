using Radenoor.Utilities;

namespace Ahura.Application.Interfaces;

public interface IWorkFlowService
{
    Task<CustomResponse> InitiateWorkFlow(string forgeName, CancellationToken cancellationToken);
}
