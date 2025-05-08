using System.ComponentModel.DataAnnotations;
using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignPurchaseInvoiceAnswerStartedEto(
    Guid PurchaseInvoiceAnswerId,
    Guid TenantId,
    Guid ClientId,
    [NotNull] string UnsignedFilePath,
    [NotNull] string UnsignedFileName,
    [NotNull] string PurchaseInvoiceAnswerIdentifier,
    [NotNull] string PurchaseInvoiceAnswerNumber
) : IIntegrationEventMessage
{
    public Guid PurchaseInvoiceAnswerId { get; } = PurchaseInvoiceAnswerId;
    public Guid TenantId { get; } = TenantId;
    public Guid ClientId { get; } = ClientId;
    [NotNull] public string UnsignedFilePath { get; } = UnsignedFilePath;
    [NotNull] public string UnsignedFileName { get; } = UnsignedFileName;
    [NotNull] public string PurchaseInvoiceAnswerIdentifier { get; } = PurchaseInvoiceAnswerIdentifier;
    [NotNull] public string PurchaseInvoiceAnswerNumber { get; } = PurchaseInvoiceAnswerNumber;
}