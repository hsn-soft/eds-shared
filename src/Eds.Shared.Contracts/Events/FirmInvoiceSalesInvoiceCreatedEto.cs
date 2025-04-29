using HsnSoft.Base.Domain.Entities.Events;

namespace Eds.Shared.Contracts.Events;

public sealed record FirmInvoiceSalesInvoiceCreatedEto(Guid FirmInvoiceIntegrationQueueId, Guid SalesInvoiceId, Guid SalesInvoiceUUID) : IIntegrationEventMessage
{
    public Guid FirmInvoiceIntegrationQueueId { get; } = FirmInvoiceIntegrationQueueId;
    public Guid SalesInvoiceId { get; } = SalesInvoiceId;
    public Guid SalesInvoiceUUID { get; } = SalesInvoiceUUID;
}