using Eds.Shared.Helper.Enums;
using HsnSoft.Base.Domain.Entities.Events;

namespace Eds.Shared.Contracts.Events;

public sealed record VideoGenerationRequestCreatedEto(ReferenceContentTypes ReferenceContentType, Guid ReferenceContentId, Guid VideoRequestId) : IIntegrationEventMessage
{
    public ReferenceContentTypes ReferenceContentType { get; } = ReferenceContentType;
    public Guid ReferenceContentId { get; } = ReferenceContentId;

    public Guid VideoRequestId { get; } = VideoRequestId;
}