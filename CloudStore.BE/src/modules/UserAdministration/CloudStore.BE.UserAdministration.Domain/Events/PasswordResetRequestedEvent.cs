using MediatR;

namespace CloudStore.BE.UserAdministration.Domain.Events;

public record PasswordResetRequestedEvent(Guid UserId) : INotification;