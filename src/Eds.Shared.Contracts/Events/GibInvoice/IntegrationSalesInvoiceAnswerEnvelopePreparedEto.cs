using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.GibInvoice;

public sealed record IntegrationSalesInvoiceAnswerEnvelopePreparedEto(
    Guid ReceivedGibDocQueueId,
    Guid TenantId,
    Guid ClientId,
    [NotNull] string PreparedFilePath,
    [NotNull] string PreparedFileName,
[NotNull] string SalesInvoiceAnswerEnvelopeIdentifier
) : IIntegrationEventMessage
{
    public Guid ReceivedGibDocQueueId { get; } = ReceivedGibDocQueueId;
    public Guid TenantId { get; } = TenantId;
    public Guid ClientId { get; } = ClientId;
    [NotNull] public string PreparedFilePath { get; } = PreparedFilePath;
    [NotNull] public string PreparedFileName { get; } = PreparedFileName;
    [NotNull] public string SalesInvoiceAnswerEnvelopeIdentifier { get; } = SalesInvoiceAnswerEnvelopeIdentifier;
}