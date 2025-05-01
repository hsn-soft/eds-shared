using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationSalesInvoiceEnvelopeResultEto(
    Guid ReceivedQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    Guid? SalesInvoiceEnvelopeId,
    Guid? SalesInvoiceEnvelopeUuid
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    public Guid? SalesInvoiceEnvelopeId { get; } = SalesInvoiceEnvelopeId;
    public Guid? SalesInvoiceEnvelopeUuid { get; } = SalesInvoiceEnvelopeUuid;
}