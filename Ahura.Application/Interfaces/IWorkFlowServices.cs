using Radenoor.Utilities;

namespace Ahura.Application.Interfaces;

public interface IWorkFlowServices
{
    Task<CustomResponse> InitiateWorkFlow(string forgeName, CancellationToken cancellationToken);
}
