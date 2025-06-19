using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationSysResEnvelopeResultEto(
    Guid ReceivedGibSysQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription
) : IIntegrationEventMessage
{
    public Guid ReceivedGibSysQueueId { get; } = ReceivedGibSysQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
}