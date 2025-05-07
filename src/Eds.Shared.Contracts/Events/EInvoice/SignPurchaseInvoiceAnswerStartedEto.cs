using System.ComponentModel.DataAnnotations;
using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignPurchaseInvoiceAnswerStartedEto(
    Guid PurchaseInvoiceAnswerId,
    [NotNull] string UnsignedFilePath,
    [NotNull] string UnsignedFileName,
    Guid PurchaseInvoiceAnswerUuid
) : IIntegrationEventMessage
{
    public Guid PurchaseInvoiceAnswerId { get; } = PurchaseInvoiceAnswerId;
    [NotNull] public string UnsignedFilePath { get; } = UnsignedFilePath;
    [NotNull] public string UnsignedFileName { get; } = UnsignedFileName;
    public Guid PurchaseInvoiceAnswerUuid { get; } = PurchaseInvoiceAnswerUuid;
}