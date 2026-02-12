using Ahura.Application.Contracts.Requests;
using Radenoor.Utilities;

namespace Ahura.Application.Interfaces;

public interface IForgeServices
{
    Task<CustomResponse> AddNewForge(AddForgeDto dto, CancellationToken cancellationToken);
}
