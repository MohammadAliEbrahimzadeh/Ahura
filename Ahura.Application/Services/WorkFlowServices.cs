using Ahura.Application.Contracts.Requests;
using Ahura.Application.Helpers;
using Ahura.Application.Interfaces;
using Ahura.Infrastructure;
using Ahura.Persistence.Entities;
using Ahura.Persistence.Enums;
using Ardalis.GuardClauses;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Radenoor.Utilities;
using System.Net;
using System.Text.Json;

namespace Ahura.Application.Services;

public class WorkFlowServices : IWorkFlowServices
{
    private readonly IUnitOfWork _unitOfWork;

    public WorkFlowServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse> InitiateWorkFlow(string forgeName, CancellationToken cancellationToken)
    {
        var forgeEntity = await _unitOfWork.GetAsQueryable<Forge>()
            .FirstOrDefaultAsync(x => x.Name!.Equals(forgeName), cancellationToken);

        if (forgeEntity == null)
            return new CustomResponse(null, false, "Workflow not found");

        var steps = JsonSerializer.Deserialize<List<ForgeStepDto>>(forgeEntity.ForgeSteps!);

        if (steps == null || !steps.Any())
            return new CustomResponse(null, false, "No steps found");

        var orderedSteps = steps.OrderBy(s => s.Order).ToList();

        foreach (var step in orderedSteps)
        {
            var config = Convertors.ConvertToRelatedClass(step.ActionType, step.Configuration!);

            Guard.Against.Null(config, nameof(config), $"Configuration was null for Step '{step.Order}' (Type: {step.ActionType})");

            if (step.ActionType == ActionTypeEnum.ExternalHttpCall && config is HttpRequestCall httpCall)
            {
                var success = await SendApiRequest(httpCall, cancellationToken);

                if (!success)
                    throw new Exception($"Step : {step.Order} out of {orderedSteps.Count} failed.");
            }
        }

        return new CustomResponse(true, true, "Workflow Executed");
    }


    #region Private Method(s)

    public async Task<bool> SendApiRequest(HttpRequestCall requestCall, CancellationToken cancellationToken)
    {
        Guard.Against.Null(requestCall, nameof(requestCall));
        Guard.Against.NullOrWhiteSpace(requestCall.Endpoint, nameof(requestCall.Endpoint));

        using var httpClient = new HttpClient();

        StringContent? content = null;

        if (requestCall.HttpCallEnum == HttpCallEnum.Post || requestCall.HttpCallEnum == HttpCallEnum.Put)
        {
            if (!string.IsNullOrEmpty(requestCall.PostBody))
            {
                content = new StringContent(requestCall.PostBody, System.Text.Encoding.UTF8, "application/json");
            }
        }

        if (requestCall.Headers != null)
        {
            foreach (var header in requestCall.Headers)
            {
                if (!string.IsNullOrEmpty(header.Key) && header.Value != null)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        HttpResponseMessage response = requestCall.HttpCallEnum switch
        {
            HttpCallEnum.Get => await httpClient.GetAsync(requestCall.Endpoint, cancellationToken),
            HttpCallEnum.Post => await httpClient.PostAsync(requestCall.Endpoint, content, cancellationToken),
            HttpCallEnum.Put => await httpClient.PutAsync(requestCall.Endpoint, content, cancellationToken),
            HttpCallEnum.Delete => await httpClient.DeleteAsync(requestCall.Endpoint, cancellationToken),
            _ => throw new NotSupportedException($"HTTP Method '{requestCall.HttpCallEnum}' is not supported.")
        };

        //ToDo : Log Why Failed
        return response.IsSuccessStatusCode;
    }

    #endregion
}
