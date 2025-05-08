using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignPurchaseInvoiceAnswerResultEto(
    Guid PurchaseInvoiceAnswerId,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription,
    [NotNull] string PurchaseInvoiceAnswerIdentifier
) : IIntegrationEventMessage
{
    public Guid PurchaseInvoiceAnswerId { get; } = PurchaseInvoiceAnswerId;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
    [NotNull] public string PurchaseInvoiceAnswerIdentifier { get; } = PurchaseInvoiceAnswerIdentifier;
}