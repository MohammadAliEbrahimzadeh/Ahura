using Ahura.Persistence.Enums;

namespace Ahura.Application.Contracts.Requests;

public class AddUserDto
{
    public string? Username { get; set; }

    public string? PhoneNumber { get; set; }
}