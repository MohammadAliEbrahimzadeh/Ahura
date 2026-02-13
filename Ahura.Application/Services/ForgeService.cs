using Ahura.Application.Contracts.Requests;
using Ahura.Application.Interfaces;
using Ahura.Infrastructure;
using Ahura.Persistence.Entities;
using Mapster;
using Radenoor.Utilities;
using System.Net;

namespace Ahura.Application.Services;

public class ForgeService : IForgeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ForgeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse> AddNewForge(AddForgeDto dto, CancellationToken cancellationToken)
    {
        var forgeEntity = dto.Adapt<Forge>();

        await _unitOfWork.AddAsync(forgeEntity, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse(forgeEntity.Id, true, "Created", HttpStatusCode.Created);
    }
}
