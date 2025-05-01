using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationPurchaseInvoiceAnswerResultEto(
    Guid ReceivedQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    Guid? PurchaseInvoiceAnswerId,
    Guid? PurchaseInvoiceAnswerUuid
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    public Guid? PurchaseInvoiceAnswerId { get; } = PurchaseInvoiceAnswerId;
    public Guid? PurchaseInvoiceAnswerUuid { get; } = PurchaseInvoiceAnswerUuid;
}