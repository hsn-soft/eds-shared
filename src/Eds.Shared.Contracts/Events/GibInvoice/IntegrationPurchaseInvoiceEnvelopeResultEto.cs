using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.GibInvoice;

public sealed record IntegrationPurchaseInvoiceEnvelopeResultEto(
    Guid ReceivedGibDocQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription
) : IIntegrationEventMessage
{
    public Guid ReceivedGibDocQueueId { get; } = ReceivedGibDocQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
}