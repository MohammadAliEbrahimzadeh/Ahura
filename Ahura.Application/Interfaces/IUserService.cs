using Ahura.Application.Contracts.Requests;
using Radenoor.Utilities;

namespace Ahura.Application.Interfaces;

public interface IUserService
{
    Task<CustomResponse> AddNewUser(AddUserDto addUserDto, CancellationToken cancellationToken);
}
