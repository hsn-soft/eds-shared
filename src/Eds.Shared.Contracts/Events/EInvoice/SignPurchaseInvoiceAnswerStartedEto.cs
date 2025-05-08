using System.ComponentModel.DataAnnotations;
using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignPurchaseInvoiceAnswerStartedEto(
    Guid PurchaseInvoiceAnswerId,
    [NotNull] string UnsignedFilePath,
    [NotNull] string UnsignedFileName,
    [NotNull] string PurchaseInvoiceAnswerIdentifier
) : IIntegrationEventMessage
{
    public Guid PurchaseInvoiceAnswerId { get; } = PurchaseInvoiceAnswerId;
    [NotNull] public string UnsignedFilePath { get; } = UnsignedFilePath;
    [NotNull] public string UnsignedFileName { get; } = UnsignedFileName;
    [NotNull] public string PurchaseInvoiceAnswerIdentifier { get; } = PurchaseInvoiceAnswerIdentifier;
}