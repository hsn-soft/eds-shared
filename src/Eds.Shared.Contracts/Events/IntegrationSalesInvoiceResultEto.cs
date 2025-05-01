using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationSalesInvoiceResultEto(
    Guid ReceivedQueueId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    Guid? SalesInvoiceId,
    Guid? SalesInvoiceUuid
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    public Guid? SalesInvoiceId { get; } = SalesInvoiceId;
    public Guid? SalesInvoiceUuid { get; } = SalesInvoiceUuid;
}