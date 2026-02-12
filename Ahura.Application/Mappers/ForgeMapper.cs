using Ahura.Application.Contracts.Requests;
using Ahura.Persistence.Entities;
using Mapster;
using System.Text.Json;

namespace Ahura.Application.Mappers;

public class ForgeMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddForgeDto, Forge>()
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.ForgeSteps, src => JsonSerializer.Serialize(src.Steps, (JsonSerializerOptions)null));
    }
}