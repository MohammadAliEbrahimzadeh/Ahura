using Ahura.Application.Contracts.Requests;
using Ahura.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Ahura.Application.Helpers;

public static class Convertors
{
    public static object? ConvertToRelatedClass(ActionTypeEnum actionType, object configuration)
    {
        if (configuration == null)
            return null;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        if (configuration is JsonElement element)
        {
            return actionType switch
            {
                ActionTypeEnum.ExternalHttpCall => element.Deserialize<HttpRequestCall>(options),
                _ => null
            };
        }

        return null;
    }
}
