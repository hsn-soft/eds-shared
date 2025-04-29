using System.Net;
using Eds.Shared.Localization;
using HsnSoft.Base;
using HsnSoft.Base.Validation.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

namespace Eds.Shared.Hosting.Microservices.Handlers;

internal sealed class ResponseExceptionHandler : IResponseExceptionHandler
{
    private readonly IStringLocalizer _localizer;

    public ResponseExceptionHandler(IStringLocalizerFactory factory)
    {
        _localizer = factory.CreateMultiple([typeof(ValidationResource), typeof(SharedResource)]);
    }

    public (int code, List<string> messages) Handle(Exception ex, IHostEnvironment env)
    {
        var code = StatusCodes.Status500InternalServerError;
        var messages = new List<string>();

        if (ex is null) return (code, messages);

        switch (ex)
        {
            // Some logic to handle specific exceptions
            case BusinessException be:
            {
                code = StatusCodes.Status400BadRequest;
                messages.Add(!string.IsNullOrWhiteSpace(be.Message) ? be.Message : GetStatusCodeDescription(code));
                if (!string.IsNullOrWhiteSpace(be.ErrorCode)) messages.Add(_localizer[ValidationResourceKeys.ErrorCode, be.ErrorCode]);
                if (be.Data is { Count: > 0 })
                {
                    messages.AddRange(be.GetDictionaryDataList()
                        .Select(data => $"{data.Key}: {data.Value}"));
                }

                if (be.InnerException != null && !env.IsHhsProduction())
                {
                    messages.AddRange(be.InnerException.GetMessages());
                }

                break;
            }
            case DomainException de:
            {
                code = StatusCodes.Status400BadRequest;
                messages.Add(!string.IsNullOrWhiteSpace(de.Message) ? de.Message : GetStatusCodeDescription(code));
                if (de.Data is { Count: > 0 })
                {
                    messages.AddRange(de.GetDictionaryDataList()
                        .Select(data => $"{data.Key}: {data.Value}"));
                }

                if (de.InnerException != null && !env.IsHhsProduction())
                {
                    messages.AddRange(de.InnerException.GetMessages());
                }

                break;
            }
            case BaseHttpException he:
            {
                code = he.HttpStatusCode;
                messages.Add(!string.IsNullOrWhiteSpace(he.Message) ? he.Message : GetStatusCodeDescription(code));
                if (he.Data is { Count: > 0 })
                {
                    messages.AddRange(he.GetDictionaryDataList()
                        .Select(data => $"{data.Key}: {data.Value}"));
                }

                if (he.InnerException != null && !env.IsHhsProduction())
                {
                    messages.AddRange(he.InnerException.GetMessages());
                }

                break;
            }
            default:
            {
                messages.Add(GetStatusCodeDescription(code));
                if (!env.IsHhsProduction())
                {
                    if (!string.IsNullOrWhiteSpace(ex.Message)) messages.Add(ex.Message);
                    messages.AddRange(ex.InnerException.GetMessages());
                }

                break;
            }
        }


        return (code, messages);
    }

    public string GetStatusCodeDescription(int statusCode)
    {
        if (statusCode is < 200 or > 520) return string.Empty;

        return (HttpStatusCode)statusCode switch
        {
            HttpStatusCode.BadRequest => _localizer["InvalidModelStateErrorMessage"],
            HttpStatusCode.Unauthorized => _localizer["UnauthorizedRequest"],
            HttpStatusCode.Forbidden => _localizer["ForbiddenRequest"],
            HttpStatusCode.NotFound => _localizer["NotFoundRequest"],
            HttpStatusCode.MethodNotAllowed => _localizer["MethodNotAllowed"],
            HttpStatusCode.RequestTimeout => _localizer["RequestTimeout"],
            HttpStatusCode.UnsupportedMediaType => _localizer["UnsupportedRequestContentType"],
            HttpStatusCode.OK => _localizer["SuccessRequest"],
            _ => _localizer[((HttpStatusCode)statusCode).ToString()]
        };
    }
}