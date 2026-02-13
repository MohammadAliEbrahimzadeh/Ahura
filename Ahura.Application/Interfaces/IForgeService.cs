using Ahura.Application.Contracts.Requests;
using Radenoor.Utilities;

namespace Ahura.Application.Interfaces;

public interface IForgeService
{
    Task<CustomResponse> AddNewForge(AddForgeDto dto, CancellationToken cancellationToken);
}
