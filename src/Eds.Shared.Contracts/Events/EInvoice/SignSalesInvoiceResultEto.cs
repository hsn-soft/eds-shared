using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignSalesInvoiceResultEto(
    Guid SalesInvoiceId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    Guid SalesInvoiceUuid
) : IIntegrationEventMessage
{
    public Guid SalesInvoiceId { get; } = SalesInvoiceId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    public Guid SalesInvoiceUuid { get; } = SalesInvoiceUuid;
}