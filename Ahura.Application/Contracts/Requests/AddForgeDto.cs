using Ahura.Persistence.Enums;

namespace Ahura.Application.Contracts.Requests;

public class AddForgeDto
{
    public string? Name { get; set; }

    public List<ForgeStepDto>? Steps { get; set; }
}


public class ForgeStepDto
{
    public int Order { get; set; } 

    public ActionTypeEnum ActionType { get; set; }

    public object? Configuration { get; set; }
}


public class HttpRequestCall
{
    public HttpCallEnum HttpCallEnum { get; set; }

    public string? Endpoint { get; set; }

    public string? PostBody { get; set; }

    public List<HttpRequestHeaders>? Headers { get; set; }
}


public class HttpRequestHeaders
{
    public string? Key { get; set; }

    public string? Value { get; set; }
}