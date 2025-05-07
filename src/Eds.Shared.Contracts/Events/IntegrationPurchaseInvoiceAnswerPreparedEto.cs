using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events;

public sealed record IntegrationPurchaseInvoiceAnswerPreparedEto(
    Guid ReceivedQueueId,
    Guid TenantId,
    Guid ClientId,
    [NotNull] string PreparedFilePath,
    [NotNull] string PreparedFileName,
    Guid PurchaseInvoiceAnswerUuid,
    bool SendWithoutApprove,
    [CanBeNull] string ClientOutboxAlias,
    [CanBeNull] string UniqueIntegrationCode
) : IIntegrationEventMessage
{
    public Guid ReceivedQueueId { get; } = ReceivedQueueId;
    public Guid TenantId { get; } = TenantId;
    public Guid ClientId { get; } = ClientId;
    [NotNull] public string PreparedFilePath { get; } = PreparedFilePath;
    [NotNull] public string PreparedFileName { get; } = PreparedFileName;
    public Guid PurchaseInvoiceAnswerUuid { get; } = PurchaseInvoiceAnswerUuid;
    public bool SendWithoutApprove { get; } = SendWithoutApprove;
    [CanBeNull] public string ClientOutboxAlias { get; } = ClientOutboxAlias;
    [CanBeNull] public string UniqueIntegrationCode { get; } = UniqueIntegrationCode;
}