using HsnSoft.Base.Domain.Entities.Events;

namespace Eds.Shared.Contracts.Events;

public sealed record AnalysisContentNormalizedResultEto(Guid AnalysisContentId, bool IsNormalizedSuccess, Guid NormalizedAnalysisId) : IIntegrationEventMessage
{
    public Guid AnalysisContentId { get; } = AnalysisContentId;
    public bool IsNormalizedSuccess { get; } = IsNormalizedSuccess;

    public Guid NormalizedAnalysisId { get; } = NormalizedAnalysisId;
}