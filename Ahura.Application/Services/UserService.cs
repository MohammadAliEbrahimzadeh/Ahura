using Ahura.Application.Contracts.Requests;
using Ahura.Application.Interfaces;
using Ahura.Infrastructure;
using Ahura.Persistence.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Radenoor.Utilities;
using System.Net;

namespace Ahura.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse> AddNewUser(AddUserDto addUserDto, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.GetAsQueryable<User>().AnyAsync(x => x.Username!.Equals(addUserDto.Username), cancellationToken))
            return new CustomResponse(null, true, "Username Already Taken", HttpStatusCode.Conflict);

        if (await _unitOfWork.GetAsQueryable<User>().AnyAsync(x => x.PhoneNumber!.Equals(addUserDto.PhoneNumber), cancellationToken))
            return new CustomResponse(null, true, "PhoneNumber Already Taken", HttpStatusCode.Conflict);

        await _unitOfWork.AddAsync(addUserDto.Adapt<User>(), cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse(null, true, "Created", HttpStatusCode.OK);
    }
}
