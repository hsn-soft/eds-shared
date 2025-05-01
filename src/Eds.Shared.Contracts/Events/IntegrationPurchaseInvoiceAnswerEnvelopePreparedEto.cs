using System.ComponentModel.DataAnnotations;
using HsnSoft.Base.Domain.Entities.Events;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationPurchaseInvoiceAnswerEnvelopePreparedEto(
    Guid ReceivedQueueId,
    [Required] string PreparedFilePath,
    [Required] string PreparedFileName,
    long PreparedFileSize,
    Guid PurchaseInvoiceAnswerEnvelopeUuid
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    [Required] public string PreparedFilePath { get; } = PreparedFilePath;
    [Required] public string PreparedFileName { get; } = PreparedFileName;
    public long PreparedFileSize { get; } = PreparedFileSize;
    public Guid PurchaseInvoiceAnswerEnvelopeUuid { get; } = PurchaseInvoiceAnswerEnvelopeUuid;
}