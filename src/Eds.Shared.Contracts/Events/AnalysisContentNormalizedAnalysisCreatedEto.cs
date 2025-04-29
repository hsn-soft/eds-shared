using HsnSoft.Base.Domain.Entities.Events;

namespace Eds.Shared.Contracts.Events;

public sealed record AnalysisContentNormalizedAnalysisCreatedEto(Guid AnalysisContentId, Guid NormalizedAnalysisId) : IIntegrationEventMessage
{
    public Guid AnalysisContentId { get; } = AnalysisContentId;
    public Guid NormalizedAnalysisId { get; } = NormalizedAnalysisId;
}