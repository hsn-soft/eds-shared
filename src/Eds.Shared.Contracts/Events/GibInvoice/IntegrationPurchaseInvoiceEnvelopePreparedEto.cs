using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.GibInvoice;

public sealed record IntegrationPurchaseInvoiceEnvelopePreparedEto(
    Guid ReceivedGibDocQueueId,
    Guid TenantId,
    Guid ClientId,
    [NotNull] string PreparedFilePath,
    [NotNull] string PreparedFileName,
    [NotNull] string  PurchaseInvoiceEnvelopeIdentifier
) : IIntegrationEventMessage
{
    public Guid ReceivedGibDocQueueId { get; } = ReceivedGibDocQueueId;
    public Guid TenantId { get; } = TenantId;
    public Guid ClientId { get; } = ClientId;
    [NotNull] public string PreparedFilePath { get; } = PreparedFilePath;
    [NotNull] public string PreparedFileName { get; } = PreparedFileName;
    [NotNull] public string PurchaseInvoiceEnvelopeIdentifier { get; } = PurchaseInvoiceEnvelopeIdentifier;
}