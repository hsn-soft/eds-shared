using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationPurchaseInvoiceAnswerEnvelopeResultEto(
    Guid ReceivedQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    Guid? PurchaseInvoiceAnswerEnvelopeId,
    Guid PurchaseInvoiceAnswerEnvelopeUuid
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    public Guid? PurchaseInvoiceAnswerEnvelopeId { get; } = PurchaseInvoiceAnswerEnvelopeId;
    public Guid PurchaseInvoiceAnswerEnvelopeUuid { get; } = PurchaseInvoiceAnswerEnvelopeUuid;
}