using Ahura.Application.Contracts.Requests;
using Ahura.Persistence.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ahura.Application.Mappers;

public class UserMapper
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddUserDto, User>()
              .Map(dest => dest.Username, src => src.Username)
              .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);
    }
}
