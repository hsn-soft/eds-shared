using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.GibInvoice;

public sealed record PrepareSendGibSysQueueEto(
    Guid ReceivedGibDocQueueId,
    Guid TenantId,
    Guid ClientId,
    [NotNull] string  RefEnvelopeIdentifier,
    [NotNull] string  RefEnvelopeSenderRegisterNumber,
    [NotNull] string  RefEnvelopeSenderAlias,
    [NotNull] string  RefEnvelopeSenderTitle,
    [NotNull] string  RefEnvelopeReceiverRegisterNumber,
    [NotNull] string  RefEnvelopeReceiverAlias,
    [NotNull] string  RefEnvelopeReceiverTitle,
    Guid PreparedSendGibSysQueueId,
    [NotNull] string PreparedSendGibSysQueueResponseCode,
    [NotNull] string PreparedSendGibSysQueueResponseDescription

) : IIntegrationEventMessage
{
    public Guid ReceivedGibDocQueueId { get; } = ReceivedGibDocQueueId;
    public Guid TenantId { get; } = TenantId;
    public Guid ClientId { get; } = ClientId;
    [NotNull] public string RefEnvelopeIdentifier { get; } = RefEnvelopeIdentifier;
    [NotNull] public string RefEnvelopeSenderRegisterNumber { get; } = RefEnvelopeSenderRegisterNumber;
    [NotNull] public string RefEnvelopeSenderAlias { get; } = RefEnvelopeSenderAlias;
    [NotNull] public string RefEnvelopeSenderTitle { get; } = RefEnvelopeSenderTitle;
    [NotNull] public string RefEnvelopeReceiverRegisterNumber { get; } = RefEnvelopeReceiverRegisterNumber;
    [NotNull] public string RefEnvelopeReceiverAlias { get; } = RefEnvelopeReceiverAlias;
    [NotNull] public string RefEnvelopeReceiverTitle { get; } = RefEnvelopeReceiverTitle;
    public Guid PreparedSendGibSysQueueId { get; } = PreparedSendGibSysQueueId;
    [NotNull] public string PreparedSendGibSysQueueResponseCode { get; } = PreparedSendGibSysQueueResponseCode;
    [NotNull] public string PreparedSendGibSysQueueResponseDescription { get; } = PreparedSendGibSysQueueResponseDescription;
}