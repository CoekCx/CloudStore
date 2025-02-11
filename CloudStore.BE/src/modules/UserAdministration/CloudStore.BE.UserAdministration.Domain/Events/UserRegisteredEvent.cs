using MediatR;

namespace CloudStore.BE.UserAdministration.Domain.Events;

public sealed record UserRegisteredEvent(Guid UserId) : INotification;