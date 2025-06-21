using HsnSoft.Base.Domain.Entities.Events;
using JetBrains.Annotations;

namespace Eds.Shared.Contracts.Events.GibInvoice;

public sealed record SendGibSysQueueResultEto(
    Guid ReceivedGibDocQueueId,
    Guid SendGibSysQueueId,
    [NotNull] string SendGibSysQueueResponseCode,
    [NotNull] string SendGibSysQueueResponseDescription,
    bool IsOperationSuccess,
    [CanBeNull] string OperationDescription
) : IIntegrationEventMessage
{
    public Guid ReceivedGibDocQueueId { get; } = ReceivedGibDocQueueId;
    public Guid SendGibSysQueueId { get; } = SendGibSysQueueId;
    [NotNull]  public string SendGibSysQueueResponseCode { get; } = SendGibSysQueueResponseCode;
    [NotNull]  public string SendGibSysQueueResponseDescription { get; } = SendGibSysQueueResponseDescription;
    public bool IsOperationSuccess { get; } = IsOperationSuccess;
    [CanBeNull] public string OperationDescription { get; } = OperationDescription;
}