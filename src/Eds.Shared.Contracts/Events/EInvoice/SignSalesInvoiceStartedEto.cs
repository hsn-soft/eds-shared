using System.ComponentModel.DataAnnotations;
using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.EInvoice;

public sealed record SignSalesInvoiceStartedEto(
    Guid SalesInvoiceId,
    [NotNull] string UnsignedFilePath,
    [NotNull] string UnsignedFileName,
    Guid SalesInvoiceUuid
) : IIntegrationEventMessage
{
    public Guid SalesInvoiceId { get; } = SalesInvoiceId;
    [NotNull] public string UnsignedFilePath { get; } = UnsignedFilePath;
    [NotNull] public string UnsignedFileName { get; } = UnsignedFileName;
    public Guid SalesInvoiceUuid { get; } = SalesInvoiceUuid;
}